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
      /* 1day = 2.4hour */

      timer.Elapsed += (sender, e) =>
                  {
                    /* ToDo */
                    /* TimeCycle.tick */
                  };
      timer.Start();
    }
  }
}