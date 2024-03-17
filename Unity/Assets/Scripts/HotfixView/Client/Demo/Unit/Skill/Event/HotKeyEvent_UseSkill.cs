using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class HotKeyEvent_UseSkill: AEvent<Scene, HotKeyEvent>
    {
        protected override async ETTask Run(Scene scene, HotKeyEvent e)
        {
            switch (e.Code)
            {
                case KeyCode.Alpha1:
                    // UnitHelper.GetMyUnitFromCurrentScene(scene).GetComponent<ActionComponent>().PlayAction("Buff_Speed");
                    scene.Root().GetComponent<ClientSenderComponent>().Send(new C2M_GMRequest() { Cmd = "AddBuff", Args = { "100000002", "10000" } });

                    break;
                case KeyCode.Alpha2:
                    UnitHelper.GetMyUnitFromCurrentScene(scene).GetComponent<ActionComponent>().StopAllAction();
                    break;
            }

            await ETTask.CompletedTask;
        }
    }
}