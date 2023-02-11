using System.Collections;
using Enums;
using UnityEngine;
using Utils;
using Utils.Ioc;

using Random = UnityEngine.Random;

namespace Controls.GameElements.Workers
{
    public abstract class WorkerControl : BaseControl
    {
        [SerializeField] private WorkerType _workerType;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _delayMin;
        [SerializeField] private float _delayMax;
        
        [Inject] private Coroutiner _coroutiner;

        private Vector3 _startPosition;
        private float _delayStartAnimation;
        public WorkerType WorkerType => _workerType;
        public bool IsBusy { get; private set; }
        
        public override void Initialize()
        {
            base.Initialize();
            
            _animator.enabled = false;
            _delayStartAnimation = Random.Range(_delayMin, _delayMax);
            _coroutiner.CoroutineRun(StartAnimation);
        }

        public void Initialize(Vector3 startPosition)
        {
            transform.position = startPosition;
        }
        
        public void Release()
        {
            IsBusy = false;
            
            InitView();
        }

        public void Busy()
        {
            IsBusy = true;
            _animator.enabled = true;
        }

        private void InitView()
        {
            
        }

        private IEnumerator StartAnimation()
        {
            yield return new WaitForSeconds(_delayStartAnimation);
        }
    }
}