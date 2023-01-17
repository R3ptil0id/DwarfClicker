using System;
using Controls;
using UnityEngine;
using Utils;
using Utils.Ioc;

using Random = UnityEngine.Random;

public class BotControl : BaseControl
{
    public enum BotLocation
    {
        Home,
        Shaft,
        Unload
    }

    [SerializeField][Range(0,10)] private float _xAirOffset = 6;
    [SerializeField][Range(0,10)] private float _yAirOffset = 0.5f;
    
    [SerializeField][Range(0,10)] private float _xTargetOffset = 5.7f;
    [SerializeField][Range(0,10)] private float _yTargetOffset = 0.4f;
    [SerializeField][Range(0,5)] private float _speed = 0.7f;
    
    [Inject] private ObjectsInstaller _objectsInstaller;
    
    private Vector3 _airPosition;
    private Vector3 _targetPosition;
    private Vector3 _startPosition;
    
    private float _timer;
    private bool _moving;
    
    private Action _moveAction;
    
    public Action<BotLocation> CameToLocation;
    
    public void Initialize(Vector3 parentPosition)
    {
        base.Initialize();

        var position = parentPosition;
        var xAir = Random.Range(-_xAirOffset, _xAirOffset);
        var yAir = Random.Range(-_yAirOffset, _yAirOffset);

        var airPointPosition = _objectsInstaller.AirPoint.position;
        _airPosition = new Vector3(airPointPosition.x + xAir, airPointPosition.y + yAir, position.z);
        
        var xTarget = Random.Range(-_xTargetOffset, _xTargetOffset);
        var yTarget = Random.Range(-_yTargetOffset, _yTargetOffset);

        var targetPosition = _objectsInstaller.TargetPoint.position;
        _targetPosition = new Vector3(targetPosition.x + xTarget, targetPosition.y + yTarget, position.z);
        _startPosition = _objectsInstaller.HomePoint.position;
        Reset();
    }

    public void StartMoveToTarget()
    {
        _moving = true;
        _moveAction = MoveToTarget;
    }

    public void StartMoveToUnload()
    {
        _moving = true;
        _moveAction = MoveToUnload;
    }
    
    public void StartMoveToHome()
    {
        _moving = true;
        _moveAction = MoveToHome;
    }

    private void MoveToTarget()
    {
        if (Vector2.Distance(transform.position, _targetPosition) > 0.02f)
        {
            transform.position = UMath.BezierCubicEaseOutBack(_timer += Time.deltaTime * _speed, _startPosition, _airPosition, _targetPosition);
        }
        else
        {
            CameToLocation?.Invoke(BotLocation.Shaft);
            Reset();
        }
    }

    private void MoveToUnload()
    {
        if (Vector2.Distance(transform.position, _objectsInstaller.UnloadPoint.position) > 0.02f)
        {
            transform.position = UMath.BezierCubicEaseOutBack(_timer += Time.deltaTime * _speed, _targetPosition,
                _airPosition, _objectsInstaller.UnloadPoint.position);
        }
        else
        {
            CameToLocation?.Invoke(BotLocation.Unload);
            Reset();
        }
    }
    
    private void MoveToHome()
    {
        if (Vector2.Distance(transform.position, _startPosition) > 0.02f)
        {
            transform.position = UMath.BezierCubicEaseOutBack(_timer += Time.deltaTime * _speed,
                _objectsInstaller.UnloadPoint.position, _airPosition, _startPosition);
        }
        else
        {
            CameToLocation?.Invoke(BotLocation.Home);
            Reset();
        }
    }
    
    private void Idle()
    {
    }

    private void Update()
    {
        if (_moving)
            _moveAction?.Invoke();
    }

    private void Reset()
    {
        _timer = 0;
        _moving = false;
        _moveAction = Idle;
    }
}
