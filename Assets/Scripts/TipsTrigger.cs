using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TipsTrigger : MonoBehaviour
{
    private BoxCollider boxCollider;


    [TextArea]
    public string tipsText;

    public string tipsTitlesText;


    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger=true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("´¥·¢tips£º"+tipsText);

            ShowTips();
        }
    }


    private void ShowTips()
    {
        TipsPanelController.Instance.SetTips(tipsTitlesText,tipsText);
        TipsPanelController.Instance.Open();
        
        Destroy(this.gameObject);
    }


}
