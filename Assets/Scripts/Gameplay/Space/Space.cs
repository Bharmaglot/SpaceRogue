using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Space.SpaceObjects;
using Object = UnityEngine.Object;

namespace Gameplay.Space
{
    public class Space : IDisposable
    {
        private readonly List<SpaceObject> _spaceObjects;
        private readonly SpaceView _spaceView;

        public Space(List<SpaceObject> spaceObjects, SpaceView spaceView)
        {
            _spaceObjects = spaceObjects;
            _spaceView = spaceView;
        }
        
        public void Dispose()
        {
            if (!_spaceObjects.Any()) return;
            
            _spaceObjects.ForEach(so => so.Dispose());
            _spaceObjects.Clear();

            if (_spaceView is not null)
            {
                Object.Destroy(_spaceView.gameObject);
            }
        }
    }
}