﻿using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[ChildOf]
	[EnableMethod]
	public partial class ES_SelfImage : Entity, ET.IAwake<Transform>, IDestroy 
	{
		public ES_SelfChatLite ES_ChatLite
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_chatlite == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"ES_ChatLite");
		    	   this.m_es_chatlite = this.AddChild<ES_SelfChatLite,Transform>(subTrans);
     			}
     			return this.m_es_chatlite;
     		}
     	}

		public ExtendImage E_IconExtendImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_IconExtendImage == null )
     			{
		    		this.m_E_IconExtendImage = UIFindHelper.FindDeepChild<ExtendImage>(this.uiTransform.gameObject,"E_Icon");
     			}
     			return this.m_E_IconExtendImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_es_chatlite = null;
			this.m_E_IconExtendImage = null;
			uiTransform = null;
		}

		private EntityRef<ES_SelfChatLite> m_es_chatlite = null;
		private ExtendImage m_E_IconExtendImage = null;
		public Transform uiTransform = null;
	}
}
