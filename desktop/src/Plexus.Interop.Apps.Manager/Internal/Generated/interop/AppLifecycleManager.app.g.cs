/**
 * Copyright 2017-2018 Plexus Interop Deutsche Bank AG
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
// 	source: interop\app_lifecycle_manager.interop
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code
namespace Plexus.Interop.Apps.Internal.Generated {
	
	using System;
	using global::Plexus;
	using global::Plexus.Channels;
	using global::Plexus.Interop;
	using global::System.Threading.Tasks;
					
					
	internal partial interface IAppLifecycleManagerClient: IClient {
		AppLifecycleManagerClient.IAppLauncherServiceProxy AppLauncherService { get; }
	}
	
	internal sealed partial class AppLifecycleManagerClient: ClientBase, IAppLifecycleManagerClient {
		
		public const string Id = "interop.AppLifecycleManager";
		
		private static ClientOptions CreateClientOptions(AppLifecycleManagerClient.ServiceBinder serviceBinder, Func<ClientOptionsBuilder, ClientOptionsBuilder> setup = null) {
			ClientOptionsBuilder builder = new ClientOptionsBuilder().WithApplicationId(Id).WithDefaultConfiguration();
			serviceBinder.Bind(builder);
			if (setup != null) {
				builder = setup(builder);
			}
			return builder.Build();
		}
		
		public AppLifecycleManagerClient(
			AppLifecycleManagerClient.IAppLifecycleServiceImpl appLifecycleService,
			Func<ClientOptionsBuilder, ClientOptionsBuilder> setup = null
		)
		:this(new AppLifecycleManagerClient.ServiceBinder(
			appLifecycleService
		), setup) { }
		
		public AppLifecycleManagerClient(AppLifecycleManagerClient.ServiceBinder serviceBinder, Func<ClientOptionsBuilder, ClientOptionsBuilder> setup = null): base(CreateClientOptions(serviceBinder, setup)) 
		{
			AppLauncherService = new AppLifecycleManagerClient.AppLauncherServiceProxy(this.CallInvoker);
		}
	
		public sealed partial class ServiceBinder {
			
			public ServiceBinder(
				AppLifecycleManagerClient.IAppLifecycleServiceImpl appLifecycleService
			) {
				_appLifecycleServiceBinder = new AppLifecycleManagerClient.AppLifecycleServiceBinder(appLifecycleService);
			}
			
			private AppLifecycleServiceBinder _appLifecycleServiceBinder;
			
			public ClientOptionsBuilder Bind(ClientOptionsBuilder builder) {
				builder = _appLifecycleServiceBinder.Bind(builder);
				return builder;
			}
		}
	
		public partial interface IAppLifecycleServiceImpl:
			global::Plexus.Interop.Apps.Internal.Generated.AppLifecycleService.IResolveAppImpl,
			global::Plexus.Interop.Apps.Internal.Generated.AppLifecycleService.IGetLifecycleEventStreamImpl
		{ }
		
		private sealed partial class AppLifecycleServiceBinder {
			
			
			private readonly IAppLifecycleServiceImpl _impl;
			
			public AppLifecycleServiceBinder(IAppLifecycleServiceImpl impl) {
				_impl = impl;
			}
			
			public ClientOptionsBuilder Bind(ClientOptionsBuilder builder) {
				return builder.WithProvidedService(global::Plexus.Interop.Apps.Internal.Generated.AppLifecycleService.Id, Bind);
			}
			
			private ProvidedServiceDefinition.Builder Bind(ProvidedServiceDefinition.Builder builder) {
				builder = builder.WithUnaryMethod<global::Plexus.Interop.Apps.Internal.Generated.ResolveAppRequest, global::Plexus.Interop.Apps.Internal.Generated.ResolveAppResponse>(global::Plexus.Interop.Apps.Internal.Generated.AppLifecycleService.ResolveAppMethodId, _impl.ResolveApp);
				builder = builder.WithServerStreamingMethod<global::Plexus.Interop.Apps.Internal.Generated.Empty, global::Plexus.Interop.Apps.Internal.Generated.AppLifecycleEvent>(global::Plexus.Interop.Apps.Internal.Generated.AppLifecycleService.GetLifecycleEventStreamMethodId, _impl.GetLifecycleEventStream);
				return builder; 							
			}
		}
		
		public sealed partial class AppLifecycleServiceImpl: IAppLifecycleServiceImpl
		{
			private readonly UnaryMethodHandler<global::Plexus.Interop.Apps.Internal.Generated.ResolveAppRequest, global::Plexus.Interop.Apps.Internal.Generated.ResolveAppResponse> _resolveAppHandler;
			private readonly ServerStreamingMethodHandler<global::Plexus.Interop.Apps.Internal.Generated.Empty, global::Plexus.Interop.Apps.Internal.Generated.AppLifecycleEvent> _getLifecycleEventStreamHandler;
			
			public AppLifecycleServiceImpl(
				UnaryMethodHandler<global::Plexus.Interop.Apps.Internal.Generated.ResolveAppRequest, global::Plexus.Interop.Apps.Internal.Generated.ResolveAppResponse> resolveAppHandler,
				ServerStreamingMethodHandler<global::Plexus.Interop.Apps.Internal.Generated.Empty, global::Plexus.Interop.Apps.Internal.Generated.AppLifecycleEvent> getLifecycleEventStreamHandler
			) {
				_resolveAppHandler = resolveAppHandler;
				_getLifecycleEventStreamHandler = getLifecycleEventStreamHandler;
			}
			
			public Task<global::Plexus.Interop.Apps.Internal.Generated.ResolveAppResponse> ResolveApp(global::Plexus.Interop.Apps.Internal.Generated.ResolveAppRequest request, MethodCallContext context) {
				return _resolveAppHandler(request, context);
			}
			
			public Task GetLifecycleEventStream(global::Plexus.Interop.Apps.Internal.Generated.Empty request, IWritableChannel<global::Plexus.Interop.Apps.Internal.Generated.AppLifecycleEvent> responseStream, MethodCallContext context) {
				return _getLifecycleEventStreamHandler(request, responseStream, context);
			}
		}					
		
		public sealed partial class AppLifecycleServiceImpl<T>: IAppLifecycleServiceImpl
			where T:
			global::Plexus.Interop.Apps.Internal.Generated.AppLifecycleService.IResolveAppImpl,
			global::Plexus.Interop.Apps.Internal.Generated.AppLifecycleService.IGetLifecycleEventStreamImpl
		{
			private readonly T _impl;
			
			public AppLifecycleServiceImpl(T impl) {
				_impl = impl;
			}
			
			public Task<global::Plexus.Interop.Apps.Internal.Generated.ResolveAppResponse> ResolveApp(global::Plexus.Interop.Apps.Internal.Generated.ResolveAppRequest request, MethodCallContext context) {
				return _impl.ResolveApp(request, context);
			}
			
			public Task GetLifecycleEventStream(global::Plexus.Interop.Apps.Internal.Generated.Empty request, IWritableChannel<global::Plexus.Interop.Apps.Internal.Generated.AppLifecycleEvent> responseStream, MethodCallContext context) {
				return _impl.GetLifecycleEventStream(request, responseStream, context);
			}
		}
		
		public partial interface IAppLauncherServiceProxy:
			global::Plexus.Interop.Apps.Internal.Generated.AppLauncherService.ILaunchProxy
		{ }
		
		public sealed partial class AppLauncherServiceProxy: IAppLauncherServiceProxy {
			
			public static global::Plexus.Interop.Apps.Internal.Generated.AppLauncherService.Descriptor Descriptor = global::Plexus.Interop.Apps.Internal.Generated.AppLauncherService.DefaultDescriptor;
			
			private readonly IClientCallInvoker _callInvoker;
									
			public AppLauncherServiceProxy(IClientCallInvoker callInvoker) {
				_callInvoker = callInvoker;
			}						
			
			public IUnaryMethodCall<global::Plexus.Interop.Apps.Internal.Generated.AppLaunchResponse> Launch(global::Plexus.Interop.Apps.Internal.Generated.AppLaunchRequest request) {
				return _callInvoker.Call(Descriptor.LaunchMethod, request);
			}
		}
		
		public IAppLauncherServiceProxy AppLauncherService { get; private set; }
	}
}
#endregion Designer generated code
