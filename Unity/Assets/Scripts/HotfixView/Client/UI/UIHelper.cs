using UnityEngine;

namespace ET.Client
{
    public static class UIHelper
    {
        public static void PopMsg(Entity entity, string msg)
        {
            entity.Root().GetComponent<UIComponent>().GetDlgLogic<UIPop>().PopMsg(msg);
        }

        public static bool WorldToUI(Vector3 world, ref Vector2 ui)
        {
            Camera camera = Global.Instance.MainCamera;
            Vector3 p = camera.WorldToScreenPoint(world);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(Global.Instance.NormalRoot, p, Global.Instance.UICamera, out ui);
            return (camera.orthographic || p.z > 0) && (p.x > 0 && p.x < Screen.width && p.y > 0 && p.y < Screen.height);
        }

        public static bool WorldToWorld(Vector3 world, ref Vector3 ui)
        {
            Vector2 p = Vector2.zero;
            bool isOver = WorldToUI(world, ref p);
            ui = Global.Instance.NormalRoot.TransformPoint(p);
            return isOver;
        }

        public static Vector3 UIToScene(Vector3 position)
        {
            Vector3 src = RectTransformUtility.WorldToScreenPoint(Global.Instance.UICamera, position);
            src.z = 0;
            src.z = Mathf.Abs(Global.Instance.MainCamera.transform.position.z - position.z);
            return Global.Instance.MainCamera.ScreenToWorldPoint(src);
        }
    }
}