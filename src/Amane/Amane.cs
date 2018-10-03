using System;
using System.Threading;
using Logger;
using Time;
using Network;

public class Amane
{
  public const string version = "0.1.0";
  public static int coreCount = System.Environment.ProcessorCount;

  static void Main(string[] args)
  {
    Console.Title = "Amane World";
    Log.info("=========================");
    Log.info("|         Amane         |");
    Log.info($"|    version: {version}     |");
    Log.info("=========================");
    Log.info("天音システムを起動しています...");
    Log.info($"{coreCount}コアに最適化して実行されます...");
    Log.info("Tickerの起動を開始します...");
    Clock.Ticking();
    Log.info("Tickerの起動が完了しました...");
    Log.info("TCP Serverの起動を開始します...");
    AmaneServer server = new AmaneServer();
    server.Start();
    Log.info("TCP Serverの起動が完了しました...");

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
