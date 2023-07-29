using Gameplay.Mechanics.Timer;
using Gameplay.Space.SpaceObjects;
using Gameplay.Space.SpaceObjects.SpaceObjectsEffects.Views;
using SpaceRogue.Scriptables.GameEvent;
using SpaceRogue.Services;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceRogue.Gameplay.GameEvent.Supernova
{
    public sealed class Supernova : IDisposable
    {

        #region Fields

        private const int COLLAPSE_SPEED = 25;
        private const float SCALE_COEFFICIENT = 1.1f;
        private const float SHOCKWAVE_RADIUS_COEFFICIENT = 0.9f;

        private readonly Updater _updater;
        private readonly SupernovaGameEventConfig _supernovaGameEventConfig;
        private readonly Timer _explosionTimer;
        private readonly SpaceObjectView _spaceObjectView;

        private readonly Color _starViewColor;
        private readonly Vector3 _starViewScale;

        private readonly List<AreaEffectView> _effectViews = new();
        private readonly DamageOnTouchEffectView _damageOnTouchEffect;
        private Color _currentColor;
        private Vector3 _currentScale;
        private bool _isSupernova;

        #endregion


        #region Properties

        public bool IsDestroyed { get; private set; } = false;

        #endregion


        #region CodeLife

        public Supernova(Updater updater, SupernovaGameEventConfig config, SpaceObjectView starView, TimerFactory timerFactory)
        {
            _updater = updater;
            _supernovaGameEventConfig = config;
            _spaceObjectView = starView;
            _starViewColor = _spaceObjectView.SpriteRenderer.color;
            _starViewScale = _spaceObjectView.transform.localScale;

            foreach (Transform child in _spaceObjectView.transform)
            {
                if (child.TryGetComponent(out AreaEffectView component))
                {
                    if (component is DamageOnTouchEffectView damageOnTouchEffect)
                    {
                        _damageOnTouchEffect = damageOnTouchEffect;
                    }
                    _effectViews.Add(component);
                }
            }

            _explosionTimer = timerFactory.Create(_supernovaGameEventConfig.TimeToExplosion);
            _explosionTimer.OnTick += PrepareSupernova;
            _explosionTimer.OnExpire += InitiateStartSupernova;
            _explosionTimer.Start();
        }

        public void Dispose()
        {
            _effectViews.Clear();
            _explosionTimer.OnTick -= PrepareSupernova;
            _explosionTimer.OnExpire -= InitiateStartSupernova;
            _explosionTimer.Dispose();
            _updater.UnsubscribeFromUpdate(StartSupernova);
            IsDestroyed = true;
        }

        #endregion


        #region Methods

        private void PrepareSupernova()
        {
            if (_spaceObjectView == null)
            {
                _explosionTimer.OnTick -= PrepareSupernova;
                Dispose();
                return;
            }

            ChangeColorRepeat(_spaceObjectView.SpriteRenderer, ref _currentColor, _supernovaGameEventConfig.WarningColor, _starViewColor);

            if (_explosionTimer.CurrentValue < 1f)
            {
                ChangeScale(_spaceObjectView.transform, ref _currentScale, _spaceObjectView.transform.localScale, _starViewScale * SCALE_COEFFICIENT);
            }
        }

        private void InitiateStartSupernova()
        {
            _explosionTimer.OnTick -= PrepareSupernova;

            foreach (var effectView in _effectViews)
            {
                if (effectView == _damageOnTouchEffect)
                {
                    continue;
                }
                effectView.Hide();
            }

            _updater.SubscribeToUpdate(StartSupernova);
        }

        private void StartSupernova()
        {
            if (_spaceObjectView == null)
            {
                _updater.UnsubscribeFromUpdate(StartSupernova);
                Dispose();
                return;
            }

            if (_spaceObjectView.transform.localScale.x <= 0)
            {
                _spaceObjectView.SpriteRenderer.sprite = _supernovaGameEventConfig.SupernovaSprite;
                _spaceObjectView.SpriteRenderer.color = _supernovaGameEventConfig.ShockwaveColor;
                _currentColor = _spaceObjectView.SpriteRenderer.color;
                _spaceObjectView.transform.localScale = Vector3.zero;
                _spaceObjectView.CircleCollider2D.isTrigger = true;
                _spaceObjectView.CircleCollider2D.offset = Vector2.zero;

                if (_damageOnTouchEffect != null)
                {
                    _damageOnTouchEffect.Init(new(_supernovaGameEventConfig.ShockwaveDamage));
                    _damageOnTouchEffect.SpriteRenderer.enabled = false;
                }

                _isSupernova = true;
            }

            if (_isSupernova)
            {
                if (_spaceObjectView.transform.localScale.x <= _supernovaGameEventConfig.ShockwaveRadius)
                {
                    if (_spaceObjectView.transform.localScale.x >= _supernovaGameEventConfig.ShockwaveRadius * SHOCKWAVE_RADIUS_COEFFICIENT)
                    {
                        ChangeColor(_spaceObjectView.SpriteRenderer, ref _currentColor,
                                    _spaceObjectView.SpriteRenderer.color, Color.clear,
                                    _supernovaGameEventConfig.ShockwaveSpeed * Time.deltaTime);
                        ChangeColor(_spaceObjectView.MinimapIconSpriteRenderer, ref _currentColor,
                                    _spaceObjectView.MinimapIconSpriteRenderer.color, Color.clear,
                                    _supernovaGameEventConfig.ShockwaveSpeed * Time.deltaTime);
                    }
                    ChangeScale(_spaceObjectView.transform, ref _currentScale, _spaceObjectView.transform.localScale,
                                Vector3.one * (_supernovaGameEventConfig.ShockwaveRadius + 1),
                                _supernovaGameEventConfig.ShockwaveSpeed);
                    PushAllRigidbodies(_spaceObjectView.CircleCollider2D, _spaceObjectView.transform.position,
                                       _supernovaGameEventConfig.ShockwaveForce);
                    return;
                }

                _spaceObjectView.transform.localScale = Vector3.one * _supernovaGameEventConfig.ShockwaveRadius;
                _spaceObjectView.SpriteRenderer.color = Color.clear;
                _spaceObjectView.CircleCollider2D.enabled = false;
                _spaceObjectView.MinimapIconSpriteRenderer.enabled = false;

                if (_damageOnTouchEffect != null)
                {
                    _damageOnTouchEffect.Hide();
                }

                _updater.UnsubscribeFromUpdate(StartSupernova);
                Dispose();
                //TODO Add BlackHole
                return;
            }

            ChangeScale(_spaceObjectView.transform, ref _currentScale, _spaceObjectView.transform.localScale,
                        -Vector3.one, COLLAPSE_SPEED);
        }

        private void ChangeColorRepeat(
            SpriteRenderer spriteRenderer, ref Color currentColor, Color a, Color b, float timeCoefficient = 1.0f)
            => ChangeColor(spriteRenderer, ref currentColor, a, b, Mathf.PingPong(Time.time * timeCoefficient, 1.0f));

        private void ChangeColor(SpriteRenderer spriteRenderer, ref Color currentColor, Color a, Color b, float t)
        {
            currentColor = Color.Lerp(a, b, t);
            spriteRenderer.color = currentColor;
        }

        private void ChangeScale(Transform transform, ref Vector3 currentScale, Vector3 a, Vector3 b, float speed = 1.0f)
        {
            currentScale = Vector3.Lerp(a, b, speed * Time.deltaTime);
            transform.localScale = currentScale;
        }

        private void PushAllRigidbodies(Collider2D collider, Vector3 position, float shockwaveForce)
        {
            var colliders = new List<Collider2D>();
            Physics2D.OverlapCollider(collider, new ContactFilter2D().NoFilter(), colliders);

            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out Rigidbody2D rigidbody))
                {
                    var direction = (item.transform.position - position).normalized;
                    rigidbody.AddForce(shockwaveForce * direction, ForceMode2D.Impulse);
                }
            }
        }

        #endregion

    }
}