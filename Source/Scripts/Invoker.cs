using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ezserver
{
    public class Invoker : MonoBehaviour
    {
        static Invoker _instance ;

        public static void InvokeInMainThread(System.Action _delegate)
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<Invoker>();
                if (obj == null)
                {
                    var _obj = new GameObject("Invoker", typeof(Invoker));
                    _instance = _obj.GetComponent<Invoker>();
                }
                else
                {
                    _instance = obj;
                }
            }

            _instance.delegates.Add(_delegate);

        }

        public static void EnableUnityLog()
        {
            Tools.Logger.logType = Tools.Logger.LogType.Custom;
            Tools.Logger.LogDelegate += (msg) => Debug.Log(msg);
            Tools.Logger.WarnDelegate += (msg) => Debug.LogWarning(msg);
            Tools.Logger.ErrorDelegate += (msg) => Debug.LogError(msg);
        }

        public List<System.Action> delegates = new List<System.Action>();

        private void Awake()
        {
            _instance = this;
        }

        void Update()
        {
            Execute();
        }

        void Execute()
        {
            if (delegates.Count == 0)
                return;

            for (int i = 0; i < delegates.Count; i++)
                delegates[i]();

            delegates.Clear();

        }

    }
}