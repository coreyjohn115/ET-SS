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
    }
}