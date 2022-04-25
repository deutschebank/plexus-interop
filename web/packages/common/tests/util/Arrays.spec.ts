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
import { arrayBufferToString, Arrays, stringToArrayBuffer } from '../../src/util/Arrays';

describe('Arrays', () => {
  it('Should contatenate two Array Buffers', () => {
    const first = new Uint32Array([1, 2, 3]);
    const second = new Uint32Array([5, 6, 7]);
    expect(new Uint32Array(Arrays.concatenateBuffers(first.buffer, second.buffer))).toEqual(
      new Uint32Array([1, 2, 3, 5, 6, 7])
    );
  });

  it('Should convert array buffer to string and back', () => {
    const first = new Uint8Array([123, 231, 312]);
    expect(first).toEqual(new Uint8Array(stringToArrayBuffer(arrayBufferToString(first.buffer))));
  });
});
