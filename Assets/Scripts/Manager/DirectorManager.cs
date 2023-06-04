using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class DirectorManager : IActorManagerInterface
{

    public PlayableDirector pd;

    public TimelineAsset frontStab;

    public ActorManager attacker;
    public ActorManager victim;


    // Start is called before the first frame update
    void Start()
    {
        pd = GetComponent<PlayableDirector>();
        pd.playOnAwake = false;
        //pd.playableAsset=Instantiate(frontStab);




    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            pd.Play();
        }
    }

    public void PlayFrontStab(string timelineName, ActorManager attacker, ActorManager victim)
    {
        if (pd.playableAsset!=null)
        {
            return;
        }


        if (timelineName == "frontStab")
        {
            pd.playableAsset = Instantiate(frontStab);
        }

        TimelineAsset timeline = (TimelineAsset)pd.playableAsset;



        foreach (var track in timeline.GetOutputTracks())
        {
            if (track.name == "Attack Script")
            {
                pd.SetGenericBinding(track, attacker);
                foreach (var clip in track.GetClips())
                {
                    TestPlayableClip myclip = (TestPlayableClip)clip.asset;
                    TestPlayableBehaviour mybehav= myclip.template;
                    mybehav.MyFloat = 666;
                    pd.SetReferenceValue(myclip.am.exposedName, attacker);
                }
            }
            else if (track.name == "Victim Script")
            {
                pd.SetGenericBinding(track, victim);
                foreach (var clip in track.GetClips())
                {
                    TestPlayableClip myclip = (TestPlayableClip)clip.asset;
                    TestPlayableBehaviour mybehav = myclip.template;
                    mybehav.MyFloat = 777;

                    Debug.Log(GameObject.Find("B").transform.position);
                    pd.SetReferenceValue(myclip.am.exposedName, victim);
                }
            }
            else if (track.name == "Attack Animation")
            {
                pd.SetGenericBinding(track, attacker.yc.anim);
            }
            else if (track.name == "Victim Animation")
            {
                pd.SetGenericBinding(track, victim.yc.anim);
            }


        }



        //foreach (var teackBinding in pd.playableAsset.outputs)
        //{
        //    if (teackBinding.streamName == "Attack Script")
        //    {
        //        pd.SetGenericBinding(teackBinding.sourceObject, attacker);
        //    }
        //    else if (teackBinding.streamName == "Victim Script")
        //    {
        //        pd.SetGenericBinding(teackBinding.sourceObject, victim);
        //    }
        //    else if (teackBinding.streamName == "Attack Animation")
        //    {
        //        pd.SetGenericBinding(teackBinding.sourceObject, attacker.yc.anim);
        //    }
        //    else if (teackBinding.streamName == "Victim Animation")
        //    {
        //        pd.SetGenericBinding(teackBinding.sourceObject, victim.yc.anim);
        //    }
        //}

        pd.Evaluate();

        pd.Play();
    }


}
