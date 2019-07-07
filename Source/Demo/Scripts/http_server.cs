using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ezserver
{
    public class http_server : MonoBehaviour
    {
        [SerializeField]
        InputField port;

        [SerializeField]
        Button button;

        [SerializeField]
        Text console;

        bool state;

        int _port = 8080;

        public void OnClick()
        {
            if(port.text != "")
                _port = System.Int32.Parse(port.text);

            if (!state)
            {
                state = ezserver.HTTP_Server_Start(
                            (string rawUrl,string postContent)=>
                                { Invoker.InvokeInMainThread(
                                    delegate { console.text += rawUrl+" "+postContent+"\n";});
                                    return "hello world";
                                } , _port
                            );

                if (state)
                    button.transform.GetComponentInChildren<Text>().text = "Stop";


            }
            else
            {
                ezserver.HTTP_Server_Stop();
                button.transform.GetComponentInChildren<Text>().text = "Start";
                state = false;
            }
        }

        void Update()
        {
            // test post request
            if (Input.GetKeyDown(KeyCode.T))
            {
                ezserver.HTTP_Request_POST("http://127.0.0.1:8080/test", new Dictionary<string, string> { { "key", "value" } }, null);
            }   
        }

    }
}