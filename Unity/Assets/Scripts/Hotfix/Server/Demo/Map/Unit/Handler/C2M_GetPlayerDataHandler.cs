using System.Collections.Generic;

namespace ET.Server;

[MessageLocationHandler(SceneType.Map)]
public class C2M_GetPlayerDataHandler: MessageLocationHandler<Unit, C2M_GetPlayerData, M2C_GetPlayerData>
{
    protected override async ETTask Run(Unit unit, C2M_GetPlayerData request, M2C_GetPlayerData response)
    {
        Pair<Dictionary<int, long>, List<TaskProto>> pair = unit.GetComponent<TaskComponent>().GetTaskList();
        response.FinishDict = pair.Key;
        response.TaskList = pair.Value;
        response.ItemList = unit.GetComponent<ItemComponent>().GetItemList();
        await ETTask.CompletedTask;
    }
}