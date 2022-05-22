using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReaction : MonoBehaviour
{
    public FloatValue playerHealth;
    public SignalObserver healthSignal;

    public void Use(int amountToIncrease)
    {
        playerHealth.RunTimeValue += amountToIncrease;
        healthSignal.Raise();
    }
}
