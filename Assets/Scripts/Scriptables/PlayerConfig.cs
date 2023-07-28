using Gameplay.Movement;
using Gameplay.Survival;
using SpaceRogue.Gameplay.Shooting.Scriptables;
using System.Collections.Generic;
using UnityEngine;


namespace Scriptables
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Configs/Player/" + nameof(PlayerConfig))]
    public sealed class PlayerConfig : ScriptableObject
    {

        [SerializeField] private int _currentWeaponID = 0;

        [SerializeField] public List<MountedWeaponConfig> AvailableWeapons;
        [field: SerializeField] public UnitMovementConfig UnitMovement { get; private set; }
        [field: SerializeField] public EntitySurvivalConfig Survival { get; private set; }

        public MountedWeaponConfig CurrentWeapon => AvailableWeapons[_currentWeaponID];


        #region Methods

        public void ChangeWeapon(bool isNext)
        {
            if (isNext)
            {
                if (_currentWeaponID == (AvailableWeapons.Count - 1))
                {
                    _currentWeaponID = 0;
                    return;
                }

                _currentWeaponID = _currentWeaponID++;
            }
            else
            {
                if (_currentWeaponID == 0)
                {
                    _currentWeaponID = AvailableWeapons.Count - 1;
                    return;
                }

                _currentWeaponID = _currentWeaponID--;
            }

        }

        #endregion

    }
}