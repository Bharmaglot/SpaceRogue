using SpaceRogue.Abstraction;
using System;


namespace SpaceRogue.Enemy.Movement
{
    public sealed class EnemyInput : IUnitMovementInput, IUnitTurningInput, IUnitWeaponInput
    {

        #region Events

        public event Action<float> VerticalAxisInput;

#pragma warning disable 67
        public event Action<float> HorizontalAxisInput;
#pragma warning restore 67

        public event Action<float> TurnAxisInput;

        public event Action<bool> PrimaryFireInput;

#pragma warning disable 67
        public event Action<bool> AbilityInput;
#pragma warning restore 67

#pragma warning disable 67
        public event Action<bool> ChangeWeaponInput;
#pragma warning restore 67

        #endregion


        #region Metods

        public void Accelerate()
        {
            VerticalAxisInput?.Invoke(1.0f);
        }

        public void Decelerate()
        {
            VerticalAxisInput?.Invoke(-1.0f);
        }

        public void HoldSpeed()
        {
            VerticalAxisInput?.Invoke(0.0f);
        }

        public void TurnRight(float value = 1.0f)
        {
            TurnAxisInput?.Invoke(Math.Abs(value));
        }

        public void TurnLeft(float value = 1.0f)
        {
            TurnAxisInput?.Invoke(-Math.Abs(value));
        }

        public void StopTurning()
        {
            TurnAxisInput?.Invoke(0.0f);
        }

        public void Fire()
        {
            PrimaryFireInput?.Invoke(true);
        }

        #endregion

    }
}