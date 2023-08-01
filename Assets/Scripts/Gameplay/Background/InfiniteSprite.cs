using UnityEngine;


namespace SpaceRogue.Gameplay.Background
{
    public sealed class InfiniteSprite : ParallaxEffect
    {

        #region Fields

        private readonly SpriteRenderer _spriteRenderer;

        private readonly float _textureUnitSizeX;
        private readonly float _textureUnitSizeY;

        #endregion


        #region CodeLife

        public InfiniteSprite(
            Transform cameraTransform,
            SpriteRenderer spriteRenderer,
            float coefficient) : base(cameraTransform, spriteRenderer.transform, coefficient)
        {
            _spriteRenderer = spriteRenderer;

            _textureUnitSizeX = _spriteRenderer.sprite.texture.width
                / _spriteRenderer.sprite.pixelsPerUnit
                * _spriteRenderer.transform.localScale.x;
            _textureUnitSizeY = _spriteRenderer.sprite.texture.height 
                / _spriteRenderer.sprite.pixelsPerUnit
                * _spriteRenderer.transform.localScale.y;
        }

        #endregion


        #region Methods

        protected override void OptionalExecute()
        {
            var xPosition = _spriteRenderer.transform.position.x;
            var yPosition = _spriteRenderer.transform.position.y;

            var delta = _cameraTransform.position - _spriteRenderer.transform.position;

            if (Mathf.Abs(delta.x) >= _textureUnitSizeX)
            {
                var offsetX = delta.x % _textureUnitSizeX;
                xPosition = _cameraTransform.position.x + offsetX;
            }
            if (Mathf.Abs(delta.y) >= _textureUnitSizeY)
            {
                var offsetY = delta.y % _textureUnitSizeY;
                yPosition = _cameraTransform.position.y + offsetY;
            }

            _spriteRenderer.transform.position = new(xPosition, yPosition);
        }

        #endregion

    }
}
