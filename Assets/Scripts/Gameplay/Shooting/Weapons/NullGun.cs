using UnityEngine;


namespace SpaceRogue.Gameplay.Shooting.Weapons
{
    public sealed class NullGun : Weapon
    {
        public override void CommenceFiring(Vector2 bulletPosition, Quaternion turretRotation) 
            => Debug.Log($"Null-gun has fired, bullet position {bulletPosition}, direction {turretRotation.eulerAngles}!");
    }
}