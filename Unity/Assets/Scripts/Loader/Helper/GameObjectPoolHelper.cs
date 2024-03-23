using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public static class GameObjectPoolHelper
    {
        private static readonly Dictionary<string, GameObjectPool> poolDict = new Dictionary<string, GameObjectPool>();

        public static void InitPool(string poolName, GameObject prefab, int size, PoolInflationType type = PoolInflationType.DOUBLE)
        {
            if (!poolDict.ContainsKey(poolName))
            {
                poolDict[poolName] = new GameObjectPool(poolName, prefab, GameObject.Find("Global/Pool"), size, type);
            }
        }

        public static async ETTask InitPoolFormGamObjectAsync(string poolName, int size, PoolInflationType type = PoolInflationType.DOUBLE)
        {
            if (poolDict.ContainsKey(poolName))
            {
                return;
            }
            else
            {
                try
                {
                    GameObject pb = await GetGameObjectByResTypeAsync(poolName);
                    if (pb == null)
                    {
                        Debug.LogError("[ResourceManager] Invalid prefab name for pooling :" + poolName);
                        return;
                    }

                    poolDict[poolName] = new GameObjectPool(poolName, pb, GameObject.Find("Global/Pool"), size, type);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }

            await ETTask.CompletedTask;
        }

        public static GameObject GetObjectFromPool(string poolName, bool autoActive = true)
        {
            GameObject result = null;
            if (poolDict.ContainsKey(poolName))
            {
                GameObjectPool pool = poolDict[poolName];
                result = pool.NextAvailableObject(autoActive);
#if UNITY_EDITOR
                if (result == null)
                {
                    Log.Error("[ResourceManager]:No object available in " + poolName);
                }
#endif
            }
#if UNITY_EDITOR
            else
            {
                Log.Error("[ResourceManager]:Invalid pool name specified: " + poolName);
            }
#endif
            return result;
        }

        /// <summary>
        /// Return obj to the pool
        /// </summary>
        /// <OtherParam name="go"></OtherParam>
        public static void ReturnObjectToPool(GameObject go)
        {
            PoolObject po = go.GetComponent<PoolObject>();
            if (po == null)
            {
#if UNITY_EDITOR
                Log.Error("Specified object is not a pooled instance: " + go.name);
#endif
            }
            else
            {
                GameObjectPool pool = null;
                if (poolDict.TryGetValue(po.poolName, out pool))
                {
                    pool.ReturnObjectToPool(po);
                }
#if UNITY_EDITOR
                else
                {
                    Log.Error("No pool available with name: " + po.poolName);
                }
#endif
            }
        }

        /// <summary>
        /// Return obj to the pool
        /// </summary>
        /// <OtherParam name="t"></OtherParam>
        public static void ReturnTransformToPool(Transform t)
        {
            if (t == null)
            {
#if UNITY_EDITOR
                Log.Error("[ResourceManager] try to return a null transform to pool!");
#endif
                return;
            }

            ReturnObjectToPool(t.gameObject);
        }

        public static void ClearPool(string poolName)
        {
            if (poolDict.TryGetValue(poolName, out var pool))
            {
                pool.Dispose();
            }
        }

        public static async ETTask<GameObject> GetGameObjectByResTypeAsync(string poolName)
        {
            GameObject pb = null;
            if (poolName.StartsWith("Item"))
            {
                pb = await ResourcesComponent.Instance.LoadAssetAsync<GameObject>($"Assets/Bundles/UI/Window/Item/{poolName}.prefab");
            }
            else
            {
                pb = await ResourcesComponent.Instance.LoadAssetAsync<GameObject>($"Assets/Bundles/UI/Window/Common/{poolName}.prefab");
            }

            return pb;
        }
    }
}