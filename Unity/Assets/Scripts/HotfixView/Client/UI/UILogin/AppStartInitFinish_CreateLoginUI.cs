﻿namespace ET.Client
{
	[Event(SceneType.Client)]
	public class AppStartInitFinish_CreateLoginUI: AEvent<Scene, AppStartInitFinish>
	{
		protected override async ETTask Run(Scene root, AppStartInitFinish args)
		{
			await root.GetComponent<UIComponent>().ShowWindowAsync(WindowID.Win_UILogin);
		}
	}
}
