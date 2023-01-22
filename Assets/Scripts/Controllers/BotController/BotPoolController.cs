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
    [RegistrateInIoc(needInitialize: true)]
    public class BotPoolController : BaseController, IInitializable
    {
        [Inject] private ObjectsInstaller _objectsInstaller;
        [Inject] private PrefabTable _prefabTable;
        [Inject] private PerksController _perksController;
        
        private Transform _parentObject;
        private List<BotController> _bots;
        
        public void Initialize()
        {
            _parentObject = _objectsInstaller.PoolObject;
            _bots = new List<BotController>(DataConstants.BotMaxCountOnStart);
            GenerateBots(_perksController.GetPerk<BotsPerks>().MaxCount);
        }

        public BotController GetBotController()
        {
            var control = _bots.FirstOrDefault(c => !c.IsBusy);
            control?.Initialize();
            return control;
        }

        public void GenerateBots(int count)
        {
            var maxCount = _perksController.GetPerk<BotsPerks>().MaxCount;
            
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