# EZ Server


## 使用方法：
* 将TaskInvoker.prefab拖入场景
* 使用命名空间ezserver
* 使用Invoker.InvokerInMainThread(Action action)和主线程交互（这是因为EZ Server是基于非阻塞Socket） 

# /




### 服务端



创建HTTP服务端

```
ezserver.HTTP_Server_Start((string rawURL, string postContent) => { return "Hello World"; },8085); 
```

创建TCP Socket 服务端

```
ezserver.TCP_Server_Start((string clientID)=> { },(string clientID, string receivedStr) => { return "got it"; }, 8084); 
```


创建UDP Socket 服务端

```
ezserver.UDP_Server_Start((System.Net.EndPoint endPoint, string receivedStr) => { return "got it"; }, 8083);
```



### 客户端



创建HTTP客户端

```
ezserver.HTTP_Request_GET("http://127.0.0.1", (string data) => { } ); 
```

创建TCP客户端

```
ezserver.TCP_Client_Start("127.0.0.1", 8084, (string receivedStr) => { return "got it"; }); 
```


发送UDP消息（不需要创建客户端）

```
ezserver.UDP_Send(new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 8083), "hello");
```




### 小工具



获取本地IP

```
string[] myIp = Tools.NetWorkTools.GetLocalIP();
```


正则匹配
示例：爬取页面内所有图片

```
ezserver.HTTP_Request_GET("http://example.com", (string html) => { foreach (var item in Tools.RegEx.FindAll(html, "<img src=\"", "\"", false)) { Console.WriteLine(item); } }); 
```

### Unity 线程交互

使用Invoker.InvokeInMainThread和主线程交互

```
Text text_view = this.transform.GetComponentInChildren<Text>();

ezserver.HTTP_Server_Start(
(string rawUrl,string postContent)=> { 
Invoker.InvokeInMainThread( delegate { text_view.text += rawUrl+" "+postContent+"\n"; } );
return "hello world";
}
);

```