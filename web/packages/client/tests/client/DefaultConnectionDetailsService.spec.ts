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
import { ConnectionDetails } from '../../src/client/api/container/ConnectionDetails';
import { DefaultConnectionDetailsService } from '../../src/client/api/container/DefaultConnectionDetailsService';

describe('DefaultConnectionDetailsService', () => {

    it('getConnectionDetails returns correct result', (done) => {
        const service = new DefaultConnectionDetailsService(getConnectionDetails);
        service.getConnectionDetails().then(r => {
            expect(r.ws.port).toBe(42);
            expect(r.ws.wssPort).toBe(24);
            expect(r.appInstanceId).toBe('007');
            done();
        });
    });

    it('getMetadataUrl returns correct result', (done) => {
        const service = new DefaultConnectionDetailsService(getConnectionDetails);
        service.getMetadataUrl().then(r => {
            expect(r).toBe('wss://127.0.0.1:24/metadata/interop');
            done();
        });
    });

    function getConnectionDetails(): Promise<ConnectionDetails> {
        return Promise.resolve<ConnectionDetails>({
            ws: {
                port: 42,
                wssPort: 24
            },
            appInstanceId: '007'
        });
    }
});
