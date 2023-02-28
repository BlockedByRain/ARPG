using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TesterDirctor : MonoBehaviour
{
    public PlayableDirector pd;

    //攻击者
    public Animator attacker;
    //被攻击者
    public Animator victim;

    // Start is called before the first frame update
    void Start()
    {
        pd= GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            foreach (var track in pd.playableAsset.outputs)
            {
                //print(track.streamName);
                //绑定演出者
                if (track.streamName=="Attacker Animation")
                {
                    pd.SetGenericBinding(track.sourceObject, attacker);
                }
                else if (track.streamName=="Victim Animation")
                {
                    pd.SetGenericBinding(track.sourceObject, victim);

                }

            }

            


            //随时打断
            pd.time = 0;
            pd.Stop();
            pd.Evaluate();
            pd.Play();
        }
    }
}
