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
import { expect } from 'chai';

import { DiscoveredMethod, DiscoveredServiceMethod, ProvidedMethodReference } from '@plexus-interop/client-api';

import * as plexus from '../../src/echo/server/plexus-messages';
import { ClientsSetup } from '../common/ClientsSetup';
import { ConnectionProvider } from '../common/ConnectionProvider';
import { BaseEchoTest } from './BaseEchoTest';
import { ClientStreamingHandler } from './ClientStreamingHandler';
import { NopServiceHandler } from './NopServiceHandler';
import { ServerStreamingHandler } from './ServerStreamingHandler';
import { UnaryServiceHandler } from './UnaryServiceHandler';

export class DiscoveryTests extends BaseEchoTest {
  public constructor(
    private connectionProvider: ConnectionProvider,
    private clientsSetup: ClientsSetup = new ClientsSetup()
  ) {
    super();
  }

  public testMethodDiscoveredByInputMessageId(): Promise<void> {
    return this.clientsSetup.createEchoClients(this.connectionProvider, new NopServiceHandler()).then((clients) => {
      const client = clients[0];
      return client
        .discoverMethod({ inputMessageId: 'plexus.interop.testing.EchoRequest' })
        .then((discoveryResponse) => {
          if (discoveryResponse.methods) {
            expect(discoveryResponse.methods.length).to.be.eq(4);
            discoveryResponse.methods.forEach((method) => this.assertDiscoveredMethodValid(method));
          } else {
            throw new Error('Empty response');
          }
        })
        .then(() => this.clientsSetup.disconnect(clients[0], clients[1]));
    });
  }

  public async testServiceDiscoveredById(): Promise<void> {
    const serviceId = 'plexus.interop.testing.EchoService';
    const [client, server] = await this.clientsSetup.createEchoClients(
      this.connectionProvider,
      new NopServiceHandler()
    );
    const serviceDiscoveryResponse = await client.discoverService({
      consumedService: {
        serviceId,
      },
    });
    if (serviceDiscoveryResponse.services) {
      expect(serviceDiscoveryResponse.services.length).to.be.eq(1);
      const serviceRef = serviceDiscoveryResponse.services[0];
      if (serviceRef.consumedService) {
        expect(serviceRef.consumedService.serviceId).to.eq(serviceId);
      } else {
        throw new Error('Empty consumed service');
      }
      if (serviceRef.providedService) {
        expect(serviceRef.providedService.applicationId).to.eq('plexus.interop.testing.EchoServer');
        // eslint-disable-next-line @typescript-eslint/no-unused-expressions
        expect(serviceRef.providedService.connectionId).to.not.be.undefined;
        expect(serviceRef.providedService.serviceId).to.eq(serviceId);
      } else {
        throw new Error('Empty provided service');
      }
      if (serviceRef.methods) {
        expect(serviceRef.methods.length).to.be.greaterThan(0);
        serviceRef.methods.forEach((method) => this.assertDiscoveredServiceMethodValid(method));
      } else {
        throw new Error('Empty methods');
      }
    } else {
      throw new Error('Empty Response');
    }
    await this.clientsSetup.disconnect(client, server);
  }

  public async testServiceDiscoveryReceivesEmptyResponseForWrongId(): Promise<void> {
    const [client, server] = await this.clientsSetup.createEchoClients(
      this.connectionProvider,
      new NopServiceHandler()
    );
    const serviceDiscoveryResponse = await client.discoverService({
      consumedService: {
        serviceId: 'plexus.interop.testing.DoNotExist',
      },
    });
    if (serviceDiscoveryResponse.services) {
      expect(serviceDiscoveryResponse.services.length).to.be.eq(0);
    }
    await this.clientsSetup.disconnect(client, server);
  }

  public async testClientCanInvokeDiscoveredServerStreamingRequest(): Promise<void> {
    const echoRequest = this.clientsSetup.createRequestDto();
    const handler = new ServerStreamingHandler(async (context, request, hostClient) => {
      hostClient.next(echoRequest);
      hostClient.complete();
    });
    const [client, server] = await this.clientsSetup.createEchoClients(this.connectionProvider, handler);
    const discoveryResponse = await client.discoverMethod({
      consumedMethod: {
        consumedService: {
          serviceId: 'plexus.interop.testing.EchoService',
        },
        methodId: 'ServerStreaming',
      },
    });
    if (discoveryResponse.methods) {
      expect(discoveryResponse.methods.length).to.be.eq(1);
      const method = discoveryResponse.methods[0];
      if (!method.providedMethod) {
        throw new Error('Provided method is empty');
      }
      let receivedResponse: plexus.plexus.interop.testing.IEchoRequest | null = null;
      return new Promise<void>((resolve, reject) => {
        client.sendRawServerStreamingRequest(
          method.providedMethod as ProvidedMethodReference,
          this.encodeRequestDto(echoRequest),
          {
            next: (response) => {
              receivedResponse = this.decodeRequestDto(response);
            },
            complete: async () => {
              this.assertEqual(echoRequest, receivedResponse as plexus.plexus.interop.testing.IEchoRequest);
              await this.clientsSetup.disconnect(client, server);
              resolve();
            },
            error: (e) => reject(e),
            streamCompleted: () => {},
          }
        );
      });
    }
    throw new Error('Empty response');
  }

  public async testClientCanInvokeDiscoveredBidiStreamingRequest(): Promise<void> {
    const echoRequest = this.clientsSetup.createRequestDto();
    const handler = new ClientStreamingHandler((context, hostClient) => ({
      next: () => hostClient.complete(),
      complete: () => {},
      // eslint-disable-next-line no-console
      error: (e) => console.error('Error received by server', e),
      streamCompleted: () => {},
    }));
    const [client, server] = await this.clientsSetup.createEchoClients(this.connectionProvider, handler);
    const discoveryResponse = await client.discoverMethod({
      consumedMethod: {
        consumedService: {
          serviceId: 'plexus.interop.testing.EchoService',
        },
        methodId: 'DuplexStreaming',
      },
    });
    if (discoveryResponse.methods) {
      expect(discoveryResponse.methods.length).to.be.eq(1);
      const method = discoveryResponse.methods[0];
      if (!method.providedMethod) {
        throw new Error('Provided method is empty');
      }
      // eslint-disable-next-line no-async-promise-executor
      return new Promise<void>(async (resolve, reject) => {
        const streamingClient = await client.sendRawBidirectionalStreamingRequest(
          method.providedMethod as ProvidedMethodReference,
          {
            next: () => {},
            error: (e) => {
              reject(e);
            },
            complete: async () => {
              await this.clientsSetup.disconnect(client, server);
              resolve();
            },
            streamCompleted: () => {},
          }
        );
        streamingClient.next(this.encodeRequestDto(echoRequest));
        streamingClient.complete();
      });
    }
    throw new Error('Empty response');
  }

  public async testClientCanInvokeDiscoveredMethodPassingRawData(): Promise<void> {
    const echoRequest = this.clientsSetup.createRequestDto();
    const handler = new UnaryServiceHandler(async (context, request) => request);
    const [client, server] = await this.clientsSetup.createEchoClients(this.connectionProvider, handler);
    const discoveryResponse = await client.discoverMethod({
      consumedMethod: {
        consumedService: {
          serviceId: 'plexus.interop.testing.EchoService',
        },
        methodId: 'Unary',
      },
    });
    if (discoveryResponse.methods) {
      expect(discoveryResponse.methods.length).to.be.eq(2);
      const method = discoveryResponse.methods[0];
      this.assertDiscoveredMethodValid(method);
      // invoke discovered
      await new Promise<void>((invocationResolve, invocationReject) => {
        client.sendRawUnaryRequest(
          method.providedMethod as ProvidedMethodReference,
          this.encodeRequestDto(echoRequest),
          {
            value: (response: ArrayBuffer) => {
              const echoResponse = this.decodeRequestDto(response);
              this.assertEqual(echoRequest, echoResponse);
              invocationResolve();
            },
            error: (e) => invocationReject(e),
          }
        );
      });
    } else {
      throw new Error('Empty response');
    }
    await this.clientsSetup.disconnect(client, server);
  }

  public async testClientCanInvokeDiscoveredMethodPassingObject(): Promise<void> {
    const echoRequest = this.clientsSetup.createRequestDto();
    const handler = new UnaryServiceHandler(async (context, request) => request);
    const [client, server] = await this.clientsSetup.createEchoClients(this.connectionProvider, handler);
    const discoveryResponse = await client.discoverMethod({
      consumedMethod: {
        consumedService: {
          serviceId: 'plexus.interop.testing.EchoService',
        },
        methodId: 'Unary',
      },
    });
    if (discoveryResponse.methods) {
      expect(discoveryResponse.methods.length).to.be.eq(2);
      const method = discoveryResponse.methods[0];
      this.assertDiscoveredMethodValid(method);
      // invoke discovered
      await new Promise<void>((invocationResolve, invocationReject) => {
        client.sendUnaryRequest(
          method.providedMethod as ProvidedMethodReference,
          echoRequest,
          {
            value: (response) => {
              this.assertEqual(echoRequest, response);
              invocationResolve();
            },
            error: (e) => invocationReject(e),
          },
          plexus.plexus.interop.testing.EchoRequest,
          plexus.plexus.interop.testing.EchoRequest
        );
      });
    } else {
      throw new Error('Empty response');
    }
    await this.clientsSetup.disconnect(client, server);
  }

  public async testMethodDiscoveredByOutputMessageId(): Promise<void> {
    const [client, server] = await this.clientsSetup.createEchoClients(
      this.connectionProvider,
      new NopServiceHandler()
    );
    const discoveryResponse = await client.discoverMethod({ outputMessageId: 'plexus.interop.testing.EchoRequest' });
    if (discoveryResponse.methods) {
      expect(discoveryResponse.methods.length).to.be.eq(4);
      discoveryResponse.methods.forEach((method) => this.assertDiscoveredMethodValid(method));
    } else {
      throw new Error('Empty response');
    }
    await this.clientsSetup.disconnect(client, server);
  }

  public async testMethodDiscoveredByReference(): Promise<void> {
    const [client, server] = await this.clientsSetup.createEchoClients(
      this.connectionProvider,
      new NopServiceHandler()
    );
    const discoveryResponse = await client.discoverMethod({
      consumedMethod: {
        consumedService: {
          serviceId: 'plexus.interop.testing.EchoService',
        },
        methodId: 'Unary',
      },
    });
    if (discoveryResponse.methods) {
      expect(discoveryResponse.methods.length).to.be.eq(2);
      discoveryResponse.methods.forEach((method) => this.assertDiscoveredMethodValid(method));
    } else {
      throw new Error('Empty response');
    }
    await this.clientsSetup.disconnect(client, server);
  }

  private assertDiscoveredMethodValid(discoveredMethod: DiscoveredMethod): void {
    // eslint-disable-next-line @typescript-eslint/no-unused-expressions
    expect(discoveredMethod.providedMethod).to.not.be.undefined;
    expect(discoveredMethod.inputMessageId).to.be.eq('plexus.interop.testing.EchoRequest');
    expect(discoveredMethod.outputMessageId).to.be.eq('plexus.interop.testing.EchoRequest');
    // eslint-disable-next-line @typescript-eslint/no-unused-expressions
    expect(discoveredMethod.options).to.not.be.undefined;
    const options = discoveredMethod.options || [];
    expect(options.length).to.be.greaterThan(0);
    expect(options[0].id).to.be.eq('interop.ProvidedMethodOptions.title');
    expect(options[0].value).to.be.eq(discoveredMethod.methodTitle);
  }

  private assertDiscoveredServiceMethodValid(discoveredMethod: DiscoveredServiceMethod): void {
    // eslint-disable-next-line @typescript-eslint/no-unused-expressions
    expect(discoveredMethod.methodId).to.not.be.undefined;
    // eslint-disable-next-line @typescript-eslint/no-unused-expressions
    expect(discoveredMethod.methodTitle).to.not.be.undefined;
    expect(discoveredMethod.inputMessageId).to.be.eq('plexus.interop.testing.EchoRequest');
    expect(discoveredMethod.outputMessageId).to.be.eq('plexus.interop.testing.EchoRequest');
    const options = discoveredMethod.options || [];
    expect(options.length).to.be.greaterThan(0);
    expect(options[0].id).to.be.eq('interop.ProvidedMethodOptions.title');
    expect(options[0].value).to.be.eq(discoveredMethod.methodTitle);
  }
}
