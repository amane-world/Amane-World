using Ether.Network.Packets;

namespace Packet
{
  public class Packet
  {
    public int id = 0x0f;
    public string body = "{}";
    public NetPacket packet = new NetPacket();

    public Packet(int id, string body = null)
    {
      this.id = id;
      if (body != null)
      {
        this.body = body;
      }
      packet.Write<string>($"0x{id.ToString("x2")}{body}");
    }
  }
}