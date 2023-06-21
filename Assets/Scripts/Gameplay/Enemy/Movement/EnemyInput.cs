using SpaceRogue.Abstraction;
using System;


namespace SpaceRogue.Enemy.Movement
{
    public sealed class EnemyInput : IUnitMovementInput, IUnitTurningInput, IUnitWeaponInput
    {

        #region Events

        public event Action<float> VerticalAxisInput;
        public event Action<float> HorizontalAxisInput;

        public event Action<float> TurnAxisInput;

        public event Action<bool> PrimaryFireInput;
        public event Action<bool> ChangeWeaponInput;

        #endregion


        #region Metods
                
        public void Accelerate()
        {
            VerticalAxisInput(1);
        }
        
        public void Decelerate()
        {
            VerticalAxisInput(-1);
        }
        
        public void HoldSpeed()
        {
            VerticalAxisInput(0);
        }
        
        public void TurnRight(float value = 1.0f)
        {
            TurnAxisInput?.Invoke(Math.Abs(value));
            //HorizontalAxisInput(Math.Abs(value));
        }
        
        public void TurnLeft(float value = 1.0f)
        {
            TurnAxisInput?.Invoke(-Math.Abs(value));
            //HorizontalAxisInput(-Math.Abs(value));
        }
        
        public void StopTurning()
        {
            TurnAxisInput?.Invoke(0);
            //HorizontalAxisInput(0);
        }

        public void Fire()
        {
            PrimaryFireInput.Invoke(true);
        }

        #endregion

    }
}