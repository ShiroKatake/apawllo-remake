using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private UnityEvent unityEvent;

    private void Awake() => gameEvent.Register(gameEventListener: this);
    private void OnDestroy() => gameEvent.Deregister(gameEventListener: this);
    // Update is called once per frame
    public void RaiseEvent() => unityEvent.Invoke();
}
