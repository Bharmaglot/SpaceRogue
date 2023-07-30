using SpaceRogue.Gameplay.Camera;
using UnityEngine;
using Zenject;


namespace SpaceRogue.Gameplay.Installers
{
    public sealed class CameraInstaller : MonoInstaller
    {

        #region Properties

        [field: SerializeField] public CameraView GameCameraView { get; private set; }

        #endregion


        #region Methods

        public override void InstallBindings()
        {
            InstallGameCamera();
        }

        private void InstallGameCamera()
        {
            Container
                .Bind<CameraView>()
                .FromInstance(GameCameraView)
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<GameCamera>()
                .AsSingle()
                .NonLazy();
        }

        #endregion

    }
}