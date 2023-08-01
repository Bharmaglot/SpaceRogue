using Gameplay.Movement;
using SpaceRogue.InputSystem;
using SpaceRogue.Player.Movement;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceRogue.Gameplay.Player
{
    public sealed class Player : IDisposable
    {

        #region Events

        public event Action PlayerDestroyed;
        public event Action PlayerDisposed;

        public event Action<Character.Character> OnCharacterChange;

        #endregion


        #region Fields

        private int _currentCharacterID;

        private readonly UnitMovement _unitMovement;
        private readonly UnitTurningMouse _unitTurningMouse;
        private readonly List<Character.Character> _characters;
        private readonly List<bool> _destroyedCharacters = new();
        private readonly PlayerInput _playerInput;

        private bool _disposing;

        #endregion


        #region Properties

        public PlayerView PlayerView { get; }
        public Character.Character CurrentCharacter => _characters[_currentCharacterID];

        #endregion


        #region CodeLife

        public Player(
            PlayerView playerView,
            UnitMovement unitMovement,
            UnitTurningMouse unitTurningMouse,
            List<Character.Character> characters,
            PlayerInput playerInput)
        {
            PlayerView = playerView;
            _unitMovement = unitMovement;
            _unitTurningMouse = unitTurningMouse;
            _characters = characters;

            _playerInput = playerInput;
            _currentCharacterID = 0;

            foreach (var character in _characters)
            {
                character.CharacterDestroyed += OnCharacterDestroyed;
                _destroyedCharacters.Add(false);
            }

            _playerInput.ChangeCharacterInput += ChangeCharacterInputHandler;
        }

        public void Dispose()
        {
            if (_disposing)
            {
                return;
            }

            _disposing = true;

            _playerInput.ChangeCharacterInput -= ChangeCharacterInputHandler;

            PlayerDisposed?.Invoke();

            _unitMovement.Dispose();
            _unitTurningMouse.Dispose();

            foreach (var character in _characters)
            {
                character.CharacterDestroyed -= OnCharacterDestroyed;
                character.Dispose();
            }
            _characters.Clear();
            _destroyedCharacters.Clear();

            if (PlayerView != null)
            {
                UnityEngine.Object.Destroy(PlayerView.gameObject);
            }
        }

        #endregion


        #region Methods

        public void SetStartPosition(Vector2 position)
        {
            _unitMovement.StopMoving();
            PlayerView.transform.position = position;
        }

        private void OnCharacterDestroyed(Character.Character destroyedCharacter)
        {
            for (var i = 0; i < _characters.Count; i++)
            {
                if (_characters[i] == destroyedCharacter)
                {
                    _destroyedCharacters[i] = true;
                }
            }

            if (AreAllCharactersDestroyed())
            {
                return;
            }
            ChangeCharacterInputHandler(true);
        }

        private bool AreAllCharactersDestroyed()
        {
            var count = 0;
            foreach (var destroyedCharacter in _destroyedCharacters)
            {
                if (destroyedCharacter)
                {
                    count++;
                }
            }

            if (count == _destroyedCharacters.Count)
            {
                PlayerDestroyed?.Invoke();
                Dispose();
                return true;
            }
            return false;
        }

        private void ChangeCharacterInputHandler(bool isNextWeapon)
        {
            if (AreAllCharactersDestroyed())
            {
                return;
            }

            _characters[_currentCharacterID].SetCharacterActive(false);

            foreach (var _  in _characters)
            {
                if (isNextWeapon)
                {
                    _currentCharacterID++;

                    if (_currentCharacterID == _characters.Count)
                    {
                        _currentCharacterID = 0;
                    }
                }
                else
                {
                    _currentCharacterID--;

                    if (_currentCharacterID < 0)
                    {
                        _currentCharacterID = _characters.Count - 1;
                    }
                }

                if (!_destroyedCharacters[_currentCharacterID])
                {
                    break;
                }
            }

            SetSpaceshipSprite(_characters[_currentCharacterID].SpaceshipSprite);
            _characters[_currentCharacterID].SetCharacterActive(true);

            OnCharacterChange?.Invoke(_characters[_currentCharacterID]);
        }

        private void SetSpaceshipSprite(Sprite spaceshipSprite) => PlayerView.SpriteRenderer.sprite = spaceshipSprite;

        #endregion

    }
}