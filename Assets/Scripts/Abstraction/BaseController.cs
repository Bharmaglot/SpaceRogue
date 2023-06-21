using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.ResourceManagement;
using Object = UnityEngine.Object;
using UnityDebug = UnityEngine.Debug;


namespace SpaceRogue.Abstraction
{
    public abstract class BaseController : IDisposable
    {
        private List<BaseController> _baseControllers;
        private List<GameObject> _gameObjects;
        private bool _isDisposed;


        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            DisposeBaseControllers();
            DisposeGameObjects();

            OnDispose();
        }

        private void DisposeBaseControllers()
        {
            if (_baseControllers == null)
                return;

            foreach (BaseController baseController in _baseControllers)
                baseController?.Dispose();

            _baseControllers.Clear();
        }

        private void DisposeGameObjects()
        {
            if (_gameObjects == null)
                return;

            foreach (GameObject gameObject in _gameObjects)
                Object.Destroy(gameObject);

            _gameObjects.Clear();
        }

        protected virtual void OnDispose() { }


        protected void AddController(BaseController baseController)
        {
            _baseControllers ??= new List<BaseController>();
            _baseControllers.Add(baseController);
        }

        protected void AddGameObject(GameObject gameObject)
        {
            _gameObjects ??= new List<GameObject>();
            _gameObjects.Add(gameObject);
        }
        
        protected TView LoadView<TView>(ResourcePath path)
        {
            GameObject prefab = ResourceLoader.LoadPrefab(path);
            GameObject viewObject = Object.Instantiate(prefab);
            AddGameObject(viewObject);

            TView view = viewObject.GetComponent<TView>();
            return view;
        }
        
        protected TView LoadView<TView>(ResourcePath path, Vector3 position)
        {
            GameObject prefab = ResourceLoader.LoadPrefab(path);
            GameObject viewObject = Object.Instantiate(prefab, position, Quaternion.identity);
            AddGameObject(viewObject);

            TView view = viewObject.GetComponent<TView>();
            return view;
        }

        protected void Debug(string info)
        {
            UnityDebug.Log(info);
        }
    }
}