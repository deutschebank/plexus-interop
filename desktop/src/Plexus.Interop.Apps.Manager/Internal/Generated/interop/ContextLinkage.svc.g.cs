// <auto-generated>
// 	Generated by the Plexus Interop compiler.  DO NOT EDIT!
// 	source: interop\context_linkage.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code
namespace Plexus.Interop.Apps.Internal.Generated {
	
	using System;
	using global::Plexus;
	using global::Plexus.Channels;
	using global::Plexus.Interop;
	using global::System.Threading.Tasks;
					
	internal static partial class ContextLinkageService {
		
		public const string Id = "interop.ContextLinkageService";			
		public const string CreateContextMethodId = "CreateContext";
		public const string JoinContextMethodId = "JoinContext";
		public const string GetContextsMethodId = "GetContexts";
		public const string GetLinkedInvocationsMethodId = "GetLinkedInvocations";
		public const string GetAllLinkedInvocationsMethodId = "GetAllLinkedInvocations";
		
		public static readonly ContextLinkageService.Descriptor DefaultDescriptor = CreateDescriptor();
		
		public static ContextLinkageService.Descriptor CreateDescriptor() {
			return new ContextLinkageService.Descriptor();
		} 
		
		public static ContextLinkageService.Descriptor CreateDescriptor(string alias) {
			return new ContextLinkageService.Descriptor(alias);
		}				
	
		public partial interface ICreateContextProxy {
			IUnaryMethodCall<global::Plexus.Interop.Apps.Internal.Generated.Context> CreateContext(global::Google.Protobuf.WellKnownTypes.Empty request);
		}
		
		public partial interface IJoinContextProxy {
			IUnaryMethodCall<global::Google.Protobuf.WellKnownTypes.Empty> JoinContext(global::Plexus.Interop.Apps.Internal.Generated.Context request);
		}
		
		public partial interface IGetContextsProxy {
			IUnaryMethodCall<global::Plexus.Interop.Apps.Internal.Generated.ContextsList> GetContexts(global::Google.Protobuf.WellKnownTypes.Empty request);
		}
		
		public partial interface IGetLinkedInvocationsProxy {
			IUnaryMethodCall<global::Plexus.Interop.Apps.Internal.Generated.InvocationsList> GetLinkedInvocations(global::Plexus.Interop.Apps.Internal.Generated.Context request);
		}
		
		public partial interface IGetAllLinkedInvocationsProxy {
			IUnaryMethodCall<global::Plexus.Interop.Apps.Internal.Generated.ContextToInvocationsList> GetAllLinkedInvocations(global::Google.Protobuf.WellKnownTypes.Empty request);
		}
		
		public partial interface ICreateContextImpl {
			Task<global::Plexus.Interop.Apps.Internal.Generated.Context> CreateContext(global::Google.Protobuf.WellKnownTypes.Empty request, MethodCallContext callContext);
		}
		
		public partial interface IJoinContextImpl {
			Task<global::Google.Protobuf.WellKnownTypes.Empty> JoinContext(global::Plexus.Interop.Apps.Internal.Generated.Context request, MethodCallContext callContext);
		}
		
		public partial interface IGetContextsImpl {
			Task<global::Plexus.Interop.Apps.Internal.Generated.ContextsList> GetContexts(global::Google.Protobuf.WellKnownTypes.Empty request, MethodCallContext callContext);
		}
		
		public partial interface IGetLinkedInvocationsImpl {
			Task<global::Plexus.Interop.Apps.Internal.Generated.InvocationsList> GetLinkedInvocations(global::Plexus.Interop.Apps.Internal.Generated.Context request, MethodCallContext callContext);
		}
		
		public partial interface IGetAllLinkedInvocationsImpl {
			Task<global::Plexus.Interop.Apps.Internal.Generated.ContextToInvocationsList> GetAllLinkedInvocations(global::Google.Protobuf.WellKnownTypes.Empty request, MethodCallContext callContext);
		}
		
		public sealed partial class Descriptor {
		
			public UnaryMethod<global::Google.Protobuf.WellKnownTypes.Empty, global::Plexus.Interop.Apps.Internal.Generated.Context> CreateContextMethod {get; private set; }
			public UnaryMethod<global::Plexus.Interop.Apps.Internal.Generated.Context, global::Google.Protobuf.WellKnownTypes.Empty> JoinContextMethod {get; private set; }
			public UnaryMethod<global::Google.Protobuf.WellKnownTypes.Empty, global::Plexus.Interop.Apps.Internal.Generated.ContextsList> GetContextsMethod {get; private set; }
			public UnaryMethod<global::Plexus.Interop.Apps.Internal.Generated.Context, global::Plexus.Interop.Apps.Internal.Generated.InvocationsList> GetLinkedInvocationsMethod {get; private set; }
			public UnaryMethod<global::Google.Protobuf.WellKnownTypes.Empty, global::Plexus.Interop.Apps.Internal.Generated.ContextToInvocationsList> GetAllLinkedInvocationsMethod {get; private set; }
			
			public Descriptor() {				
				CreateContextMethod = Method.Unary<global::Google.Protobuf.WellKnownTypes.Empty, global::Plexus.Interop.Apps.Internal.Generated.Context>(Id, CreateContextMethodId);
				JoinContextMethod = Method.Unary<global::Plexus.Interop.Apps.Internal.Generated.Context, global::Google.Protobuf.WellKnownTypes.Empty>(Id, JoinContextMethodId);
				GetContextsMethod = Method.Unary<global::Google.Protobuf.WellKnownTypes.Empty, global::Plexus.Interop.Apps.Internal.Generated.ContextsList>(Id, GetContextsMethodId);
				GetLinkedInvocationsMethod = Method.Unary<global::Plexus.Interop.Apps.Internal.Generated.Context, global::Plexus.Interop.Apps.Internal.Generated.InvocationsList>(Id, GetLinkedInvocationsMethodId);
				GetAllLinkedInvocationsMethod = Method.Unary<global::Google.Protobuf.WellKnownTypes.Empty, global::Plexus.Interop.Apps.Internal.Generated.ContextToInvocationsList>(Id, GetAllLinkedInvocationsMethodId);
			}
		
			public Descriptor(string alias) {
				CreateContextMethod = Method.Unary<global::Google.Protobuf.WellKnownTypes.Empty, global::Plexus.Interop.Apps.Internal.Generated.Context>(Id, alias, CreateContextMethodId);
				JoinContextMethod = Method.Unary<global::Plexus.Interop.Apps.Internal.Generated.Context, global::Google.Protobuf.WellKnownTypes.Empty>(Id, alias, JoinContextMethodId);
				GetContextsMethod = Method.Unary<global::Google.Protobuf.WellKnownTypes.Empty, global::Plexus.Interop.Apps.Internal.Generated.ContextsList>(Id, alias, GetContextsMethodId);
				GetLinkedInvocationsMethod = Method.Unary<global::Plexus.Interop.Apps.Internal.Generated.Context, global::Plexus.Interop.Apps.Internal.Generated.InvocationsList>(Id, alias, GetLinkedInvocationsMethodId);
				GetAllLinkedInvocationsMethod = Method.Unary<global::Google.Protobuf.WellKnownTypes.Empty, global::Plexus.Interop.Apps.Internal.Generated.ContextToInvocationsList>(Id, alias, GetAllLinkedInvocationsMethodId);
			}
		}
	}
					
}
#endregion Designer generated code
