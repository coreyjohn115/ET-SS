﻿namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class AddUpdateTask_Event: AEvent<Scene, AddUpdateTask>
    {
        protected override async ETTask Run(Scene scene, AddUpdateTask a)
        {
            await ETTask.CompletedTask;
        }
    }
}