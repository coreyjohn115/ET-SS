using ET;
using MemoryPack;
using System.Collections.Generic;
namespace ET
{
	[Message(OuterMessage.HttpGetRouterResponse)]
	[MemoryPackable]
	public partial class HttpGetRouterResponse: MessageObject
	{
		public static HttpGetRouterResponse Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(HttpGetRouterResponse), isFromPool) as HttpGetRouterResponse; 
		}

		[MemoryPackOrder(0)]
		public List<string> Realms { get; set; } = new();

		[MemoryPackOrder(1)]
		public List<string> Routers { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Realms.Clear();
			this.Routers.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.RouterSync)]
	[MemoryPackable]
	public partial class RouterSync: MessageObject
	{
		public static RouterSync Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(RouterSync), isFromPool) as RouterSync; 
		}

		[MemoryPackOrder(0)]
		public uint ConnectId { get; set; }

		[MemoryPackOrder(1)]
		public string Address { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.ConnectId = default;
			this.Address = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(M2C_TestResponse))]
	[Message(OuterMessage.C2M_TestRequest)]
	[MemoryPackable]
	public partial class C2M_TestRequest: MessageObject, ILocationRequest
	{
		public static C2M_TestRequest Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2M_TestRequest), isFromPool) as C2M_TestRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public string request { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.request = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.M2C_TestResponse)]
	[MemoryPackable]
	public partial class M2C_TestResponse: MessageObject, IResponse
	{
		public static M2C_TestResponse Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2C_TestResponse), isFromPool) as M2C_TestResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(3)]
		public string response { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.response = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	/// 进入地图
	/// </summary>
	[ResponseType(nameof(G2C_EnterMap))]
	[Message(OuterMessage.C2G_EnterMap)]
	[MemoryPackable]
	public partial class C2G_EnterMap: MessageObject, ISessionRequest
	{
		public static C2G_EnterMap Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2G_EnterMap), isFromPool) as C2G_EnterMap; 
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

	[Message(OuterMessage.G2C_EnterMap)]
	[MemoryPackable]
	public partial class G2C_EnterMap: MessageObject, ISessionResponse
	{
		public static G2C_EnterMap Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(G2C_EnterMap), isFromPool) as G2C_EnterMap; 
		}

	/// <summary>
	/// 自己unitId
	/// </summary>
		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(3)]
		public long MyId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.MyId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.MoveInfo)]
	[MemoryPackable]
	public partial class MoveInfo: MessageObject
	{
		public static MoveInfo Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(MoveInfo), isFromPool) as MoveInfo; 
		}

		[MemoryPackOrder(0)]
		public List<Unity.Mathematics.float3> Points { get; set; } = new();

		[MemoryPackOrder(1)]
		public Unity.Mathematics.quaternion Rotation { get; set; }

		[MemoryPackOrder(2)]
		public int TurnSpeed { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Points.Clear();
			this.Rotation = default;
			this.TurnSpeed = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.UnitInfo)]
	[MemoryPackable]
	public partial class UnitInfo: MessageObject
	{
		public static UnitInfo Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(UnitInfo), isFromPool) as UnitInfo; 
		}

		[MemoryPackOrder(0)]
		public long UnitId { get; set; }

		[MemoryPackOrder(1)]
		public int ConfigId { get; set; }

		[MemoryPackOrder(2)]
		public int Type { get; set; }

		[MemoryPackOrder(3)]
		public Unity.Mathematics.float3 Position { get; set; }

		[MemoryPackOrder(4)]
		public Unity.Mathematics.float3 Forward { get; set; }

		[MongoDB.Bson.Serialization.Attributes.BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
		[MemoryPackOrder(5)]
		public Dictionary<int, long> KV { get; set; } = new();
		[MemoryPackOrder(6)]
		public MoveInfo MoveInfo { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.UnitId = default;
			this.ConfigId = default;
			this.Type = default;
			this.Position = default;
			this.Forward = default;
			this.KV.Clear();
			this.MoveInfo = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	/// 创建对象列表
	/// </summary>
	[Message(OuterMessage.M2C_CreateUnits)]
	[MemoryPackable]
	public partial class M2C_CreateUnits: MessageObject, IMessage
	{
		public static M2C_CreateUnits Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2C_CreateUnits), isFromPool) as M2C_CreateUnits; 
		}

		[MemoryPackOrder(0)]
		public List<UnitInfo> Units { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Units.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	/// 删除对象列表
	/// </summary>
	[Message(OuterMessage.M2C_RemoveUnits)]
	[MemoryPackable]
	public partial class M2C_RemoveUnits: MessageObject, IMessage
	{
		public static M2C_RemoveUnits Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2C_RemoveUnits), isFromPool) as M2C_RemoveUnits; 
		}

		[MemoryPackOrder(0)]
		public List<long> Units { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Units.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	/// 创建主玩家
	/// </summary>
	[Message(OuterMessage.M2C_CreateMyUnit)]
	[MemoryPackable]
	public partial class M2C_CreateMyUnit: MessageObject, IMessage
	{
		public static M2C_CreateMyUnit Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2C_CreateMyUnit), isFromPool) as M2C_CreateMyUnit; 
		}

		[MemoryPackOrder(0)]
		public UnitInfo Unit { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Unit = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	/// 开始切换场景
	/// </summary>
	[Message(OuterMessage.M2C_StartSceneChange)]
	[MemoryPackable]
	public partial class M2C_StartSceneChange: MessageObject, IMessage
	{
		public static M2C_StartSceneChange Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2C_StartSceneChange), isFromPool) as M2C_StartSceneChange; 
		}

		[MemoryPackOrder(0)]
		public long SceneInstanceId { get; set; }

		[MemoryPackOrder(1)]
		public string SceneName { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.SceneInstanceId = default;
			this.SceneName = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	/// 寻路请求
	/// </summary>
	[Message(OuterMessage.C2M_PathfindingResult)]
	[MemoryPackable]
	public partial class C2M_PathfindingResult: MessageObject, ILocationMessage
	{
		public static C2M_PathfindingResult Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2M_PathfindingResult), isFromPool) as C2M_PathfindingResult; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public Unity.Mathematics.float3 Position { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Position = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	/// 停止移动
	/// </summary>
	[Message(OuterMessage.C2M_Stop)]
	[MemoryPackable]
	public partial class C2M_Stop: MessageObject, ILocationMessage
	{
		public static C2M_Stop Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2M_Stop), isFromPool) as C2M_Stop; 
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

	/// <summary>
	/// 寻路广播
	/// </summary>
	[Message(OuterMessage.M2C_PathfindingResult)]
	[MemoryPackable]
	public partial class M2C_PathfindingResult: MessageObject, IMessage
	{
		public static M2C_PathfindingResult Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2C_PathfindingResult), isFromPool) as M2C_PathfindingResult; 
		}

		[MemoryPackOrder(0)]
		public long Id { get; set; }

		[MemoryPackOrder(1)]
		public Unity.Mathematics.float3 Position { get; set; }

		[MemoryPackOrder(2)]
		public List<Unity.Mathematics.float3> Points { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Id = default;
			this.Position = default;
			this.Points.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	/// 停止移动
	/// </summary>
	[Message(OuterMessage.M2C_Stop)]
	[MemoryPackable]
	public partial class M2C_Stop: MessageObject, IMessage
	{
		public static M2C_Stop Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2C_Stop), isFromPool) as M2C_Stop; 
		}

		[MemoryPackOrder(0)]
		public int Error { get; set; }

		[MemoryPackOrder(1)]
		public long Id { get; set; }

		[MemoryPackOrder(2)]
		public Unity.Mathematics.float3 Position { get; set; }

		[MemoryPackOrder(3)]
		public Unity.Mathematics.quaternion Rotation { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Error = default;
			this.Id = default;
			this.Position = default;
			this.Rotation = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(G2C_Ping))]
	[Message(OuterMessage.C2G_Ping)]
	[MemoryPackable]
	public partial class C2G_Ping: MessageObject, ISessionRequest
	{
		public static C2G_Ping Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2G_Ping), isFromPool) as C2G_Ping; 
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

	[Message(OuterMessage.G2C_Ping)]
	[MemoryPackable]
	public partial class G2C_Ping: MessageObject, ISessionResponse
	{
		public static G2C_Ping Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(G2C_Ping), isFromPool) as G2C_Ping; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(3)]
		public long Time { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.Time = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.G2C_Test)]
	[MemoryPackable]
	public partial class G2C_Test: MessageObject, ISessionMessage
	{
		public static G2C_Test Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(G2C_Test), isFromPool) as G2C_Test; 
		}

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(M2C_Reload))]
	[Message(OuterMessage.C2M_Reload)]
	[MemoryPackable]
	public partial class C2M_Reload: MessageObject, ISessionRequest
	{
		public static C2M_Reload Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2M_Reload), isFromPool) as C2M_Reload; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public string Account { get; set; }

		[MemoryPackOrder(2)]
		public string Password { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Account = default;
			this.Password = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.M2C_Reload)]
	[MemoryPackable]
	public partial class M2C_Reload: MessageObject, ISessionResponse
	{
		public static M2C_Reload Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2C_Reload), isFromPool) as M2C_Reload; 
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

	[ResponseType(nameof(R2C_Login))]
	[Message(OuterMessage.C2R_Login)]
	[MemoryPackable]
	public partial class C2R_Login: MessageObject, ISessionRequest
	{
		public static C2R_Login Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2R_Login), isFromPool) as C2R_Login; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public string Account { get; set; }

		[MemoryPackOrder(2)]
		public string Password { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Account = default;
			this.Password = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.R2C_Login)]
	[MemoryPackable]
	public partial class R2C_Login: MessageObject, ISessionResponse
	{
		public static R2C_Login Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(R2C_Login), isFromPool) as R2C_Login; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(3)]
		public string Address { get; set; }

		[MemoryPackOrder(4)]
		public long Key { get; set; }

		[MemoryPackOrder(5)]
		public long GateId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.Address = default;
			this.Key = default;
			this.GateId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(G2C_LoginGate))]
	[Message(OuterMessage.C2G_LoginGate)]
	[MemoryPackable]
	public partial class C2G_LoginGate: MessageObject, ISessionRequest
	{
		public static C2G_LoginGate Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2G_LoginGate), isFromPool) as C2G_LoginGate; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public long Id { get; set; }

		[MemoryPackOrder(1)]
		public long Key { get; set; }

		[MemoryPackOrder(2)]
		public long GateId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Id = default;
			this.Key = default;
			this.GateId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.G2C_LoginGate)]
	[MemoryPackable]
	public partial class G2C_LoginGate: MessageObject, ISessionResponse
	{
		public static G2C_LoginGate Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(G2C_LoginGate), isFromPool) as G2C_LoginGate; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(3)]
		public long PlayerId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.PlayerId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.AccountProto)]
	[MemoryPackable]
	public partial class AccountProto: MessageObject
	{
		public static AccountProto Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(AccountProto), isFromPool) as AccountProto; 
		}

		[MemoryPackOrder(-1)]
		public long Id { get; set; }

		[MemoryPackOrder(1)]
		public int AccountType { get; set; }

		[MemoryPackOrder(2)]
		public long CreateTime { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Id = default;
			this.AccountType = default;
			this.CreateTime = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.HttpAccount)]
	[MemoryPackable]
	public partial class HttpAccount: MessageObject
	{
		public static HttpAccount Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(HttpAccount), isFromPool) as HttpAccount; 
		}

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(0)]
		public AccountProto Account { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Error = default;
			this.Account = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.ServerInfoProto)]
	[MemoryPackable]
	public partial class ServerInfoProto: MessageObject
	{
		public static ServerInfoProto Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(ServerInfoProto), isFromPool) as ServerInfoProto; 
		}

		[MemoryPackOrder(0)]
		public long Id { get; set; }

		[MemoryPackOrder(1)]
		public int Status { get; set; }

		[MemoryPackOrder(2)]
		public string ServerName { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Id = default;
			this.Status = default;
			this.ServerName = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.HttpServerList)]
	[MemoryPackable]
	public partial class HttpServerList: MessageObject
	{
		public static HttpServerList Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(HttpServerList), isFromPool) as HttpServerList; 
		}

		[MemoryPackOrder(0)]
		public List<ServerInfoProto> ServerList { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.ServerList.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.RoleInfoProto)]
	[MemoryPackable]
	public partial class RoleInfoProto: MessageObject
	{
		public static RoleInfoProto Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(RoleInfoProto), isFromPool) as RoleInfoProto; 
		}

		[MemoryPackOrder(0)]
		public long Id { get; set; }

		[MemoryPackOrder(1)]
		public string Name { get; set; }

		[MemoryPackOrder(2)]
		public int State { get; set; }

		[MemoryPackOrder(3)]
		public string Account { get; set; }

		[MemoryPackOrder(4)]
		public long LastLoginTime { get; set; }

		[MemoryPackOrder(5)]
		public long CreateTime { get; set; }

		[MemoryPackOrder(6)]
		public int ServerId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Id = default;
			this.Name = default;
			this.State = default;
			this.Account = default;
			this.LastLoginTime = default;
			this.CreateTime = default;
			this.ServerId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.HttpRoleList)]
	[MemoryPackable]
	public partial class HttpRoleList: MessageObject
	{
		public static HttpRoleList Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(HttpRoleList), isFromPool) as HttpRoleList; 
		}

		[MemoryPackOrder(0)]
		public List<RoleInfoProto> RoleList { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RoleList.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///创建角色
	/// </summary>
	[ResponseType(nameof(A2C_CreateRole))]
	[Message(OuterMessage.C2A_CreateRole)]
	[MemoryPackable]
	public partial class C2A_CreateRole: MessageObject, ISessionRequest
	{
		public static C2A_CreateRole Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2A_CreateRole), isFromPool) as C2A_CreateRole; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public string Token { get; set; }

		[MemoryPackOrder(1)]
		public string Account { get; set; }

		[MemoryPackOrder(2)]
		public string Name { get; set; }

		[MemoryPackOrder(3)]
		public int ServerId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Token = default;
			this.Account = default;
			this.Name = default;
			this.ServerId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.A2C_CreateRole)]
	[MemoryPackable]
	public partial class A2C_CreateRole: MessageObject, ISessionResponse
	{
		public static A2C_CreateRole Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(A2C_CreateRole), isFromPool) as A2C_CreateRole; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(0)]
		public RoleInfoProto RoleInfo { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.RoleInfo = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///获取角色列表
	/// </summary>
	[ResponseType(nameof(A2C_GetRoles))]
	[Message(OuterMessage.C2A_GetRoles)]
	[MemoryPackable]
	public partial class C2A_GetRoles: MessageObject, ISessionRequest
	{
		public static C2A_GetRoles Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2A_GetRoles), isFromPool) as C2A_GetRoles; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public string Token { get; set; }

		[MemoryPackOrder(1)]
		public string Account { get; set; }

		[MemoryPackOrder(2)]
		public int ServerId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Token = default;
			this.Account = default;
			this.ServerId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.A2C_GetRoles)]
	[MemoryPackable]
	public partial class A2C_GetRoles: MessageObject, ISessionResponse
	{
		public static A2C_GetRoles Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(A2C_GetRoles), isFromPool) as A2C_GetRoles; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(0)]
		public List<RoleInfoProto> RoleInfo { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.RoleInfo.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///删除角色
	/// </summary>
	[ResponseType(nameof(A2C_DeleteRole))]
	[Message(OuterMessage.C2A_DeleteRole)]
	[MemoryPackable]
	public partial class C2A_DeleteRole: MessageObject, ISessionRequest
	{
		public static C2A_DeleteRole Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2A_DeleteRole), isFromPool) as C2A_DeleteRole; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public string Token { get; set; }

		[MemoryPackOrder(1)]
		public string Account { get; set; }

		[MemoryPackOrder(2)]
		public long RoleInfoId { get; set; }

		[MemoryPackOrder(3)]
		public int ServerId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Token = default;
			this.Account = default;
			this.RoleInfoId = default;
			this.ServerId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.A2C_DeleteRole)]
	[MemoryPackable]
	public partial class A2C_DeleteRole: MessageObject, ISessionResponse
	{
		public static A2C_DeleteRole Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(A2C_DeleteRole), isFromPool) as A2C_DeleteRole; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(0)]
		public long DeletedRoleInfoId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.DeletedRoleInfoId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.G2C_TestHotfixMessage)]
	[MemoryPackable]
	public partial class G2C_TestHotfixMessage: MessageObject, ISessionMessage
	{
		public static G2C_TestHotfixMessage Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(G2C_TestHotfixMessage), isFromPool) as G2C_TestHotfixMessage; 
		}

		[MemoryPackOrder(0)]
		public string Info { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Info = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(M2C_TestRobotCase))]
	[Message(OuterMessage.C2M_TestRobotCase)]
	[MemoryPackable]
	public partial class C2M_TestRobotCase: MessageObject, ILocationRequest
	{
		public static C2M_TestRobotCase Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2M_TestRobotCase), isFromPool) as C2M_TestRobotCase; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public int N { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.N = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.M2C_TestRobotCase)]
	[MemoryPackable]
	public partial class M2C_TestRobotCase: MessageObject, ILocationResponse
	{
		public static M2C_TestRobotCase Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2C_TestRobotCase), isFromPool) as M2C_TestRobotCase; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(3)]
		public int N { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.N = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.C2M_TestRobotCase2)]
	[MemoryPackable]
	public partial class C2M_TestRobotCase2: MessageObject, ILocationMessage
	{
		public static C2M_TestRobotCase2 Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2M_TestRobotCase2), isFromPool) as C2M_TestRobotCase2; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public int N { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.N = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.M2C_TestRobotCase2)]
	[MemoryPackable]
	public partial class M2C_TestRobotCase2: MessageObject, ILocationMessage
	{
		public static M2C_TestRobotCase2 Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2C_TestRobotCase2), isFromPool) as M2C_TestRobotCase2; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public int N { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.N = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(M2C_TransferMap))]
	[Message(OuterMessage.C2M_TransferMap)]
	[MemoryPackable]
	public partial class C2M_TransferMap: MessageObject, ILocationRequest
	{
		public static C2M_TransferMap Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2M_TransferMap), isFromPool) as C2M_TransferMap; 
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

	[Message(OuterMessage.M2C_TransferMap)]
	[MemoryPackable]
	public partial class M2C_TransferMap: MessageObject, ILocationResponse
	{
		public static M2C_TransferMap Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2C_TransferMap), isFromPool) as M2C_TransferMap; 
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

	[ResponseType(nameof(G2C_Benchmark))]
	[Message(OuterMessage.C2G_Benchmark)]
	[MemoryPackable]
	public partial class C2G_Benchmark: MessageObject, ISessionRequest
	{
		public static C2G_Benchmark Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2G_Benchmark), isFromPool) as C2G_Benchmark; 
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

	[Message(OuterMessage.G2C_Benchmark)]
	[MemoryPackable]
	public partial class G2C_Benchmark: MessageObject, ISessionResponse
	{
		public static G2C_Benchmark Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(G2C_Benchmark), isFromPool) as G2C_Benchmark; 
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
	///受伤信息
	/// </summary>
	[Message(OuterMessage.HurtInfo)]
	[MemoryPackable]
	public partial class HurtInfo: MessageObject
	{
		public static HurtInfo Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(HurtInfo), isFromPool) as HurtInfo; 
		}

	/// <summary>
	///受伤者id
	/// </summary>
		[MemoryPackOrder(0)]
		public long Id { get; set; }

	/// <summary>
	///伤害值
	/// </summary>
		[MemoryPackOrder(1)]
		public long Hurt { get; set; }

	/// <summary>
	///吸血值
	/// </summary>
		[MemoryPackOrder(2)]
		public long SuckHp { get; set; }

	/// <summary>
	///反伤
	/// </summary>
		[MemoryPackOrder(3)]
		public long BackHurt { get; set; }

	/// <summary>
	///是否暴击
	/// </summary>
		[MemoryPackOrder(4)]
		public bool IsCrit { get; set; }

	/// <summary>
	///是否闪避
	/// </summary>
		[MemoryPackOrder(5)]
		public bool IsMiss { get; set; }

	/// <summary>
	///是否格挡
	/// </summary>
		[MemoryPackOrder(6)]
		public bool IsFender { get; set; }

	/// <summary>
	///是否是加血
	/// </summary>
		[MemoryPackOrder(7)]
		public bool IsAddHp { get; set; }

	/// <summary>
	///是否免疫
	/// </summary>
		[MemoryPackOrder(8)]
		public bool IsImmUnity { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Id = default;
			this.Hurt = default;
			this.SuckHp = default;
			this.BackHurt = default;
			this.IsCrit = default;
			this.IsMiss = default;
			this.IsFender = default;
			this.IsAddHp = default;
			this.IsImmUnity = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///更新护盾值
	/// </summary>
	[Message(OuterMessage.M2C_UpdateUnitShield)]
	[MemoryPackable]
	public partial class M2C_UpdateUnitShield: MessageObject, IMessage
	{
		public static M2C_UpdateUnitShield Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2C_UpdateUnitShield), isFromPool) as M2C_UpdateUnitShield; 
		}

		[MemoryPackOrder(0)]
		public long Id { get; set; }

		[MongoDB.Bson.Serialization.Attributes.BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
		[MemoryPackOrder(1)]
		public Dictionary<int, long> KV { get; set; } = new();
		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Id = default;
			this.KV.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///任务
	/// </summary>
	[Message(OuterMessage.TaskProto)]
	[MemoryPackable]
	public partial class TaskProto: MessageObject
	{
		public static TaskProto Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(TaskProto), isFromPool) as TaskProto; 
		}

		[MemoryPackOrder(0)]
		public int Id { get; set; }

		[MemoryPackOrder(1)]
		public List<long> Args { get; set; } = new();

		[MemoryPackOrder(2)]
		public long Min { get; set; }

		[MemoryPackOrder(3)]
		public long Max { get; set; }

		[MemoryPackOrder(4)]
		public int Status { get; set; }

		[MemoryPackOrder(5)]
		public long Time { get; set; }

		[MemoryPackOrder(6)]
		public long AcceptTime { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Id = default;
			this.Args.Clear();
			this.Min = default;
			this.Max = default;
			this.Status = default;
			this.Time = default;
			this.AcceptTime = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	/// 更新任务
	/// </summary>
	[Message(OuterMessage.M2C_UpdateTask)]
	[MemoryPackable]
	public partial class M2C_UpdateTask: MessageObject, IMessage
	{
		public static M2C_UpdateTask Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2C_UpdateTask), isFromPool) as M2C_UpdateTask; 
		}

		[MemoryPackOrder(0)]
		public List<TaskProto> List { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.List.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	/// 删除任务
	/// </summary>
	[Message(OuterMessage.M2C_DeleteTask)]
	[MemoryPackable]
	public partial class M2C_DeleteTask: MessageObject, IMessage
	{
		public static M2C_DeleteTask Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2C_DeleteTask), isFromPool) as M2C_DeleteTask; 
		}

		[MemoryPackOrder(0)]
		public List<int> List { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.List.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	/// 提交任务
	/// </summary>
	[ResponseType(nameof(M2C_CommitTask))]
	[Message(OuterMessage.C2M_CommitTask)]
	[MemoryPackable]
	public partial class C2M_CommitTask: MessageObject, ILocationRequest
	{
		public static C2M_CommitTask Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2M_CommitTask), isFromPool) as C2M_CommitTask; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public int Id { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Id = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.M2C_CommitTask)]
	[MemoryPackable]
	public partial class M2C_CommitTask: MessageObject, ILocationResponse
	{
		public static M2C_CommitTask Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2C_CommitTask), isFromPool) as M2C_CommitTask; 
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
	///道具
	/// </summary>
	[Message(OuterMessage.ItemProto)]
	[MemoryPackable]
	public partial class ItemProto: MessageObject
	{
		public static ItemProto Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(ItemProto), isFromPool) as ItemProto; 
		}

		[MemoryPackOrder(0)]
		public int Id { get; set; }

		[MemoryPackOrder(1)]
		public long Count { get; set; }

		[MemoryPackOrder(2)]
		public long ValidTime { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Id = default;
			this.Count = default;
			this.ValidTime = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	/// 更新道具
	/// </summary>
	[Message(OuterMessage.M2C_UpdateItem)]
	[MemoryPackable]
	public partial class M2C_UpdateItem: MessageObject, IMessage
	{
		public static M2C_UpdateItem Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2C_UpdateItem), isFromPool) as M2C_UpdateItem; 
		}

		[MemoryPackOrder(0)]
		public List<ItemProto> List { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.List.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	/// 使用道具
	/// </summary>
	[ResponseType(nameof(M2C_UseItem))]
	[Message(OuterMessage.C2M_UseItem)]
	[MemoryPackable]
	public partial class C2M_UseItem: MessageObject, ILocationRequest
	{
		public static C2M_UseItem Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2M_UseItem), isFromPool) as C2M_UseItem; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public int Id { get; set; }

		[MemoryPackOrder(1)]
		public long Count { get; set; }

		[MemoryPackOrder(2)]
		public List<string> Args { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Id = default;
			this.Count = default;
			this.Args.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.M2C_UseItem)]
	[MemoryPackable]
	public partial class M2C_UseItem: MessageObject, ILocationResponse
	{
		public static M2C_UseItem Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2C_UseItem), isFromPool) as M2C_UseItem; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(2)]
		public List<string> RetArgs { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.RetArgs.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	/// 删除道具
	/// </summary>
	[ResponseType(nameof(M2C_DeleteItem))]
	[Message(OuterMessage.C2M_DeleteItem)]
	[MemoryPackable]
	public partial class C2M_DeleteItem: MessageObject, ILocationRequest
	{
		public static C2M_DeleteItem Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2M_DeleteItem), isFromPool) as C2M_DeleteItem; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public List<ItemProto> List { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.List.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.M2C_DeleteItem)]
	[MemoryPackable]
	public partial class M2C_DeleteItem: MessageObject, ILocationResponse
	{
		public static M2C_DeleteItem Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2C_DeleteItem), isFromPool) as M2C_DeleteItem; 
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
	/// 请求玩家数据
	/// </summary>
	[ResponseType(nameof(M2C_GetPlayerData))]
	[Message(OuterMessage.C2M_GetPlayerData)]
	[MemoryPackable]
	public partial class C2M_GetPlayerData: MessageObject, ILocationRequest
	{
		public static C2M_GetPlayerData Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2M_GetPlayerData), isFromPool) as C2M_GetPlayerData; 
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

	[Message(OuterMessage.M2C_GetPlayerData)]
	[MemoryPackable]
	public partial class M2C_GetPlayerData: MessageObject, ILocationResponse
	{
		public static M2C_GetPlayerData Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(M2C_GetPlayerData), isFromPool) as M2C_GetPlayerData; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(0)]
		public List<ItemProto> ItemList { get; set; } = new();

		[MemoryPackOrder(1)]
		public List<TaskProto> TaskList { get; set; } = new();

		[MongoDB.Bson.Serialization.Attributes.BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
		[MemoryPackOrder(2)]
		public Dictionary<int, long> FinishDict { get; set; } = new();
		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.ItemList.Clear();
			this.TaskList.Clear();
			this.FinishDict.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.RankRoleInfoProto)]
	[MemoryPackable]
	public partial class RankRoleInfoProto: MessageObject
	{
		public static RankRoleInfoProto Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(RankRoleInfoProto), isFromPool) as RankRoleInfoProto; 
		}

		[MemoryPackOrder(0)]
		public string Name { get; set; }

		[MemoryPackOrder(1)]
		public string HeadIcon { get; set; }

		[MemoryPackOrder(2)]
		public int Level { get; set; }

		[MemoryPackOrder(3)]
		public long Fight { get; set; }

		[MemoryPackOrder(4)]
		public int Sex { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Name = default;
			this.HeadIcon = default;
			this.Level = default;
			this.Fight = default;
			this.Sex = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///排行榜显示信息
	/// </summary>
	[Message(OuterMessage.RankInfoProto)]
	[MemoryPackable]
	public partial class RankInfoProto: MessageObject
	{
		public static RankInfoProto Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(RankInfoProto), isFromPool) as RankInfoProto; 
		}

		[MemoryPackOrder(0)]
		public long UnitId { get; set; }

	/// <summary>
	///分数
	/// </summary>
		[MemoryPackOrder(1)]
		public long Score { get; set; }

	/// <summary>
	///排名
	/// </summary>
		[MemoryPackOrder(2)]
		public long Rank { get; set; }

	/// <summary>
	///时间
	/// </summary>
		[MemoryPackOrder(3)]
		public long Time { get; set; }

	/// <summary>
	///角色信息
	/// </summary>
		[MemoryPackOrder(4)]
		public RankRoleInfoProto RoleInfo { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.UnitId = default;
			this.Score = default;
			this.Rank = default;
			this.Time = default;
			this.RoleInfo = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///获取排行榜数据
	/// </summary>
	[ResponseType(nameof(Ran2C_GetRankResponse))]
	[Message(OuterMessage.C2Rank_GetRankRequest)]
	[MemoryPackable]
	public partial class C2Rank_GetRankRequest: MessageObject, IRankRequest
	{
		public static C2Rank_GetRankRequest Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2Rank_GetRankRequest), isFromPool) as C2Rank_GetRankRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public long UnitId { get; set; }

		[MemoryPackOrder(1)]
		public int Type { get; set; }

		[MemoryPackOrder(2)]
		public int SubType { get; set; }

		[MemoryPackOrder(3)]
		public int Page { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.UnitId = default;
			this.Type = default;
			this.SubType = default;
			this.Page = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.Ran2C_GetRankResponse)]
	[MemoryPackable]
	public partial class Ran2C_GetRankResponse: MessageObject, IRankResponse
	{
		public static Ran2C_GetRankResponse Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Ran2C_GetRankResponse), isFromPool) as Ran2C_GetRankResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(0)]
		public List<RankInfoProto> List { get; set; } = new();

	/// <summary>
	///自己的数据
	/// </summary>
		[MemoryPackOrder(1)]
		public RankInfoProto SelfInfo { get; set; }

		[MemoryPackOrder(2)]
		public int Page { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.List.Clear();
			this.SelfInfo = default;
			this.Page = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.PlayerInfoProto)]
	[MemoryPackable]
	public partial class PlayerInfoProto: MessageObject
	{
		public static PlayerInfoProto Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(PlayerInfoProto), isFromPool) as PlayerInfoProto; 
		}

		[MemoryPackOrder(5)]
		public long UnitId { get; set; }

		[MemoryPackOrder(0)]
		public string Name { get; set; }

		[MemoryPackOrder(1)]
		public string HeadIcon { get; set; }

		[MemoryPackOrder(2)]
		public int Level { get; set; }

		[MemoryPackOrder(3)]
		public long Fight { get; set; }

		[MemoryPackOrder(4)]
		public int Sex { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.UnitId = default;
			this.Name = default;
			this.HeadIcon = default;
			this.Level = default;
			this.Fight = default;
			this.Sex = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///发送聊天消息
	/// </summary>
	[ResponseType(nameof(Chat2C_SendResponse))]
	[Message(OuterMessage.C2Chat_SendRequest)]
	[MemoryPackable]
	public partial class C2Chat_SendRequest: MessageObject, IChatRequest
	{
		public static C2Chat_SendRequest Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2Chat_SendRequest), isFromPool) as C2Chat_SendRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public int Channel { get; set; }

		[MemoryPackOrder(1)]
		public PlayerInfoProto RoleInfo { get; set; }

		[MemoryPackOrder(2)]
		public string Message { get; set; }

		[MemoryPackOrder(3)]
		public long GroupId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Channel = default;
			this.RoleInfo = default;
			this.Message = default;
			this.GroupId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(OuterMessage.Chat2C_SendResponse)]
	[MemoryPackable]
	public partial class Chat2C_SendResponse: MessageObject, IChatResponse
	{
		public static Chat2C_SendResponse Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Chat2C_SendResponse), isFromPool) as Chat2C_SendResponse; 
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
	///收到聊天信息
	/// </summary>
	[Message(OuterMessage.Chat2C_UpdateChat)]
	[MemoryPackable]
	public partial class Chat2C_UpdateChat: MessageObject, IChatMessage
	{
		public static Chat2C_UpdateChat Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Chat2C_UpdateChat), isFromPool) as Chat2C_UpdateChat; 
		}

		[MemoryPackOrder(0)]
		public int Channel { get; set; }

		[MemoryPackOrder(1)]
		public PlayerInfoProto RoleInfo { get; set; }

		[MemoryPackOrder(2)]
		public string Message { get; set; }

		[MemoryPackOrder(3)]
		public long GroupId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Channel = default;
			this.RoleInfo = default;
			this.Message = default;
			this.GroupId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	public static class OuterMessage
	{
		 public const ushort HttpGetRouterResponse = 10002;
		 public const ushort RouterSync = 10003;
		 public const ushort C2M_TestRequest = 10004;
		 public const ushort M2C_TestResponse = 10005;
		 public const ushort C2G_EnterMap = 10006;
		 public const ushort G2C_EnterMap = 10007;
		 public const ushort MoveInfo = 10008;
		 public const ushort UnitInfo = 10009;
		 public const ushort M2C_CreateUnits = 10010;
		 public const ushort M2C_RemoveUnits = 10011;
		 public const ushort M2C_CreateMyUnit = 10012;
		 public const ushort M2C_StartSceneChange = 10013;
		 public const ushort C2M_PathfindingResult = 10014;
		 public const ushort C2M_Stop = 10015;
		 public const ushort M2C_PathfindingResult = 10016;
		 public const ushort M2C_Stop = 10017;
		 public const ushort C2G_Ping = 10018;
		 public const ushort G2C_Ping = 10019;
		 public const ushort G2C_Test = 10020;
		 public const ushort C2M_Reload = 10021;
		 public const ushort M2C_Reload = 10022;
		 public const ushort C2R_Login = 10023;
		 public const ushort R2C_Login = 10024;
		 public const ushort C2G_LoginGate = 10025;
		 public const ushort G2C_LoginGate = 10026;
		 public const ushort AccountProto = 10027;
		 public const ushort HttpAccount = 10028;
		 public const ushort ServerInfoProto = 10029;
		 public const ushort HttpServerList = 10030;
		 public const ushort RoleInfoProto = 10031;
		 public const ushort HttpRoleList = 10032;
		 public const ushort C2A_CreateRole = 10033;
		 public const ushort A2C_CreateRole = 10034;
		 public const ushort C2A_GetRoles = 10035;
		 public const ushort A2C_GetRoles = 10036;
		 public const ushort C2A_DeleteRole = 10037;
		 public const ushort A2C_DeleteRole = 10038;
		 public const ushort G2C_TestHotfixMessage = 10039;
		 public const ushort C2M_TestRobotCase = 10040;
		 public const ushort M2C_TestRobotCase = 10041;
		 public const ushort C2M_TestRobotCase2 = 10042;
		 public const ushort M2C_TestRobotCase2 = 10043;
		 public const ushort C2M_TransferMap = 10044;
		 public const ushort M2C_TransferMap = 10045;
		 public const ushort C2G_Benchmark = 10046;
		 public const ushort G2C_Benchmark = 10047;
		 public const ushort HurtInfo = 10048;
		 public const ushort M2C_UpdateUnitShield = 10049;
		 public const ushort TaskProto = 10050;
		 public const ushort M2C_UpdateTask = 10051;
		 public const ushort M2C_DeleteTask = 10052;
		 public const ushort C2M_CommitTask = 10053;
		 public const ushort M2C_CommitTask = 10054;
		 public const ushort ItemProto = 10055;
		 public const ushort M2C_UpdateItem = 10056;
		 public const ushort C2M_UseItem = 10057;
		 public const ushort M2C_UseItem = 10058;
		 public const ushort C2M_DeleteItem = 10059;
		 public const ushort M2C_DeleteItem = 10060;
		 public const ushort C2M_GetPlayerData = 10061;
		 public const ushort M2C_GetPlayerData = 10062;
		 public const ushort RankRoleInfoProto = 10063;
		 public const ushort RankInfoProto = 10064;
		 public const ushort C2Rank_GetRankRequest = 10065;
		 public const ushort Ran2C_GetRankResponse = 10066;
		 public const ushort PlayerInfoProto = 10067;
		 public const ushort C2Chat_SendRequest = 10068;
		 public const ushort Chat2C_SendResponse = 10069;
		 public const ushort Chat2C_UpdateChat = 10070;
	}
}