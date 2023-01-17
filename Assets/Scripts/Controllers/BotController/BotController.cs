using System;
using Constants;
using Services.Timers;
using Utils.Ioc;

namespace Controllers.BotController
{
    public class BotController : BaseController
    {
        [Inject] private TimersService _timersService;
        private ITimer _timer;
        private readonly BotControl _botControl;
        private BotControl.BotLocation _botLocation;
        
        public bool IsBusy { get; private set; }

        public BotController(BotControl botControl)
        {
            _botControl = botControl;
        }

        public void Initialize()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;
            
            _botControl.CameToLocation += CameToLocationHandler;
            _botControl.GenerateRandomPositions();
            _botControl.StartMoveToTarget();
        }
        
        private void Release()
        {
            if (!IsBusy)
            {
                return;
            }

            IsBusy = false;
            _botControl.HideFromScreen();
            _botControl.CameToLocation -= CameToLocationHandler;
        }

        private void CameToLocationHandler(BotControl.BotLocation botLocation)
        {
            _botLocation = botLocation;
            
            switch(botLocation)
            {
                case BotControl.BotLocation.Home:
                    Release();
                    IsBusy = false;
                    break;
                case BotControl.BotLocation.Shaft:
                case BotControl.BotLocation.Unload:
                    _timersService.AddTimer(DataConstants.BotCollectingTime, OnTimerDone);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(botLocation), botLocation, null);
            }
        }

        private void OnTimerDone(ITimer obj)
        {
            switch (_botLocation)
            {
                case BotControl.BotLocation.Home:
                    break;
                case BotControl.BotLocation.Shaft:
                    _botControl.StartMoveToUnload();
                    break;
                case BotControl.BotLocation.Unload:
                    _botControl.StartMoveToHome();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}