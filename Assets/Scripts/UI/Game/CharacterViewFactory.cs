using Zenject;


namespace SpaceRogue.UI.Game
{
    public sealed class CharacterViewFactory : PlaceholderFactory<CharacterListView, CharacterView>
    {

        #region Fields

        private readonly DiContainer _diContainer;
        private readonly CharacterView _characterView;

        #endregion


        #region CodeLife

        public CharacterViewFactory(DiContainer diContainer, CharacterView characterView)
        {
            _diContainer = diContainer;
            _characterView = characterView;
        }

        #endregion


        #region Methods

        public override CharacterView Create(CharacterListView characterListView) 
            => _diContainer
                .InstantiatePrefabForComponent<CharacterView>(_characterView, characterListView.transform);

        #endregion

    }
}