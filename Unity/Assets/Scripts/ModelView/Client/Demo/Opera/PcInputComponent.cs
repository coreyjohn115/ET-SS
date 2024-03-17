using System;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

namespace ET.Client
{
    public struct HotKeyEvent
    {
        public string Name;
        public KeyCode Code;
    }

    public struct HotKey
    {
        public List<KeyCode> Keys;

        public bool Check()
        {
            bool hasDown = false;
            for (int i = 0; i < this.Keys.Count; i++)
            {
                KeyCode code = this.Keys[i];
                if (!Input.GetKeyDown(code))
                {
                    continue;
                }

                hasDown = true;
                break;
            }

            if (!hasDown)
            {
                return false;
            }

            for (int i = 0; i < this.Keys.Count; i++)
            {
                KeyCode code = this.Keys[i];
                if (!Input.GetKey(code))
                {
                    return false;
                }
            }

            return true;
        }
    }

    [ComponentOf(typeof (Scene))]
    public class PcInputComponent: Entity, IAwake, IUpdate, ILateUpdate
    {
        public LeanFingerFilter Use = new LeanFingerFilter(true);
        
        public bool moving;
        public Dictionary<string, HotKey> hotKeyDict = new Dictionary<string, HotKey>();
    }
}