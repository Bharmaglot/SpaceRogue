using Abstracts;
using UnityEngine;
using Scriptables.Space;

namespace Gameplay.Space.Star
{
    public class GravityController : BaseController
    {
        public GravityView GravityView { get; }
        public PointEffector2D PointEffector;
        private readonly GravityConfig _gravityConfig;

        public GravityController(GravityView gravityView, Transform BlackHoleParent, GravityConfig gravityConfig)
        {
            GravityView = gravityView;
            GravityView.transform.parent = BlackHoleParent;
            PointEffector = gravityView.GetComponent<PointEffector2D>();
            PointEffector.forceMagnitude = -gravityConfig.ForceGravity;


            AddGameObject(GravityView.gameObject);
        }

    }
}