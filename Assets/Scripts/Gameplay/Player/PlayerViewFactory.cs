using Zenject;

namespace Gameplay.Player
{
    public sealed class PlayerViewFactory : PlaceholderFactory<PlayerView>
    {
        private readonly DiContainer _diContainer;
        private readonly PlayerView _playerViewPrefab;

        public PlayerViewFactory(DiContainer diContainer, PlayerView playerViewPrefab)
        {
            _diContainer = diContainer;
            _playerViewPrefab = playerViewPrefab;
        }
        
        public override PlayerView Create()
        {
            return _diContainer
                .InstantiatePrefabForComponent<PlayerView>(_playerViewPrefab);
        }
    }
}