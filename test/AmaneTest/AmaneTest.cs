using System;
using System.IO;
using Xunit;

namespace AmaneTest
{
  public class AamneTest
  {
    [Fact]
    public void WorldTest()
    {
      new FileInfo("./../../../world/awo/0.-1.0.nbt.zlib").Delete();
      new FileInfo("./../../../world/awo/0.0.0.nbt.zlib").Delete();
      new FileInfo("./../../../world/awo/0.1.0.nbt.zlib").Delete();
      for (int i = 0; i < 3; i++)
      {
        Assert.True(World.GenerateFlat(0, -1, 0, "./../../../"));
        Assert.True(World.GenerateFlat(0, 0, 0, "./../../../"));
        Assert.True(World.GenerateFlat(0, 1, 0, "./../../../"));
        new FileInfo("./../../../world/awo/0.-1.0.nbt.zlib").Delete();
        new FileInfo("./../../../world/awo/0.0.0.nbt.zlib").Delete();
        new FileInfo("./../../../world/awo/0.1.0.nbt.zlib").Delete();
      }
    }
  }
}
