using Gameplay.Mechanics.Timer;
using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Abilities;
using SpaceRogue.Gameplay.Abilities.Scriptables;
using SpaceRogue.Gameplay.Shooting.Factories;
using UnityEngine;

public class TorpedoAbility : Ability
{

    #region Fields

    private readonly TorpedoAbilityConfig _torpedoAbilityConfig;
    private readonly TorpedoFactory _torpedoFactory;
    private readonly InstantExplosionFactory _instantExplosionFactory;
    private readonly EntityViewBase _entityView;

    #endregion


    #region CodeLife

    public TorpedoAbility(
        TorpedoAbilityConfig torpedoAbilityConfig,
        TorpedoFactory torpedoFactory,
        TimerFactory timerFactory,
        InstantExplosionFactory instantExplosionFactory,
        EntityViewBase entityView) : base(torpedoAbilityConfig, timerFactory)
    {
        _torpedoAbilityConfig = torpedoAbilityConfig;
        _torpedoFactory = torpedoFactory;
        _instantExplosionFactory = instantExplosionFactory;
        _entityView = entityView;
    }

    #endregion


    #region Methods

    public override void UseAbility()
    {
        if (IsOnCooldown)
        {
            return;
        }

        Vector2 position = _entityView.transform.position;
        Quaternion rotation = _entityView.transform.rotation;
        _torpedoFactory.Create(position, rotation, _torpedoAbilityConfig.TorpedoConfig, _instantExplosionFactory, this);

        CooldownTimer.Start();
    }

    #endregion

}