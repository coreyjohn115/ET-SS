using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class UIServerListViewComponent : Entity, IAwake, IDestroy 
	{
		public void DestroyWidget()
		{
			uiTransform = null;
		}

		public Transform uiTransform = null;
	}
}
