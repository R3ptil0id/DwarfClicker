using System.Collections;
using Enums;
using Services.Coroutines;
using UnityEngine;
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
        
        [Inject] private CoroutineService _coroutiner;

        private Vector3 _startPosition;
        private float _delayStartAnimation;
        public WorkerType WorkerType => _workerType;
        public bool IsBusy { get; private set; }
        
        public override void Initialize()
        {
            base.Initialize();
            
            _animator.enabled = false;
            _delayStartAnimation = Random.Range(_delayMin, _delayMax);
            _coroutiner.StartCoroutine(StartAnimation());
        }

        public void Initialize(Vector3 startPosition)
        {
            transform.position = startPosition;
        }
        
        public void Release()
        {
            IsBusy = false;
            _coroutiner.StopCoroutine(StartAnimation());
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