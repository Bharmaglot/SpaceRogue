using Gameplay.Mechanics.Timer;
using Gameplay.Shooting.Factories;
using Gameplay.Shooting.Scriptables;
using SpaceRogue.Enums;
using UnityEngine;
using Zenject;

namespace Gameplay.Shooting.Weapons
{
    public class Mortar : Weapon
    {
        private readonly MortarConfig _mortarConfig;
        private readonly EntityType _entityType;
        private readonly MineFactory _mineFactory;

        public Mortar(MortarConfig mortarConfig, EntityType entityType, MineFactory mineFactory, TimerFactory timerFactory)
        {
            _mortarConfig = mortarConfig;
            _entityType = entityType;
            _mineFactory = mineFactory;
            CooldownTimer = timerFactory.Create(mortarConfig.Cooldown);
        }

        public override void CommenceFiring(Vector2 minePosition, Quaternion turretDirection)
        {
            if (IsOnCooldown) return;
            if (_mineFactory == null)
                Debug.Log($"Проверка 0");
            else
                Debug.Log($"Проверка 1");

            //_mineFactory.Create(minePosition, _mortarConfig.MineConfig);

            CooldownTimer.Start();
        }
    }
}