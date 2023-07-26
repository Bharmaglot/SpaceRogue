using UnityEngine;


namespace SpaceRogue.Gameplay.Abilities
{
    public sealed class NullAbility : Ability
    {
        public override void UseAbility() 
            => Debug.Log($"Ability Used!");
    }
}