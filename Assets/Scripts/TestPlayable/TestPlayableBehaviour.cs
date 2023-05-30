using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TestPlayableBehaviour : PlayableBehaviour
{
    public GameObject MyCamera;
    public float MyFloat;

    PlayableDirector director;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }

    public override void OnGraphStart(Playable playable)
    {
        director = (PlayableDirector)playable.GetGraph().GetResolver();

        foreach (var track in director.playableAsset.outputs)
        {
            //Debug.Log(track.streamName);
            if (track.streamName == "Attack Script" || track.streamName == "Victim Script")
            {
                ActorManager am= (ActorManager)director.GetGenericBinding(track.sourceObject);
                am.LockUnlockActorController(true);
            }
        }
    }


    public override void OnGraphStop(Playable playable)
    {
        foreach (var track in director.playableAsset.outputs)
        {
            //Debug.Log(track.streamName);
            if (track.streamName == "Attacker Script"|| track.streamName == "Victim Script")
            {
                ActorManager am = (ActorManager)director.GetGenericBinding(track.sourceObject);
                am.LockUnlockActorController(false);
            }
        }
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
