using Abstracts;
using Gameplay.Damage;
using UnityEngine;


namespace Gameplay.Space.Star
{
    public class DamageZoneController : BaseController
    {
        public DamageZoneView DamageZoneView { get; }

        

        public DamageZoneController(DamageZoneView damageZoneView, Transform spaceObjectParent, int damage, float cooldownDamage)
        {
            DamageZoneView = damageZoneView;
            DamageZoneView.transform.parent = spaceObjectParent;
            DamageZoneView.DamageCooldown = cooldownDamage;

            var DamageModel = new DamageModel(damage);

            DamageZoneView.Init(DamageModel);

            AddGameObject(DamageZoneView.gameObject);
        }

        
    }
}
