using fNbt;

class World
{
  public int time = 86400;
  public static void GenerateFlat()
  {
    // var compound = new NbtCompound("root"){
    //     new NbtInt("someInt", 123),
    //     new NbtList("byteList") {
    //         new NbtByte(1),
    //         new NbtByte(2),
    //         new NbtByte(3)
    //     },
    //     new NbtCompound("nestedCompound") {
    //         new NbtDouble("pi", 3.14)
    //     }
    // };
    // var NBTFile = new NbtFile(compound);
    // NBTFile.SaveToFile("worlds/server.nbt.zlib", NbtCompression.ZLib);
  }
}