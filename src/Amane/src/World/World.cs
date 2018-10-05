using fNbt;

class World
{
  public int time = 86400;
  public static void GenerateFlat(int x, int y, int z)
  {
    NbtCompound TileEntities = new NbtCompound("TileEntities");
    if (y > 0)
    {
      NbtCompound Blocks;
      TileEntities.Add(Blocks = new NbtCompound("Blocks"));
      for (int i = 0; i < 4096; i++)
      {
        Blocks.Add(new NbtCompound(i.ToString()) {
          new NbtInt("BlockPos", i),
          new NbtInt("BlockID", 1),
        });
      }
    }
    NbtCompound compound = new NbtCompound("root")
    {
        new NbtInt("DataVersion", Amane.chunkVersion),
        new NbtCompound("Level")
        {
          new NbtInt("xPos", x),
          new NbtInt("yPos", y),
          new NbtInt("zPos", z),
          TileEntities
        }
    };
    var NBTFile = new NbtFile(compound);
    NBTFile.SaveToFile($"world/awo/{x.ToString()}.{y.ToString()}.{z.ToString()}.nbt.zlib", NbtCompression.ZLib);
  }
}