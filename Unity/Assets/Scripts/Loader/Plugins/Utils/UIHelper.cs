using UnityEngine;

namespace ET
{
    public static class UIHelper
    {
        public static void Normalize(this Transform self)
        {
            self.localPosition = Vector3.zero;
            self.localScale = Vector3.one;
            self.rotation = Quaternion.identity;
        }
    }
}