using Gameplay.Mechanics.Timer;
using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Shooting;
using SpaceRogue.Gameplay.Shooting.Factories;
using UnityEngine;
using Zenject;


public class InstantExplosionFactory : PlaceholderFactory<Vector2, InstantExplosionConfig, IDestroyable, InstantExplosion>
{

    #region Fields

    private readonly ExplosionViewFactory _viewFactory;
    private readonly TimerFactory _timerFactory;

    #endregion


    #region CodeLife

    public InstantExplosionFactory(ExplosionViewFactory viewFactory, TimerFactory timerFactory)
    {
        _viewFactory = viewFactory;
        _timerFactory = timerFactory;
    }

    #endregion


    #region Methods

    public override InstantExplosion Create(Vector2 position, InstantExplosionConfig config, IDestroyable destroyable)
    {
        var view = _viewFactory.Create(position, config);
        view.transform.localScale = new Vector2(config.DamageRadiusSize, config.DamageRadiusSize);
        var explosion = new InstantExplosion(_timerFactory, view, config, destroyable);
        return explosion;
    }

    #endregion

}

