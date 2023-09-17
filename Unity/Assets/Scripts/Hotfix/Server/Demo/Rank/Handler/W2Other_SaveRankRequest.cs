﻿namespace ET.Server;

[MessageHandler(SceneType.Rank)]
public class W2Other_SaveRankRequest: MessageHandler<Scene, W2Other_SaveDataRequest, Other2W_SaveDataResponse>
{
    protected override async ETTask Run(Scene scene, W2Other_SaveDataRequest request, Other2W_SaveDataResponse response)
    {
        await ETTask.CompletedTask;
        Log.Error("保存排行榜数据!");
    }
}