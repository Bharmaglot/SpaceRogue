using Abstracts;
using Gameplay.Damage;
using UnityEngine;


namespace Gameplay.Space.Star
{
    public class DamageZoneController : BaseController
    {
        public DamageZoneView DamageZoneView { get; }

        private int RepeatableDamage;
        

        public DamageZoneController(DamageZoneView damageZoneView, Transform spaceObjectParent, int damage, float cooldownDamage)
        {
            DamageZoneView = damageZoneView;
            DamageZoneView.transform.parent = spaceObjectParent;
            RepeatableDamage = damage;
            DamageZoneView.CooldownDamage = cooldownDamage;

            var DamageModel = new DamageModel(RepeatableDamage);

            DamageZoneView.Init(DamageModel);

            AddGameObject(DamageZoneView.gameObject);
        }

        
    }
}
