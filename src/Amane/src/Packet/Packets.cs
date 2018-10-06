using Packet;

namespace Packet
{
  public class SayPacket : Packet { public SayPacket(string msg) : base(0x0E, $"{{msg: \"{msg}\"}}") { } }
  // class ChunkPacket : Packet { ChunkPacket() : base(0x22, "{}") { } }
}