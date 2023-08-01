using SpaceRogue.Gameplay.Player;
using SpaceRogue.Gameplay.Player.Character;
using SpaceRogue.UI.Game;
using System;
using System.Collections.Generic;


namespace SpaceRogue.UI.Services
{
    public sealed class CharacterListService : IDisposable
    {

        #region Fields

        private readonly CharacterListView _characterListView;
        private readonly CharacterFactory _characterFactory;
        private readonly PlayerFactory _playerFactory;
        private readonly CharacterViewFactory _characterViewFactory;

        private Gameplay.Player.Player _player;
        private readonly List<CharacterView> _characterViews = new();
        private List<Character> _characters = new();

        #endregion


        #region CodeLife

        public CharacterListService(
            CharacterListView characterListView,
            CharacterFactory characterFactory,
            PlayerFactory playerFactory,
            CharacterViewFactory characterViewFactory)
        {
            _characterListView = characterListView;
            _characterFactory = characterFactory;
            _playerFactory = playerFactory;
            _characterViewFactory = characterViewFactory;

            _characterFactory.OnCharactersCreated += OnCharactersCreatedHandler;
            _playerFactory.OnPlayerSpawned += OnPlayerSpawnedHandler;
        }

        public void Dispose()
        {
            _characterFactory.OnCharactersCreated -= OnCharactersCreatedHandler;
            _playerFactory.OnPlayerSpawned -= OnPlayerSpawnedHandler;
            _player.OnCharacterChange -= OnCharacterChanged;
            UnsubscribesFromCharacters();

            foreach (var characterView in _characterViews)
            {
                if (characterView != null)
                {
                    UnityEngine.Object.Destroy(characterView.gameObject);
                }
            }
            _characterViews.Clear();
            _characters.Clear();
        }

        #endregion


        #region Methods

        private void OnCharactersCreatedHandler(List<Character> characters)
        {
            if (_characters.Count > 0)
            {
                UnsubscribesFromCharacters();
                _characters.Clear();
            }

            _characters = characters;

            if (_characterViews.Count == 0)
            {
                for (var i = 0; i < _characters.Count; i++)
                {
                    var characterView = _characterViewFactory.Create(_characterListView);
                    characterView.Hide();
                    _characterViews.Add(characterView);
                }
            }

            SetupCharacterViews();
        }

        private void OnPlayerSpawnedHandler(Gameplay.Player.Player player)
        {
            if (_player != null)
            {
                _player.OnCharacterChange -= OnCharacterChanged;
            }

            _player = player;
            _player.OnCharacterChange += OnCharacterChanged;

            OnCharacterChanged(_player.CurrentCharacter);
        }

        private void OnCharacterChanged(Character currentCharacter)
        {
            for (var i = 0; i < _characters.Count; i++)
            {
                _characterViews[i].SetPanelActive(_characters[i] == currentCharacter);
            }
        }

        private void SetupCharacterViews()
        {
            for (var i = 0; i < _characters.Count; i++)
            {
                _characterViews[i].Init(_characters[i].Name, _characters[i].CharacterIcon);

                _characterViews[i].PlayerStatusBarView.HealthBar.Init(
                0.0f,
                _characters[i].Survival.EntityHealth.MaximumHealth,
                _characters[i].Survival.EntityHealth.CurrentHealth);

                _characters[i].Survival.EntityHealth.HealthChanged += UpdateHealthBar;

                if (_characters[i].Survival.EntityShield != null)
                {
                    _characterViews[i].PlayerStatusBarView.ShieldBar.Init(
                        0.0f,
                        _characters[i].Survival.EntityShield.MaximumShield,
                        _characters[i].Survival.EntityShield.CurrentShield);

                    _characters[i].Survival.EntityShield.ShieldChanged += UpdateShieldBar;
                }

                _characterViews[i].SetPanelActive(false);
                _characterViews[i].Show();
            }
        }

        private void UnsubscribesFromCharacters()
        {
            for (var i = 0; i < _characters.Count; i++)
            {
                if (_characters[i].Survival != null)
                {
                    _characters[i].Survival.EntityHealth.HealthChanged -= UpdateHealthBar;

                    if (_characters[i].Survival.EntityShield != null)
                    {
                        _characters[i].Survival.EntityShield.ShieldChanged -= UpdateShieldBar;
                    }
                }
            }
        }

        private void UpdateHealthBar()
        {
            for (var i = 0; i < _characters.Count; i++)
            {
                _characterViews[i].PlayerStatusBarView
                    .HealthBar.UpdateValue(_characters[i].Survival.EntityHealth.CurrentHealth);
            }
        }

        private void UpdateShieldBar()
        {
            for (var i = 0; i < _characters.Count; i++)
            {
                _characterViews[i].PlayerStatusBarView
                    .ShieldBar.UpdateValue(_characters[i].Survival.EntityShield.CurrentShield);
            }
        }

        #endregion

    }
}