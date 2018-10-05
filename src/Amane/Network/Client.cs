using Ether.Network.Common;
using Ether.Network.Packets;
using System;

namespace Network
{
  internal sealed class Client : NetUser
  {
    public void SendFirstPacket()
    {
      using (var packet = new NetPacket())
      {
        packet.Write("Welcome " + this.Id.ToString());

        this.Send(packet);
      }
    }

    public override void HandleMessage(INetPacketStream packet)
    {
      var value = packet.Read<string>();

      Console.WriteLine("Received '{1}' from {0}", this.Id, value);

      using (var p = new NetPacket())
      {
        p.Write(string.Format("OK: '{0}'", value));
        this.Server.SendToAll(p);
      }
    }
  }
}
