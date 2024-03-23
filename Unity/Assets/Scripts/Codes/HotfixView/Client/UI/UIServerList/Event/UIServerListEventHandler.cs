namespace ET.Client
{
	[FriendOf(typeof(WindowCoreData))]
	[FriendOf(typeof(UIBaseWindow))]
	[AUIEvent(WindowID.Win_UIServerList)]
	public  class UIServerListEventHandler : IAUIEventHandler
	{
		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.WindowData.WindowType = UIWindowType.Normal; 
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.AddComponent<UIServerListViewComponent>(); 
			uiBaseWindow.AddComponent<UIServerList>(); 
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<UIServerList>().RegisterUIEvent(); 
		}

		public void OnFocus(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<UIServerList>().Focus(); 
		}

		public void OnUnFocus(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<UIServerList>().UnFocus(); 
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
		{
			uiBaseWindow.GetComponent<UIServerList>().ShowWindow(contextData); 
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow)
		{

		}

		public void BeforeUnload(UIBaseWindow uiBaseWindow)
		{

		}
	}
}
