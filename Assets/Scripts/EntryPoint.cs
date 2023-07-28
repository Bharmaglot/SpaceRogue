using System;
using UnityEngine;

public sealed class EntryPoint : MonoBehaviour
{

    #region UpdateMechanism

    private static event Action OnUpdate = () => { };
    private static event Action<float> OnDeltaTimeUpdate = (_) => { };
    private static event Action OnFixedUpdate = () => { };
    private static event Action OnLateUpdate = () => { };
    
    public static void SubscribeToUpdate(Action callback) => OnUpdate += callback;
    public static void UnsubscribeFromUpdate(Action callback) => OnUpdate -= callback;
    public static void SubscribeToUpdate(Action<float> callback) => OnDeltaTimeUpdate += callback;
    public static void UnsubscribeFromUpdate(Action<float> callback) => OnDeltaTimeUpdate -= callback;
    
    public static void SubscribeToFixedUpdate(Action callback) => OnFixedUpdate += callback;
    public static void UnsubscribeFromFixedUpdate(Action callback) => OnFixedUpdate -= callback;    
    
    public static void SubscribeToLateUpdate(Action callback) => OnLateUpdate += callback;
    public static void UnsubscribeFromLateUpdate(Action callback) => OnLateUpdate -= callback;
    
    private void Update()
    {
        OnUpdate.Invoke();
        OnDeltaTimeUpdate.Invoke(Time.deltaTime);
    }
    private void FixedUpdate()
    {
        OnFixedUpdate.Invoke();
    }
    private void LateUpdate()
    {
        OnLateUpdate.Invoke();
    }

    #endregion
    
    
}
