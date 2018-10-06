using System;
using System.Threading;
using Logger;
using Time;
using Network;
using Packet;
using dotenv.net;

public class Amane
{
  public const string version = "0.1.0";
  public const string codename = "sumire";
  public const int chunkVersion = 1;


  static void Main(string[] args)
  {
    Console.Title = "Amane World";
    Log.info("=========================");
    Log.info("|         Amane         |");
    Log.info($"|    version: {version}     |");
    Log.info($"|   codename: {codename}    |");
    Log.info("=========================");
    Log.info("天音システムを起動しています...");
    Log.warn("天音システムはまだ不完全です");
    Log.warn("自己責任での利用をお願いします");
    Log.info("Configの起動を開始します...");
    DotEnv.Config();
    Log.info("Configの起動が完了しました...");
    Log.info("Tickerの起動を開始します...");
    Clock.Ticking();
    Log.info("Tickerの起動が完了しました...");
    Log.info("TCP Serverの起動を開始します...");
    AmaneServer server = new AmaneServer();
    server.Start();
    Log.info("TCP Serverの起動が完了しました");

    while (true)
    {
      string command = Console.ReadLine();
      switch (command.Split(" ")[0])
      {
        case "exit":
          Amane.exit();
          break;
        case "say":
          var sayPacket = new SayPacket(command.Split(" ")[1]);
          server.SendToAll(sayPacket.packet);
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
