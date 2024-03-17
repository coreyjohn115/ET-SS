using UnityEditor;
using UnityEngine;

namespace ETEditor
{
    public static class UIEditorController
    {
        [MenuItem("ET/Tools/SpawnEUICode _&#e", false, -2)]
        public static void CreateNewCode()
        {
            GameObject go = Selection.activeObject as GameObject;
            UICodeSpawner.SpawnEUICode(go);
        }

        [MenuItem("ET/Tools/CreateBone _&#r")]
        public static void CreateUnitBone()
        {
            Transform aT = Selection.activeTransform;
            if (!aT)
            {
                return;
            }

            var boneList = new[] { "Hud", "Down", "Chest", "Body" };
            var bNode = aT.Find("Bones");
            if (!bNode)
            {
                GameObject go = new GameObject("Bones");
                go.transform.SetParent(aT);
                bNode = go.transform;
            }

            var collect = aT.GetComponent<ReferenceCollector>();
            if (!collect)
            {
                collect = aT.gameObject.AddComponent<ReferenceCollector>();
            }

            collect.Clear();
            foreach (string bone in boneList)
            {
                Transform n = bNode.Find(bone);
                if (!n)
                {
                    GameObject go = new GameObject(bone);
                    go.transform.SetParent(bNode);
                    n = go.transform;
                }

                collect.Add(bone, n);
            }
            
            var audio = aT.GetComponentInChildren<AudioSource>();
            collect.Add("AudioSource", audio);
            
            var animator = aT.GetComponentInChildren<Animator>();
            collect.Add("Animator", animator);

            EditorUtility.SetDirty(aT);
        }
    }
}