using System.Collections.Generic;
using System.Linq;
using Constants;
using Controllers.Perks;
using Controls;
using Controls.GameElements.Bot;
using ScriptableObjects;
using UnityEngine;
using Utils.Ioc;

using Object = UnityEngine.Object;

namespace Controllers.BotController
{
    public class BotPoolController : InitializableBaseController
    {
        [Inject] private ObjectsInstaller _objectsInstaller;
        [Inject] private PrefabTable _prefabTable;
        [Inject] private PerksController _perksController;
        
        private Transform _parentObject;
        private List<BotController> _bots;

        public BotPoolController()
        {
            _parentObject = _objectsInstaller.PoolObject;
            _bots = new List<BotController>(CommonConstants.BotMaxCountOnStart);
            GenerateBots(_perksController.GetPerksData<BotsPerks>().MaxCount);
        }

        public BotController GetBotController()
        {
            var control = _bots.FirstOrDefault(c => !c.IsBusy);
            control?.Initialize();
            return control;
        }

        public void GenerateBots(int count)
        {
            var maxCount = _perksController.GetPerksData<BotsPerks>().MaxCount;
            
            if (_bots.Count >= maxCount)
            {
                return;
            }

            var checkBots = _bots.Capacity - maxCount;
            
            if (checkBots < 0)
            {
                count = Mathf.Abs(checkBots);
            }

            for (var i = 0; i < count; i++)
            {
                var instance = Object.Instantiate(_prefabTable.Bot, _parentObject);
                var component = instance.GetComponent<BotControl>();
                component.Initialize();
                var controller = new BotController(component);
                    
                _bots.Add(controller);
            }
        }
    }
}