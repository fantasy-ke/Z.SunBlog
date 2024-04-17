import * as signalR from "@microsoft/signalr";
import { ElNotification } from "element-plus";
import * as msgpack from "@microsoft/signalr-protocol-msgpack";

class ChatHub {
  public connection: signalR.HubConnection | undefined;

  // constructor() {
  //   this.initHunConnection();
  // }

  initHunConnection = () =>{
    if (
      !this.connection ||
      this.connection?.state === signalR.HubConnectionState.Disconnected ||
      this.connection?.state === signalR.HubConnectionState.Disconnecting
    ) {
      const apiUrl = useRuntimeConfig().public.apiBaseUrl as string;
      this.connection = new signalR.HubConnectionBuilder()
        .configureLogging(signalR.LogLevel.Information)
        .withUrl(`${apiUrl}/chatHub`, {
          accessTokenFactory: () => JSON.parse(localStorage.getItem("zblog_User")?? "")?.zToken?.accessToken,
          transport: signalR.HttpTransportType.WebSockets,
          skipNegotiation: true,
        })
        .withHubProtocol(new msgpack.MessagePackHubProtocol())
        .withAutomaticReconnect({
          nextRetryDelayInMilliseconds: () => {
            return 5000; // 每5秒重连一次
          },
        })
        .build();

      this.connection.keepAliveIntervalInMilliseconds = 15 * 1000; // 心跳检测15s
      this.connection.serverTimeoutInMilliseconds = 30 * 60 * 1000; // 超时时间30m
      this.connection.on("ReceiveMessage", (data) => {
        ElNotification({
          title: data.Title,
          message: data.Message,
          type: "success",
          position: "top-right",
        });
      });
    }
  }

  start = async ()=> {
    this.initHunConnection();
    this.connection?.start().then(() => {
      console.info("signalr启动成功");
    });
    this.connection?.onreconnecting(() => {
      ElNotification({
        title: "提示",
        message: "服务器已断线...",
        type: "error",
        position: "bottom-right",
      });
    });
    // 重连成功
    this.connection?.onreconnected(() => {
      console.log("重连成功");
    });
  }

  close = async ()=> {
    this.connection!.onclose((error: Error | undefined) => {
      console.info(error?.message ?? "断开连接");
    });
    this.connection!.stop().then(() => {
      console.info("结束连接");
    });
  }
}

export default new ChatHub();
