using System.Net;
using System.Reflection;

namespace ET.Server
{
    [HttpHandler(SceneType.Account, "/query_info")]
    public class HttpQueryInfoHandler: IHttpHandler
    {
        public async ETTask Handle(Scene scene, HttpListenerContext context)
        {
            string sceneName = context.Request.QueryString["scene"];
            if (sceneName.IsNullOrEmpty())
            {
                HttpHelper.Response(context, "scene type is null");
                return;
            }

            string componentName = context.Request.QueryString["component"];
            if (componentName.IsNullOrEmpty())
            {
                HttpHelper.Response(context, "component name is null");
                return;
            }

            var actorId = StartSceneConfigCategory.Instance.GetBySceneName(scene.Zone(), sceneName).ActorId;
            var rep = await scene.GetComponent<MessageSender>().Call(actorId, new ConmponentQueryRequest() { ComponentName = componentName });
            if (rep == null)
            {
                HttpHelper.Response(context, "component not found");
                return;
            }

            byte[] data = (rep as ComponentQueryResponse).Entity;
            var entity = MongoHelper.Deserialize<Entity>(data);
            string field = context.Request.QueryString["field"];
            if (!field.IsNullOrEmpty())
            {
                var f = entity.GetType().GetField(field,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.NonPublic);
                if (f == null)
                {
                    HttpHelper.Response(context, "field not found");
                    return;
                }

                HttpHelper.Response(context, f.GetValue(entity));
                return;
            }

            HttpHelper.Response(context, entity);
        }
    }
}