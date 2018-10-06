using System;
using System.Threading;
using Logger;
using Time;
using Network;
using dotenv.net;

public class Amane
{
  public const string version = "0.1.0";
  public const int chunkVersion = 1;

  static void Main(string[] args)
  {
    Console.Title = "Amane World";
    Log.info("=========================");
    Log.info("|         Amane         |");
    Log.info($"|    version: {version}     |");
    Log.info("=========================");
    Log.info("天音システムを起動しています...");
    Log.info($"{System.Environment.ProcessorCount}コアに最適化して実行されます...");
    Log.info("Configの起動を開始します...");
    DotEnv.Config();
    Log.info("Configの起動が完了しました...");
    Log.info("Tickerの起動を開始します...");
    Clock.Ticking();
    Log.info("Tickerの起動が完了しました...");
    Log.info("TCP Serverの起動を開始します...");
    AmaneServer server = new AmaneServer();
    server.Start();
    Log.info("TCP Serverの起動が完了しました...");

    // for (int x = -2; x < 3; x++)
    // {
    //   for (int y = -2; y < 3; y++)
    //   {
    //     for (int z = -2; z < 3; z++)
    //     {
    //       World.GenerateFlat(x, y, z);
    //     }
    //   }
    // }

    while (true)
    {
      string command = Console.ReadLine();
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
