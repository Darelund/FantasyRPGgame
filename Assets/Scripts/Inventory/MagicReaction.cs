using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicReaction : MonoBehaviour
{
    public FloatValue playerMagic;
    public SignalObserver magicSignal;

    public void Use(int amountToIncrease)
    {
        playerMagic.RunTimeValue += amountToIncrease;
        magicSignal.Raise();
    }
}
