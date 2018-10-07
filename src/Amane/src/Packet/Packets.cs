using fNbt;
using Packet;

namespace Packet
{
  public class SayPacket : Packet
  {
    public SayPacket(string msg) : base(0x0E, $"{{\"msg\": \"{msg}\"}}") { }
  }
  public class ChunkPacket : Packet
  {
    public ChunkPacket(int x, int y, int z, NbtTag tag) : base(0x22, $"{{\"Chunk X\":{x}, \"Chunk Y\":{y}, \"Chunk Z\":{z},\"Data\":{tag["Level"]["Blocks"]}}}") { }
  }
}