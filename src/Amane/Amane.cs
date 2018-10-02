using System;
using System.Threading;
using Logger;
using Time;

public class Amane
{
  public const string version = "0.1.0";
  public static int coreCount = System.Environment.ProcessorCount;

  static void Main(string[] args)
  {
    Log.info("=========================");
    Log.info("|         Amane         |");
    Log.info($"|    version: {version}     |");
    Log.info("=========================");
    Log.info("天音システムを起動しています...");
    Log.info($"{coreCount}コアに最適化して実行されます...");
    Clock.Ticking();
    while (true)
    {
      string command = Console.ReadLine();
      Log.info("コマンドを受け取りました");
      if (command == "exit")
      {
        Amane.exit();
        break;
      }
    }
  }
  public static void exit()
  {
    Log.info("終了処理を開始します");
    Log.info("タイマーを停止します");
    Clock.timer.Stop();
    Log.info("タイマーを停止しました");
  }
}
