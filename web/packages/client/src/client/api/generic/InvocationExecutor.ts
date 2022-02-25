/**
 * Copyright 2017-2022 Plexus Interop Deutsche Bank AG
 * SPDX-License-Identifier: Apache-2.0
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
import { ActionReference, MethodInvocationContext } from '@plexus-interop/client-api';
import { AnonymousSubscription, Logger, LoggerFactory } from '@plexus-interop/common';
import { ClientProtocolHelper, ErrorCompletion, InvocationMetaInfo, SuccessCompletion } from '@plexus-interop/protocol';
import { UniqueId } from '@plexus-interop/transport-common';

import { BaseInvocation } from '../../generic/BaseInvocation';
import { Invocation } from '../../generic/Invocation';
import { InvocationHandlersRegistry } from './handlers/InvocationHandlersRegistry';
import { StreamingInvocationHost } from './handlers/streaming/StreamingInvocationHost';

export class InvocationExecutor {
  private log: Logger = LoggerFactory.getLogger('InvocationExecutor');

  constructor(private readonly handlersRegistry: InvocationHandlersRegistry) {}

  public handleGenericInvocation(invocation: Invocation): void {
    this.log.trace(`Received invocation`);
    new StreamingInvocationHost(this.handlersRegistry).executeGenericHandler(invocation);
  }

  public handleInvocation(invocation: BaseInvocation<any, any>): void {
    this.log.trace(`Received invocation`);
    new StreamingInvocationHost(this.handlersRegistry).executeTypeAwareHandler(invocation);
  }

  public invokeRawUnaryHandler(
    invocationContext: MethodInvocationContext,
    actionReference: ActionReference,
    requestPayloadBuffer: ArrayBuffer
  ): Promise<ArrayBuffer> {
    return this.internalInvokeUnaryHandler(invocationContext, actionReference, requestPayloadBuffer, false);
  }

  public invokeUnaryHandler(
    invocationContext: MethodInvocationContext,
    actionReference: ActionReference,
    requestPayload: any
  ): Promise<any> {
    return this.internalInvokeUnaryHandler(invocationContext, actionReference, requestPayload, true);
  }

  public internalInvokeUnaryHandler(
    invocationContext: MethodInvocationContext,
    actionReference: ActionReference,
    requestPayload: any,
    isTyped: boolean
  ): Promise<any> {
    return new Promise<any>((resolve, reject) => {
      const invocation: BaseInvocation<any, any> = {
        uuid: () => UniqueId.generateNew(),
        sendMessage: async (response) => resolve(response),
        open: (observer) => {
          observer.started(new AnonymousSubscription());
          observer.next(requestPayload);
          observer.streamCompleted();
          observer.complete();
        },
        getMetaInfo: () => {
          const metaInfo: InvocationMetaInfo = {
            ...actionReference,
            consumerApplicationId: invocationContext.consumerApplicationId,
            consumerConnectionId: invocationContext.consumerConnectionId,
          };
          return metaInfo;
        },
        sendCompleted: async () => {},
        close: async (completion) => {
          if (!ClientProtocolHelper.isSuccessCompletion(completion || new SuccessCompletion())) {
            reject(completion);
            return new ErrorCompletion();
          }
          return new SuccessCompletion();
        },
      };

      if (isTyped) {
        this.handleInvocation(invocation);
      } else {
        this.handleGenericInvocation(invocation);
      }
    });
  }
}
