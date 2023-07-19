using SpaceRogue.Abstraction;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting
{
    public abstract class MountedWeapon
    {
        #region Properties

        protected Weapon Weapon { get; set; }
        protected Transform UnitViewTransform { get; set; }

        #endregion

        #region CodeLife

        public MountedWeapon(Weapon weapon, EntityViewBase entityView)
        {
            Weapon = weapon;
            UnitViewTransform = entityView.transform;
        }

        #endregion

        #region Methods

        public abstract void CommenceFiring();

        #endregion
    }
}