using Gameplay.Mechanics.Timer;
using SpaceRogue.Abstraction;
using SpaceRogue.Enums;
using SpaceRogue.Gameplay.Shooting;
using SpaceRogue.Services;
using System;

public abstract class BaseReactiveObject : IDisposable, IDestroyable
{

    #region Events

    public event Action Destroyed;

    #endregion


    #region Fields

    protected const float ANGLE_CORRECTION = 270.0f;

    protected readonly InstantExplosionFactory _instantExplosionFactory;

    protected readonly ReactiveObjectView _reactiveObjectView;
    protected readonly TargetFinderView _targetFinderView;
    protected EntityViewBase _targetView;

    protected EntityType _targetUnitType;

    protected readonly Updater _updater;
    protected Timer _timer;

    #endregion


    #region CodeLife

    public BaseReactiveObject(ReactiveObjectView view,
        Updater updater,
        TimerFactory timerFactory,
        InstantExplosionFactory instantExplosionFactory)
    {
        _targetFinderView = view.TargetFinderView;
        _updater = updater;
        _reactiveObjectView = view;

        _reactiveObjectView.TargetEntersTrigger += ActiveExplosion;
        _updater.SubscribeToUpdate(RocketFly);

        _instantExplosionFactory = instantExplosionFactory;
    }

    public virtual void Dispose()
    {
        _timer.Dispose();
        _reactiveObjectView.TargetEntersTrigger -= ActiveExplosion;
        _updater.UnsubscribeFromUpdate(RocketFly);
        OnDispose();
        Destroyed?.Invoke();
    }

    #endregion


    #region Methods

    protected abstract void RocketFly();

    protected abstract void OnDispose();

    protected virtual void ActiveExplosion()
    {
        Dispose();
    }

    protected virtual void TargetLocked(EntityViewBase target)
    {
        _targetFinderView.TargetEntersTrigger -= FindTarget;
        _targetView = target;
    }

    protected void FindTarget(EntityViewBase target)
    {
        if (_targetUnitType.HasFlag(target.EntityType))
        {
            TargetLocked(target);
        }
    }

    protected void ExpolsionAfterLifeTime() => ActiveExplosion();

    #endregion

}
