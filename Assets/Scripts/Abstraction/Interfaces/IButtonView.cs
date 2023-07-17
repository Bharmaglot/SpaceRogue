using System;


namespace SpaceRogue.Abstraction
{
    public interface IButtonView
    {
        void Init(Action onClickAction);
    }
}