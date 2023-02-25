using Abstracts;
using Gameplay.Damage;
using UnityEngine;

namespace Gameplay.Space.Star
{
    public sealed class SpaceObjectController : BaseController
    {
        public SpaceObjectView SpaceObjectView { get; }

        private const int FatalDamage = 9999;

        public SpaceObjectController(SpaceObjectView spaceObjectView, Transform spaceObjectParent)
        {
            SpaceObjectView = spaceObjectView;
            SpaceObjectView.transform.parent = spaceObjectParent;

            var damageModel = new DamageModel(FatalDamage);
            spaceObjectView.Init(damageModel);

            AddGameObject(spaceObjectView.gameObject);
        }

    }
}