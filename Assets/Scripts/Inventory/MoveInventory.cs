using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveInventory : MonoBehaviour,IDragHandler
{
    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform=GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition +=eventData.delta;
    }


}
