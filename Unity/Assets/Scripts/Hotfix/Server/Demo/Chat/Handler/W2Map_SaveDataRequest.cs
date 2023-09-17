﻿namespace ET.Server;

[MessageHandler(SceneType.Chat)]
public class W2Chat_SaveDataRequest: MessageHandler<Scene, W2Other_SaveDataRequest, Other2W_SaveDataResponse>
{
    protected override async ETTask Run(Scene scene, W2Other_SaveDataRequest request, Other2W_SaveDataResponse response)
    {
        await ETTask.CompletedTask;
        Log.Error("保存聊天数据!");
    }
}