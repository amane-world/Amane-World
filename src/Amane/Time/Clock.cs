using System.Timers;
using Logger;
namespace Time
{
  class Clock
  {
    public static Timer timer = new Timer(100);
    public static void Ticking()
    {
      /* 1tick = 1sec */

      timer.Elapsed += (sender, e) =>
                  {
                    /* ToDo */
                  };
      timer.Start();
    }
  }
}