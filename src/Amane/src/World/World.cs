using fNbt;

public class World
{
  public int time = 86400;

  public static bool ExistChunk(int x, int y, int z)
  {
    if (System.IO.File.Exists($"world/awo/{x.ToString()}.{y.ToString()}.{z.ToString()}.nbt.zlib"))
    {
      return true;
    }
    return false;
  }

  public static bool GenerateFlat(int x, int y, int z)
  {
    if (ExistChunk(x, y, z))
    {
      return false;
    }
    NbtCompound TileEntities = new NbtCompound("TileEntities");
    if (y > 0)
    {
      NbtCompound Blocks;
      TileEntities.Add(Blocks = new NbtCompound("Blocks"));
      for (int i = 0; i < 4096; i++)
      {
        Blocks.Add(new NbtCompound(i.ToString()) {
          new NbtFloat("BlockPos", i),
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
    return true;
  }
}