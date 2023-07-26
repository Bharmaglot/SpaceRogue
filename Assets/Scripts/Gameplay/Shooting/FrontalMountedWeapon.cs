using SpaceRogue.Abstraction;
using SpaceRogue.Gameplay.Shooting.Factories;
using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting
{
    public sealed class FrontalMountedWeapon : MountedWeapon
    {

        #region Fields

        private const float GUN_POSITION_CORRECTION = 0.7f;

        private readonly Transform _gunPointViewTransform;

        #endregion


        #region CodeLife

        public FrontalMountedWeapon(
            Weapon weapon,
            EntityViewBase entityView,
            GunPointViewFactory gunPointViewFactory) : base(weapon, entityView)
        {
            var unitScale = UnitViewTransform.localScale;
            var gunPointPosition = UnitViewTransform.position
                + UnitViewTransform.TransformDirection(GUN_POSITION_CORRECTION * Mathf.Max(unitScale.x, unitScale.y) * Vector3.up);
            var gunPoint = gunPointViewFactory.Create(gunPointPosition, UnitViewTransform.rotation, UnitViewTransform);
            _gunPointViewTransform = gunPoint.transform;
        }

        #endregion


        #region Methods

        public override void CommenceFiring() => Weapon.CommenceFiring(_gunPointViewTransform.position, _gunPointViewTransform.rotation);

        #endregion

    }
}