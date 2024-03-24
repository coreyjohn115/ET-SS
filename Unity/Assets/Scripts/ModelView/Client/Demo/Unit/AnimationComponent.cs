using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof (Unit))]
    public class AnimationComponent: Entity, IAwake, IDestroy
    {
        public Animation animation;

        public int lastAnimLayer = 0;
        public string lastAnimName = null;
        public int lastAnimStateFrame = 0;
        public AnimationState lastAnimState = null;
    }
}