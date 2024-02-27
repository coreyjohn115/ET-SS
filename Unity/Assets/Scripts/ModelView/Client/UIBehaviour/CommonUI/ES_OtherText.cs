﻿using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[ChildOf]
	[EnableMethod]
	public partial class ES_OtherText : Entity, ET.IAwake<Transform>, IDestroy 
	{
		public ES_OtherChatLite ES_OtherChatLite
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_otherchatlite == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"ES_OtherChatLite");
		    	   this.m_es_otherchatlite = this.AddChild<ES_OtherChatLite,Transform>(subTrans);
     			}
     			return this.m_es_otherchatlite;
     		}
     	}

		public ExtendImage E_BgExtendImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BgExtendImage == null )
     			{
		    		this.m_E_BgExtendImage = UIFindHelper.FindDeepChild<ExtendImage>(this.uiTransform.gameObject,"E_Bg");
     			}
     			return this.m_E_BgExtendImage;
     		}
     	}

		public LongClickButton E_MsgLongClickButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MsgLongClickButton == null )
     			{
		    		this.m_E_MsgLongClickButton = UIFindHelper.FindDeepChild<LongClickButton>(this.uiTransform.gameObject,"E_Bg/E_Msg");
     			}
     			return this.m_E_MsgLongClickButton;
     		}
     	}

		public SymbolText E_MsgSymbolText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MsgSymbolText == null )
     			{
		    		this.m_E_MsgSymbolText = UIFindHelper.FindDeepChild<SymbolText>(this.uiTransform.gameObject,"E_Bg/E_Msg");
     			}
     			return this.m_E_MsgSymbolText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_es_otherchatlite = null;
			this.m_E_BgExtendImage = null;
			this.m_E_MsgLongClickButton = null;
			this.m_E_MsgSymbolText = null;
			uiTransform = null;
		}

		private EntityRef<ES_OtherChatLite> m_es_otherchatlite = null;
		private ExtendImage m_E_BgExtendImage = null;
		private LongClickButton m_E_MsgLongClickButton = null;
		private SymbolText m_E_MsgSymbolText = null;
		public Transform uiTransform = null;
	}
}
