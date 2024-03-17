using System.Collections.Generic;
using System.Net;

namespace ET.Server
{
    [HttpHandler(SceneType.Account, "/unit_list")]
    public class HttpUnitListHandler: IHttpHandler
    {
        public async ETTask Handle(Scene scene, HttpListenerContext context)
        {
            var gates = StartSceneConfigCategory.Instance.Gates[scene.Zone()];
            Dictionary<string, Entity> rDict = new Dictionary<string, Entity>();
            foreach (var gate in gates)
            {
                var resp = await scene.GetComponent<MessageSender>()
                        .Call(gate.ActorId, new ComponentQueryRequest() { ComponentName = "PlayerComponent" });
                byte[] data = (resp as ComponentQueryResponse).Entity;
                var entity = MongoHelper.Deserialize<Entity>(data);
                rDict.Add(gate.Name, entity);
            }

            HttpHelper.Response(context, rDict);
            await ETTask.CompletedTask;
        }
    }
}