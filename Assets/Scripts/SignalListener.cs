using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public UnityEvent signalEvent;
    [SerializeField] public SignalObserver signal;
   

    public void OnSignalRaised()
    {
        signalEvent.Invoke();
    }

    private void OnEnable()
    {
        signal.RegisterListener(this);
    }

    private void OnDisable()
    {
        signal.DeRegisterListener(this);
    }
}
