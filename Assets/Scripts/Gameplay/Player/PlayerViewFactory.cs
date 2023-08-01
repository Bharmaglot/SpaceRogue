using Zenject;


namespace SpaceRogue.Gameplay.Player
{
    public sealed class PlayerViewFactory : PlaceholderFactory<PlayerView>
    {

        #region Fields

        private readonly DiContainer _diContainer;
        private readonly PlayerView _playerViewPrefab;

        #endregion


        #region CodeLife

        public PlayerViewFactory(DiContainer diContainer, PlayerView playerViewPrefab)
        {
            _diContainer = diContainer;
            _playerViewPrefab = playerViewPrefab;
        }

        #endregion


        #region Methods

        public override PlayerView Create() 
            => _diContainer.InstantiatePrefabForComponent<PlayerView>(_playerViewPrefab);

        #endregion

    }
}