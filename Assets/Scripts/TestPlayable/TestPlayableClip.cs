using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TestPlayableClip : PlayableAsset, ITimelineClipAsset
{
    public TestPlayableBehaviour template = new TestPlayableBehaviour ();
    public ExposedReference<ActorManager> am;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TestPlayableBehaviour>.Create (graph, template);
        TestPlayableBehaviour clone = playable.GetBehaviour ();
        am.exposedName = GetInstanceID().ToString();
        clone.am = am.Resolve (graph.GetResolver ());
        return playable;
    }
}
