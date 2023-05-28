using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TestPlayableBehaviour : PlayableBehaviour
{
    public Camera MyCamera;
    public float MyFloat;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }

    public override void OnGraphStart(Playable playable)
    {
        
    }


    public override void OnGraphStop(Playable playable)
    {
        
    }


    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {

    }


    public override void OnBehaviourPause(Playable playable, FrameData info)
    {

    }


    public override void PrepareFrame(Playable playable, FrameData info)
    {

    }

}
