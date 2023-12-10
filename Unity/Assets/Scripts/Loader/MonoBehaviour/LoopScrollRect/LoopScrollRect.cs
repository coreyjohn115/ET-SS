﻿using System;

namespace UnityEngine.UI
{
    public abstract class LoopScrollRect: LoopScrollRectBase
    {
        [NonSerialized]
        public LoopScrollDataSourceInstance dataSource = new LoopScrollDataSourceInstance();

        protected override void ProvideData(Transform transform, int index)
        {
            dataSource.ProvideData(transform, index);
        }

        protected override RectTransform GetFromTempPool(int itemIdx)
        {
            RectTransform nextItem = null;
            if (deletedItemTypeStart > 0)
            {
                deletedItemTypeStart--;
                nextItem = m_Content.GetChild(0) as RectTransform;
                nextItem.SetSiblingIndex(itemIdx - itemTypeStart + deletedItemTypeStart);
            }
            else if (deletedItemTypeEnd > 0)
            {
                deletedItemTypeEnd--;
                nextItem = m_Content.GetChild(m_Content.childCount - 1) as RectTransform;
                nextItem.SetSiblingIndex(itemIdx - itemTypeStart + deletedItemTypeStart);
            }
            else
            {
                nextItem = prefabSource.GetObject().transform as RectTransform;
                nextItem.transform.SetParent(m_Content, false);
                nextItem.gameObject.SetActive(true);
            }

            ProvideData(nextItem, itemIdx);
            return nextItem;
        }

        protected override void ReturnToTempPool(bool fromStart, int count)
        {
            if (fromStart)
                deletedItemTypeStart += count;
            else
                deletedItemTypeEnd += count;
        }

        protected override void ClearTempPool()
        {
            Debug.Assert(m_Content.childCount >= deletedItemTypeStart + deletedItemTypeEnd);
            if (deletedItemTypeStart > 0)
            {
                for (int i = deletedItemTypeStart - 1; i >= 0; i--)
                {
                    prefabSource.ReturnObject(m_Content.GetChild(i));
                }

                deletedItemTypeStart = 0;
            }

            if (deletedItemTypeEnd > 0)
            {
                int t = m_Content.childCount - deletedItemTypeEnd;
                for (int i = m_Content.childCount - 1; i >= t; i--)
                {
                    prefabSource.ReturnObject(m_Content.GetChild(i));
                }

                deletedItemTypeEnd = 0;
            }
        }

        public void AddItemRefreshListener(Action<Transform, int> scrollMoveEvent)
        {
            if (this.dataSource == null || scrollMoveEvent == null)
            {
                Debug.LogError("dataSource or scrollMoveEvent is error!");
            }

            this.dataSource.ScrollMoveEvent = null;
            this.dataSource.ScrollMoveEvent = scrollMoveEvent;
        }

        public void AddPrefabListener(Func<int, string> prefabEvent)
        {
            if (this.dataSource == null || prefabEvent == null)
            {
                Debug.LogError("dataSource or scrollMoveEvent is error!");
            }

            this.dataSource.PrefabEvent = null;
            this.dataSource.PrefabEvent = prefabEvent;
        }
    }
}