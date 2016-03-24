using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungletribes_Server
{
    public class FrameController
    {
        protected double DeltaTime;
        protected readonly double MaxDeltaTime;
        protected readonly double MinDeltaTime;
        protected readonly bool SlowCatchUp;
        protected readonly Action<double> Action;
        protected readonly Stopwatch Watch;

        public FrameController(int min_fps, int max_fps, bool slow_catch_up, Action<double> action)
            : this(slow_catch_up, action)
        {
            MaxDeltaTime = 1.0 / min_fps;
            MinDeltaTime = 1.0 / max_fps;
        }

        public FrameController(double min_delta_time, double max_delta_time, bool slow_catch_up, Action<double> action)
            : this(slow_catch_up, action)
        {
            MaxDeltaTime = min_delta_time;
            MinDeltaTime = max_delta_time;
        }

        protected FrameController(bool slow_catch_up, Action<double> action)
        {
            SlowCatchUp = slow_catch_up;
            Action = action;
            Watch = new Stopwatch();
            Watch.Start();
        }

        public void TryFrame()
        {
            var delta_time = Watch.Elapsed.TotalSeconds;
            Watch.Restart();
            DeltaTime += delta_time;

            if (DeltaTime <= MinDeltaTime)
                // Skip
                return;
            else if (DeltaTime >= MaxDeltaTime)
                DoFrame(SlowCatchUp ? MaxDeltaTime : DeltaTime);
            else
                DoFrame(DeltaTime);
        }

        protected void DoFrame(double delta_time)
        {
            Action(delta_time);
            DeltaTime -= delta_time;
        }
    }
}
