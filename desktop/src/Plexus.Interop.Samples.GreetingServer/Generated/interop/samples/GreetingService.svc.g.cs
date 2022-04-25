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
// <auto-generated>
// 	Generated by the Plexus Interop compiler.  DO NOT EDIT!
// 	source: interop\samples\greeting_service.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code
namespace Plexus.Interop.Samples.GreetingServer.Generated {
	
	using System;
	using global::Plexus;
	using global::Plexus.Channels;
	using global::Plexus.Interop;
	using global::System.Threading.Tasks;
					
	internal static partial class GreetingService {
		
		public const string Id = "interop.samples.GreetingService";			
		public const string UnaryMethodId = "Unary";
		public const string ServerStreamingMethodId = "ServerStreaming";
		public const string ClientStreamingMethodId = "ClientStreaming";
		public const string DuplexStreamingMethodId = "DuplexStreaming";
		
		public static readonly GreetingService.Descriptor DefaultDescriptor = CreateDescriptor();
		
		public static GreetingService.Descriptor CreateDescriptor() {
			return new GreetingService.Descriptor();
		} 
		
		public static GreetingService.Descriptor CreateDescriptor(string alias) {
			return new GreetingService.Descriptor(alias);
		}				
	
		public partial interface IUnaryProxy {
			IUnaryMethodCall<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse> Unary(global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest request);
		}
		
		public partial interface IServerStreamingProxy {
			IServerStreamingMethodCall<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse> ServerStreaming(global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest request);
		}
		
		public partial interface IClientStreamingProxy {
			IClientStreamingMethodCall<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest, global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse> ClientStreaming();
		}
		
		public partial interface IDuplexStreamingProxy {
			IDuplexStreamingMethodCall<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest, global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse> DuplexStreaming();
		}
		
		public partial interface IUnaryImpl {
			Task<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse> Unary(global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest request, MethodCallContext context);
		}
		
		public partial interface IServerStreamingImpl {
			Task ServerStreaming(global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest request, IWritableChannel<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse> responseStream, MethodCallContext context);
		}
		
		public partial interface IClientStreamingImpl {
			Task<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse> ClientStreaming(IReadableChannel<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest> requestStream, MethodCallContext context);
		}
		
		public partial interface IDuplexStreamingImpl {
			Task DuplexStreaming(IReadableChannel<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest> requestStream, IWritableChannel<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse> responseStream, MethodCallContext context);
		}
		
		public sealed partial class Descriptor {
		
			public UnaryMethod<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest, global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse> UnaryMethod {get; private set; }
			public ServerStreamingMethod<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest, global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse> ServerStreamingMethod {get; private set; }
			public ClientStreamingMethod<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest, global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse> ClientStreamingMethod {get; private set; }
			public DuplexStreamingMethod<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest, global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse> DuplexStreamingMethod {get; private set; }
			
			public Descriptor() {				
				UnaryMethod = Method.Unary<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest, global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse>(Id, UnaryMethodId);
				ServerStreamingMethod = Method.ServerStreaming<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest, global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse>(Id, ServerStreamingMethodId);
				ClientStreamingMethod = Method.ClientStreaming<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest, global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse>(Id, ClientStreamingMethodId);
				DuplexStreamingMethod = Method.DuplexStreaming<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest, global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse>(Id, DuplexStreamingMethodId);
			}
		
			public Descriptor(string alias) {
				UnaryMethod = Method.Unary<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest, global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse>(Id, alias, UnaryMethodId);
				ServerStreamingMethod = Method.ServerStreaming<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest, global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse>(Id, alias, ServerStreamingMethodId);
				ClientStreamingMethod = Method.ClientStreaming<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest, global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse>(Id, alias, ClientStreamingMethodId);
				DuplexStreamingMethod = Method.DuplexStreaming<global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingRequest, global::Plexus.Interop.Samples.GreetingServer.Generated.GreetingResponse>(Id, alias, DuplexStreamingMethodId);
			}
		}
	}
					
}
#endregion Designer generated code
