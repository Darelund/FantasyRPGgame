using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public SignalObserver signal;
    public UnityEvent signalEvent;

    public void OnSignalRaised()
    {


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