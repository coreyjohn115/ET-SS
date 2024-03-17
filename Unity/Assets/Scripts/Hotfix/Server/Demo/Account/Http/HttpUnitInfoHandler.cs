using System.Net;

namespace ET.Server
{
    [HttpHandler(SceneType.Account, "/unit_info")]
    public class HttpUnitInfoHandler: IHttpHandler
    {
        public async ETTask Handle(Scene scene, HttpListenerContext context)
        {
            string roleId = context.Request.QueryString["Id"];
            if (roleId.IsNullOrEmpty())
            {
                HttpHelper.Response(context, "roleId is null");
                return;
            }

            string map = context.Request.QueryString["Map"];
            if (map.IsNullOrEmpty())
            {
                HttpHelper.Response(context, "map is null");
                return;
            }

            string name = context.Request.QueryString["Component"];
            var actorId = StartSceneConfigCategory.Instance.GetBySceneName(scene.Zone(), map).ActorId;
            var resp = await scene.GetComponent<MessageSender>().Call(actorId, new UnitQueryRequest() { ComponentName = name, Id = roleId.ToLong() });
            if (resp == null)
            {
                HttpHelper.Response(context, "unit not found");
                return;
            }

            byte[] data = (resp as UnitQueryResponse).Entity;
            if (data.IsNullOrEmpty())
            {
                HttpHelper.Response(context, "unit not found");
                return;
            }

            var entity = MongoHelper.Deserialize<Entity>(data);
            HttpHelper.Response(context, entity);
            await ETTask.CompletedTask;
        }
    }
}