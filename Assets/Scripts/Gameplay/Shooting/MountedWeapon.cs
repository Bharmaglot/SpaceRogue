using SpaceRogue.Abstraction;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting
{
    public abstract class MountedWeapon
    {

        #region Properties

        public Weapon Weapon { get; private set; }
        protected Transform UnitViewTransform { get; private set; }

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