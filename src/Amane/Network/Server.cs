using System;
using Logger;
using Ether.Network.Server;

namespace Network
{
  internal sealed class AmaneServer : NetServer<Client>
  {
    public AmaneServer()
    {
      this.Configuration.Backlog = 100;
      this.Configuration.Port = 29265;
      this.Configuration.MaximumNumberOfConnections = 100;
      this.Configuration.Host = "127.0.0.1";
      this.Configuration.BufferSize = 8;
      this.Configuration.Blocking = true;
    }

    protected override void Initialize()
    {
      Log.info("TCP Serverの準備が完了しました");
    }

    protected override void OnClientConnected(Client connection)
    {
      Log.info($"New client[{connection.Id.ToString()}] connected!");

      connection.SendFirstPacket();
    }

    protected override void OnClientDisconnected(Client connection)
    {
      Log.info($"Client[{connection.Id.ToString()}] disconnected!");
    }

    protected override void OnError(Exception exception)
    {
      Log.err(exception.ToString());
    }
  }
}