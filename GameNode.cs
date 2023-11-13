
namespace Framework
{
    public interface IScene
    {
        public void Start();
        public void Update();
    }

    public class GameNode : IScene
    {
        #region Member Variables

        protected const int CONSOLE_TIME_MS = 1000;
        protected const int CONSOLE_PER_FRAME = 60;
        private int consoleFps = CONSOLE_TIME_MS / CONSOLE_PER_FRAME;

        protected Timer? _timer;

        protected bool _isRunning;
        protected bool _isAsyncUpdate = false;

        /* Property */
        public bool IsRunning { get { return _isRunning; } set { _isRunning = value; } }

        #endregion

        #region Main Methods
        public virtual void Start()
        {
            _isRunning = true;
        }

        public virtual void Update()
        {
            if (!IsRunning)
                return;
        }

        protected virtual void AsyncUpdate(object? state)
        {
            if (!IsRunning || !_isAsyncUpdate)
            {
                this.StopUpdate();
                return;
            }
        }
        #endregion

        #region Helper Methods
        protected virtual void StartAsyncUpdate()
        {
            // Console Update Initilizing
            if (_timer == null)
                _timer = new Timer(AsyncUpdate, null, 0, consoleFps);

            _isAsyncUpdate = true;
        }

        public void StopUpdate()
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
            _timer?.Dispose();
            _isAsyncUpdate = false;
        }

        public virtual void Reset()
        {
            _isRunning = false;

            if(_timer != null)
                this.StopUpdate();
        }

        protected void SettingFPS(int fps)
        {
            consoleFps = CONSOLE_TIME_MS / fps;
        }

        protected void SettingFPStoMS(int ms)
        {
            consoleFps = ms;
        }
        #endregion
    }
}
