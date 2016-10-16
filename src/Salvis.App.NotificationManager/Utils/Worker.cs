using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Salvis.App.NotificationManager.Utils
{
    class Worker
    {

        public event EventHandler<EventArgs> WorkerStarted;

        private readonly Timer _timer;

        private Action _delegateToRun;

        private object _delegateParameters;

        private Boolean _isRunning;

        public Worker()
        {
            var timeDelay = Framework.Helpers.ConfigurationHelper.GetSetting<int>("delay");
            _timer = new Timer
            {
                AutoReset = true,
                Interval = TimeSpan.FromSeconds(timeDelay).TotalMilliseconds
            };
            _timer.Elapsed += _timer_Elapsed;
        }

        ~Worker()
        {
            _timer.Stop();
            _timer.Dispose();
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Task.Factory.StartNew(_delegateToRun, CancellationToken.None);
        }

        #region Public Objects

        public Boolean IsWorking
        {
            get { return _timer.Enabled || _isRunning; }
        }

        public void Start(Action action, object parameters)
        {
            _delegateToRun = action;
            _delegateParameters = parameters;
            _isRunning = true;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _isRunning = false;
        }

        #endregion

    }
}
