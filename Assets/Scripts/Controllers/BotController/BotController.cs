using System;
using Constants;
using Controllers.DepositoryControllers;
using Controls.GameElements.Bot;
using Enums;
using Services.Timers;
using Utils.Ioc;

namespace Controllers.BotController
{
    public class BotController //: BaseController
    {
        [Inject] private TimersService _timersService;
        [Inject] private EconomyController _economyController;
        
        private readonly BotControl _botControl;
        
        private ITimer _timer;
        private BotLocation _botLocation;
        
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

        private void CameToLocationHandler(BotLocation botLocation)
        {
            _botLocation = botLocation;
            
            switch(botLocation)
            {
                case BotLocation.Home:
                    Release();
                    IsBusy = false;
                    break;
                case BotLocation.Shaft:
                case BotLocation.Unload:
                    _timersService.AddTimer(CommonConstants.BotCollectingTime, OnTimerDone);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(botLocation), botLocation, null);
            }
        }

        private void OnTimerDone(ITimer obj)
        {
            switch (_botLocation)
            {
                case BotLocation.Home:
                    break;
                case BotLocation.Shaft:
                    _botControl.StartMoveToUnload();
                    break;
                case BotLocation.Unload:
                    _botControl.StartMoveToHome();
                    _economyController.AddCurrency(CurrencyType.Currency0, CommonConstants.BotCollectCount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}