using System;

using System.Threading;
using System.Windows.Forms;
using System.Timers;

namespace Software_II__Advanced__CSharp__C969
{
    internal class LiveClock
    {
        private readonly System.Windows.Forms.Timer _timer;
        private readonly Label _displayLabel;

 

        public LiveClock(Label label, int interval = 1000)
        {
            _displayLabel = label ?? throw new ArgumentNullException(nameof(label));

            _timer = new System.Windows.Forms.Timer
            {
                Interval = interval
            };

            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _displayLabel.Text = DateTime.Now.ToString("hh:mm:ss");
        }

     
        public void Start()
        {
            _timer.Start();
        }

     
        public void Stop()
        {
            _timer.Stop();
        }
    }
}
