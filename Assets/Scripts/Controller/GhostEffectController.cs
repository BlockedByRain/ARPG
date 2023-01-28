using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffectController : MonoBehaviour
{
    public GhostEffect[] effects;
   
    // Start is called before the first frame update
    void Start()
    {
        effects= GetComponentsInChildren<GhostEffect>();       

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
