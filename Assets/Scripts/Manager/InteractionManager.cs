using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : IActorManagerInterface
{
    private CapsuleCollider interCol;

    public List<EventCasterManager> overlapEcastms =new List<EventCasterManager>();

    // Start is called before the first frame update
    void Start()
    {
        interCol = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        EventCasterManager[] ecms=col.GetComponents<EventCasterManager>();
        foreach (var ecm in ecms)
        {
            if (!overlapEcastms.Contains(ecm))
            {
                overlapEcastms.Add(ecm);
            }
        }
    }


    private void OnTriggerExit(Collider col)
    {
        EventCasterManager[] ecms = col.GetComponents<EventCasterManager>();
        foreach (var ecm in ecms)
        {
            if (overlapEcastms.Contains(ecm))
            {
                overlapEcastms.Remove(ecm);
            }
        }
    }


}
