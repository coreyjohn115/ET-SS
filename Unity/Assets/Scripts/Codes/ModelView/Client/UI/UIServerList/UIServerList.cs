namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public  class UIServerList : Entity, IAwake, IUILogic
	{
		public UIServerListViewComponent View { get => GetParent<UIBaseWindow>().GetComponent<UIServerListViewComponent>();} 
	}
}
