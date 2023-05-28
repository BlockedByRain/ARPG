using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TestPlayableClip : PlayableAsset, ITimelineClipAsset
{
    public TestPlayableBehaviour template = new TestPlayableBehaviour ();
    public ExposedReference<Camera> MyCamera;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TestPlayableBehaviour>.Create (graph, template);
        TestPlayableBehaviour clone = playable.GetBehaviour ();
        clone.MyCamera = MyCamera.Resolve (graph.GetResolver ());
        return playable;
    }
}
