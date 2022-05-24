using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    [SerializeField]
    PlayableDirector PlayableDirector;

    public void Play(float time)
    {
        PlayableDirector.time = time;
    }

}
