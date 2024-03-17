using ET;
using MemoryPack;
using System.Collections.Generic;
namespace ET
{
	/// <summary>
	/// using
	/// </summary>
	[ResponseType(nameof(InnerPingResponse))]
	[Message(InnerMessage.InnerPingRequest)]
	[MemoryPackable]
	public partial class InnerPingRequest: MessageObject, IRequest
	{
		public static InnerPingRequest Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(InnerPingRequest), isFromPool) as InnerPingRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.InnerPingResponse)]
	[MemoryPackable]
	public partial class InnerPingResponse: MessageObject, IResponse
	{
		public static InnerPingResponse Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(InnerPingResponse), isFromPool) as InnerPingResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///通过名字查询组件数据
	/// </summary>
	[ResponseType(nameof(ComponentQueryResponse))]
	[Message(InnerMessage.ComponentQueryRequest)]
	[MemoryPackable]
	public partial class ComponentQueryRequest: MessageObject, IRequest
	{
		public static ComponentQueryRequest Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(ComponentQueryRequest), isFromPool) as ComponentQueryRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public string ComponentName { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.ComponentName = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.ComponentQueryResponse)]
	[MemoryPackable]
	public partial class ComponentQueryResponse: MessageObject, IResponse
	{
		public static ComponentQueryResponse Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(ComponentQueryResponse), isFromPool) as ComponentQueryResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(0)]
		public byte[] Entity { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.Entity = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///查询Unit数据
	/// </summary>
	[ResponseType(nameof(UnitQueryResponse))]
	[Message(InnerMessage.UnitQueryRequest)]
	[MemoryPackable]
	public partial class UnitQueryRequest: MessageObject, IRequest
	{
		public static UnitQueryRequest Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(UnitQueryRequest), isFromPool) as UnitQueryRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public long Id { get; set; }

		[MemoryPackOrder(1)]
		public string ComponentName { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Id = default;
			this.ComponentName = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.UnitQueryResponse)]
	[MemoryPackable]
	public partial class UnitQueryResponse: MessageObject, IResponse
	{
		public static UnitQueryResponse Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(UnitQueryResponse), isFromPool) as UnitQueryResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(0)]
		public byte[] Entity { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.Entity = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(A2M_Reload))]
	[Message(InnerMessage.M2A_Reload)]
	[MemoryPackable]
	public partial class M2A_Reload: MessageObject, IRequest
	{
		public static M2A_Reload Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2A_Reload), isFromPool) as M2A_Reload; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.A2M_Reload)]
	[MemoryPackable]
	public partial class A2M_Reload: MessageObject, IResponse
	{
		public static A2M_Reload Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(A2M_Reload), isFromPool) as A2M_Reload; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(G2G_LockResponse))]
	[Message(InnerMessage.G2G_LockRequest)]
	[MemoryPackable]
	public partial class G2G_LockRequest: MessageObject, IRequest
	{
		public static G2G_LockRequest Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(G2G_LockRequest), isFromPool) as G2G_LockRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public long Id { get; set; }

		[MemoryPackOrder(2)]
		public string Address { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Id = default;
			this.Address = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.G2G_LockResponse)]
	[MemoryPackable]
	public partial class G2G_LockResponse: MessageObject, IResponse
	{
		public static G2G_LockResponse Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(G2G_LockResponse), isFromPool) as G2G_LockResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(G2G_LockReleaseResponse))]
	[Message(InnerMessage.G2G_LockReleaseRequest)]
	[MemoryPackable]
	public partial class G2G_LockReleaseRequest: MessageObject, IRequest
	{
		public static G2G_LockReleaseRequest Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(G2G_LockReleaseRequest), isFromPool) as G2G_LockReleaseRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public long Id { get; set; }

		[MemoryPackOrder(2)]
		public string Address { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Id = default;
			this.Address = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.G2G_LockReleaseResponse)]
	[MemoryPackable]
	public partial class G2G_LockReleaseResponse: MessageObject, IResponse
	{
		public static G2G_LockReleaseResponse Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(G2G_LockReleaseResponse), isFromPool) as G2G_LockReleaseResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(ObjectAddResponse))]
	[Message(InnerMessage.ObjectAddRequest)]
	[MemoryPackable]
	public partial class ObjectAddRequest: MessageObject, IRequest
	{
		public static ObjectAddRequest Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(ObjectAddRequest), isFromPool) as ObjectAddRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public int Type { get; set; }

		[MemoryPackOrder(2)]
		public long Key { get; set; }

		[MemoryPackOrder(3)]
		public ActorId ActorId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Type = default;
			this.Key = default;
			this.ActorId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.ObjectAddResponse)]
	[MemoryPackable]
	public partial class ObjectAddResponse: MessageObject, IResponse
	{
		public static ObjectAddResponse Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(ObjectAddResponse), isFromPool) as ObjectAddResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(ObjectLockResponse))]
	[Message(InnerMessage.ObjectLockRequest)]
	[MemoryPackable]
	public partial class ObjectLockRequest: MessageObject, IRequest
	{
		public static ObjectLockRequest Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(ObjectLockRequest), isFromPool) as ObjectLockRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public int Type { get; set; }

		[MemoryPackOrder(2)]
		public long Key { get; set; }

		[MemoryPackOrder(3)]
		public ActorId ActorId { get; set; }

		[MemoryPackOrder(4)]
		public int Time { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Type = default;
			this.Key = default;
			this.ActorId = default;
			this.Time = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.ObjectLockResponse)]
	[MemoryPackable]
	public partial class ObjectLockResponse: MessageObject, IResponse
	{
		public static ObjectLockResponse Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(ObjectLockResponse), isFromPool) as ObjectLockResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(ObjectUnLockResponse))]
	[Message(InnerMessage.ObjectUnLockRequest)]
	[MemoryPackable]
	public partial class ObjectUnLockRequest: MessageObject, IRequest
	{
		public static ObjectUnLockRequest Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(ObjectUnLockRequest), isFromPool) as ObjectUnLockRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public int Type { get; set; }

		[MemoryPackOrder(2)]
		public long Key { get; set; }

		[MemoryPackOrder(3)]
		public ActorId OldActorId { get; set; }

		[MemoryPackOrder(4)]
		public ActorId NewActorId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Type = default;
			this.Key = default;
			this.OldActorId = default;
			this.NewActorId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.ObjectUnLockResponse)]
	[MemoryPackable]
	public partial class ObjectUnLockResponse: MessageObject, IResponse
	{
		public static ObjectUnLockResponse Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(ObjectUnLockResponse), isFromPool) as ObjectUnLockResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(ObjectRemoveResponse))]
	[Message(InnerMessage.ObjectRemoveRequest)]
	[MemoryPackable]
	public partial class ObjectRemoveRequest: MessageObject, IRequest
	{
		public static ObjectRemoveRequest Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(ObjectRemoveRequest), isFromPool) as ObjectRemoveRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public int Type { get; set; }

		[MemoryPackOrder(2)]
		public long Key { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Type = default;
			this.Key = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.ObjectRemoveResponse)]
	[MemoryPackable]
	public partial class ObjectRemoveResponse: MessageObject, IResponse
	{
		public static ObjectRemoveResponse Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(ObjectRemoveResponse), isFromPool) as ObjectRemoveResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(ObjectGetResponse))]
	[Message(InnerMessage.ObjectGetRequest)]
	[MemoryPackable]
	public partial class ObjectGetRequest: MessageObject, IRequest
	{
		public static ObjectGetRequest Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(ObjectGetRequest), isFromPool) as ObjectGetRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public int Type { get; set; }

		[MemoryPackOrder(2)]
		public long Key { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Type = default;
			this.Key = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.ObjectGetResponse)]
	[MemoryPackable]
	public partial class ObjectGetResponse: MessageObject, IResponse
	{
		public static ObjectGetResponse Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(ObjectGetResponse), isFromPool) as ObjectGetResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(3)]
		public int Type { get; set; }

		[MemoryPackOrder(4)]
		public ActorId ActorId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.Type = default;
			this.ActorId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(G2R_GetLoginKey))]
	[Message(InnerMessage.R2G_GetLoginKey)]
	[MemoryPackable]
	public partial class R2G_GetLoginKey: MessageObject, IRequest
	{
		public static R2G_GetLoginKey Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(R2G_GetLoginKey), isFromPool) as R2G_GetLoginKey; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public string Account { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Account = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.G2R_GetLoginKey)]
	[MemoryPackable]
	public partial class G2R_GetLoginKey: MessageObject, IResponse
	{
		public static G2R_GetLoginKey Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(G2R_GetLoginKey), isFromPool) as G2R_GetLoginKey; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(3)]
		public long Key { get; set; }

		[MemoryPackOrder(4)]
		public long GateId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.Key = default;
			this.GateId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.G2M_SessionDisconnect)]
	[MemoryPackable]
	public partial class G2M_SessionDisconnect: MessageObject, ILocationMessage
	{
		public static G2M_SessionDisconnect Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(G2M_SessionDisconnect), isFromPool) as G2M_SessionDisconnect; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(M2G_RequestEnterGameState))]
	[Message(InnerMessage.G2M_RequestEnterGameState)]
	[MemoryPackable]
	public partial class G2M_RequestEnterGameState: MessageObject, ILocationRequest
	{
		public static G2M_RequestEnterGameState Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(G2M_RequestEnterGameState), isFromPool) as G2M_RequestEnterGameState; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.M2G_RequestEnterGameState)]
	[MemoryPackable]
	public partial class M2G_RequestEnterGameState: MessageObject, ILocationResponse
	{
		public static M2G_RequestEnterGameState Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2G_RequestEnterGameState), isFromPool) as M2G_RequestEnterGameState; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(M2M_UnitTransferResponse))]
	[Message(InnerMessage.M2M_UnitTransferRequest)]
	[MemoryPackable]
	public partial class M2M_UnitTransferRequest: MessageObject, IRequest
	{
		public static M2M_UnitTransferRequest Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2M_UnitTransferRequest), isFromPool) as M2M_UnitTransferRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public ActorId OldActorId { get; set; }

		[MemoryPackOrder(2)]
		public byte[] Unit { get; set; }

		[MemoryPackOrder(3)]
		public List<byte[]> Entitys { get; set; } = new();

		[MemoryPackOrder(4)]
		public bool IsEnterGame { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.OldActorId = default;
			this.Unit = default;
			this.Entitys.Clear();
			this.IsEnterGame = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.M2M_UnitTransferResponse)]
	[MemoryPackable]
	public partial class M2M_UnitTransferResponse: MessageObject, IResponse
	{
		public static M2M_UnitTransferResponse Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2M_UnitTransferResponse), isFromPool) as M2M_UnitTransferResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///添加或更新缓存数据
	/// </summary>
	[ResponseType(nameof(Cache2Other_UpdateCache))]
	[Message(InnerMessage.Other2Cache_UpdateCache)]
	[MemoryPackable]
	public partial class Other2Cache_UpdateCache: MessageObject, IRequest
	{
		public static Other2Cache_UpdateCache Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Other2Cache_UpdateCache), isFromPool) as Other2Cache_UpdateCache; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public long UnitId { get; set; }

	/// <summary>
	///实体列表
	/// </summary>
		[MemoryPackOrder(1)]
		public List<string> EntityTypeList { get; set; } = new();

		[MemoryPackOrder(2)]
		public List<byte[]> EntityData { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.UnitId = default;
			this.EntityTypeList.Clear();
			this.EntityData.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.Cache2Other_UpdateCache)]
	[MemoryPackable]
	public partial class Cache2Other_UpdateCache: MessageObject, IResponse
	{
		public static Cache2Other_UpdateCache Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Cache2Other_UpdateCache), isFromPool) as Cache2Other_UpdateCache; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///获取缓存数据
	/// </summary>
	[ResponseType(nameof(Cache2Other_GetCache))]
	[Message(InnerMessage.Other2Cache_GetCache)]
	[MemoryPackable]
	public partial class Other2Cache_GetCache: MessageObject, IRequest
	{
		public static Other2Cache_GetCache Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Other2Cache_GetCache), isFromPool) as Other2Cache_GetCache; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public long UnitId { get; set; }

		[MemoryPackOrder(1)]
		public List<string> ComponentNameList { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.UnitId = default;
			this.ComponentNameList.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.Cache2Other_GetCache)]
	[MemoryPackable]
	public partial class Cache2Other_GetCache: MessageObject, IResponse
	{
		public static Cache2Other_GetCache Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Cache2Other_GetCache), isFromPool) as Cache2Other_GetCache; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(0)]
		public List<string> ComponentNameList { get; set; } = new();

		[MemoryPackOrder(1)]
		public List<byte[]> Entitys { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.ComponentNameList.Clear();
			this.Entitys.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///删除缓存数据
	/// </summary>
	[ResponseType(nameof(Cache2Other_DeleteCache))]
	[Message(InnerMessage.Other2Cache_DeleteCache)]
	[MemoryPackable]
	public partial class Other2Cache_DeleteCache: MessageObject, IRequest
	{
		public static Other2Cache_DeleteCache Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Other2Cache_DeleteCache), isFromPool) as Other2Cache_DeleteCache; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public long UnitId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.UnitId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.Cache2Other_DeleteCache)]
	[MemoryPackable]
	public partial class Cache2Other_DeleteCache: MessageObject, IResponse
	{
		public static Cache2Other_DeleteCache Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Cache2Other_DeleteCache), isFromPool) as Cache2Other_DeleteCache; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///玩家进入游戏
	/// </summary>
	[ResponseType(nameof(Other2G_EnterResponse))]
	[Message(InnerMessage.G2Other_EnterRequest)]
	[MemoryPackable]
	public partial class G2Other_EnterRequest: MessageObject, IRequest
	{
		public static G2Other_EnterRequest Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(G2Other_EnterRequest), isFromPool) as G2Other_EnterRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public long PlayerId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.PlayerId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.Other2G_EnterResponse)]
	[MemoryPackable]
	public partial class Other2G_EnterResponse: MessageObject, IResponse
	{
		public static Other2G_EnterResponse Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Other2G_EnterResponse), isFromPool) as Other2G_EnterResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(0)]
		public long Id { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.Id = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///玩家退出游戏
	/// </summary>
	[ResponseType(nameof(Other2G_LeaveResponse))]
	[Message(InnerMessage.G2Other_LeaveRequest)]
	[MemoryPackable]
	public partial class G2Other_LeaveRequest: MessageObject, IRequest
	{
		public static G2Other_LeaveRequest Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(G2Other_LeaveRequest), isFromPool) as G2Other_LeaveRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public long PlayerId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.PlayerId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.Other2G_LeaveResponse)]
	[MemoryPackable]
	public partial class Other2G_LeaveResponse: MessageObject, IResponse
	{
		public static Other2G_LeaveResponse Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Other2G_LeaveResponse), isFromPool) as Other2G_LeaveResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///守护进程请求保存数据
	/// </summary>
	[ResponseType(nameof(Other2W_SaveDataResponse))]
	[Message(InnerMessage.W2Other_SaveDataRequest)]
	[MemoryPackable]
	public partial class W2Other_SaveDataRequest: MessageObject, IRequest
	{
		public static W2Other_SaveDataRequest Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(W2Other_SaveDataRequest), isFromPool) as W2Other_SaveDataRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.Other2W_SaveDataResponse)]
	[MemoryPackable]
	public partial class Other2W_SaveDataResponse: MessageObject, IResponse
	{
		public static Other2W_SaveDataResponse Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Other2W_SaveDataResponse), isFromPool) as Other2W_SaveDataResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///守护进程关闭其他进程
	/// </summary>
	[ResponseType(nameof(Other2W_SaveDataResponse))]
	[Message(InnerMessage.W2Other_CloseRequest)]
	[MemoryPackable]
	public partial class W2Other_CloseRequest: MessageObject, IRequest
	{
		public static W2Other_CloseRequest Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(W2Other_CloseRequest), isFromPool) as W2Other_CloseRequest; 
		}

	/// <summary>
	///要关闭的进程ID, 小于0时全部关闭
	/// </summary>
		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public int ProcessId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.ProcessId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.W2Other_CloseResponse)]
	[MemoryPackable]
	public partial class W2Other_CloseResponse: MessageObject, IResponse
	{
		public static W2Other_CloseResponse Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(W2Other_CloseResponse), isFromPool) as W2Other_CloseResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///守护进程开启其他进程
	/// </summary>
	[ResponseType(nameof(Other2W_SaveDataResponse))]
	[Message(InnerMessage.W2Other_OpenRequest)]
	[MemoryPackable]
	public partial class W2Other_OpenRequest: MessageObject, IRequest
	{
		public static W2Other_OpenRequest Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(W2Other_OpenRequest), isFromPool) as W2Other_OpenRequest; 
		}

	/// <summary>
	///要关闭的进程ID, 小于0时全部开启
	/// </summary>
		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public int ProcessId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.ProcessId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.W2Other_OpenResponse)]
	[MemoryPackable]
	public partial class W2Other_OpenResponse: MessageObject, IResponse
	{
		public static W2Other_OpenResponse Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(W2Other_OpenResponse), isFromPool) as W2Other_OpenResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///获取开服时间
	/// </summary>
	[ResponseType(nameof(A2O_GetServerTime))]
	[Message(InnerMessage.O2A_GetServerTime)]
	[MemoryPackable]
	public partial class O2A_GetServerTime: MessageObject, IRequest
	{
		public static O2A_GetServerTime Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(O2A_GetServerTime), isFromPool) as O2A_GetServerTime; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public int ZoneId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.ZoneId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.A2O_GetServerTime)]
	[MemoryPackable]
	public partial class A2O_GetServerTime: MessageObject, IResponse
	{
		public static A2O_GetServerTime Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(A2O_GetServerTime), isFromPool) as A2O_GetServerTime; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(0)]
		public int Status { get; set; }

		[MemoryPackOrder(1)]
		public long OpenTime { get; set; }

		[MemoryPackOrder(2)]
		public long EnterTime { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.Status = default;
			this.OpenTime = default;
			this.EnterTime = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	public static class InnerMessage
	{
		 public const ushort InnerPingRequest = 20002;
		 public const ushort InnerPingResponse = 20003;
		 public const ushort ComponentQueryRequest = 20004;
		 public const ushort ComponentQueryResponse = 20005;
		 public const ushort UnitQueryRequest = 20006;
		 public const ushort UnitQueryResponse = 20007;
		 public const ushort M2A_Reload = 20008;
		 public const ushort A2M_Reload = 20009;
		 public const ushort G2G_LockRequest = 20010;
		 public const ushort G2G_LockResponse = 20011;
		 public const ushort G2G_LockReleaseRequest = 20012;
		 public const ushort G2G_LockReleaseResponse = 20013;
		 public const ushort ObjectAddRequest = 20014;
		 public const ushort ObjectAddResponse = 20015;
		 public const ushort ObjectLockRequest = 20016;
		 public const ushort ObjectLockResponse = 20017;
		 public const ushort ObjectUnLockRequest = 20018;
		 public const ushort ObjectUnLockResponse = 20019;
		 public const ushort ObjectRemoveRequest = 20020;
		 public const ushort ObjectRemoveResponse = 20021;
		 public const ushort ObjectGetRequest = 20022;
		 public const ushort ObjectGetResponse = 20023;
		 public const ushort R2G_GetLoginKey = 20024;
		 public const ushort G2R_GetLoginKey = 20025;
		 public const ushort G2M_SessionDisconnect = 20026;
		 public const ushort G2M_RequestEnterGameState = 20027;
		 public const ushort M2G_RequestEnterGameState = 20028;
		 public const ushort M2M_UnitTransferRequest = 20029;
		 public const ushort M2M_UnitTransferResponse = 20030;
		 public const ushort Other2Cache_UpdateCache = 20031;
		 public const ushort Cache2Other_UpdateCache = 20032;
		 public const ushort Other2Cache_GetCache = 20033;
		 public const ushort Cache2Other_GetCache = 20034;
		 public const ushort Other2Cache_DeleteCache = 20035;
		 public const ushort Cache2Other_DeleteCache = 20036;
		 public const ushort G2Other_EnterRequest = 20037;
		 public const ushort Other2G_EnterResponse = 20038;
		 public const ushort G2Other_LeaveRequest = 20039;
		 public const ushort Other2G_LeaveResponse = 20040;
		 public const ushort W2Other_SaveDataRequest = 20041;
		 public const ushort Other2W_SaveDataResponse = 20042;
		 public const ushort W2Other_CloseRequest = 20043;
		 public const ushort W2Other_CloseResponse = 20044;
		 public const ushort W2Other_OpenRequest = 20045;
		 public const ushort W2Other_OpenResponse = 20046;
		 public const ushort O2A_GetServerTime = 20047;
		 public const ushort A2O_GetServerTime = 20048;
	}
}
