using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0f, 0.2688679f, 0.5377358f)]
[TrackClipType(typeof(TestPlayableClip))]
[TrackBindingType(typeof(ActorManager))]
public class TestPlayableTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<TestPlayableMixerBehaviour>.Create (graph, inputCount);
    }
}
