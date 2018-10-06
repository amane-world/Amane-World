using System;

namespace Logger
{
  public class Log
  {
    private const string LogTimeFormat = "yyyy-MM-dd h:mm:ss";

    public static void info(string text)
    {
      string nowTime = DateTime.Now.ToString(LogTimeFormat);
      Console.WriteLine($"[{nowTime}][INFO] {text}");
    }
    public static void warn(string text)
    {
      string nowTime = DateTime.Now.ToString(LogTimeFormat);
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine($"[{nowTime}][WARN] {text}");
      Console.ResetColor();
    }
    public static void err(string text)
    {
      string nowTime = DateTime.Now.ToString(LogTimeFormat);
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine($"[{nowTime}][ERR] {text}");
      Console.ResetColor();
    }
    public static void notice(string text)
    {
      string nowTime = DateTime.Now.ToString(LogTimeFormat);
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine($"[{nowTime}][NOTICE] {text}");
      Console.ResetColor();
    }
  }
}
