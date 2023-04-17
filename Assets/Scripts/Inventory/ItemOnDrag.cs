using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    public Inventory inventory;

    private int currentItemIndex;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //记录起始点
        originalParent = transform.parent;
        currentItemIndex = originalParent.GetComponent<Slot>().slotIndex;


        //脱离起始点
        transform.SetParent(transform.parent.parent);
        //刷新
        transform.position = eventData.position;

        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //刷新
        transform.position = eventData.position;

        //检测射线
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject target = eventData.pointerCurrentRaycast.gameObject;


        if (target.name == "BG")
        {
            //归位
            transform.SetParent(originalParent);
            transform.position = originalParent.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }



        if (target.name == "ItemImage")
        {
            //交换
            transform.SetParent(target.transform.parent.parent);
            transform.position = target.transform.parent.parent.position;


            var temp = inventory.itemList[currentItemIndex];
            inventory.itemList[currentItemIndex] = inventory.itemList[target.GetComponentInParent<Slot>().slotIndex];
            inventory.itemList[target.GetComponentInParent<Slot>().slotIndex] = temp;


            target.transform.parent.SetParent(originalParent);
            target.transform.parent.position = originalParent.position;

            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;

        }

        if (target.name == "Slot(Clone)")
        {
            //换位
            transform.SetParent(target.transform);
            transform.position = target.transform.position;

            //修改itemlist
            inventory.itemList[target.GetComponentInParent<Slot>().slotIndex] = inventory.itemList[currentItemIndex];

            //修改itemlist
            if (target.GetComponentInParent<Slot>().slotIndex != currentItemIndex)
            {
                inventory.itemList[currentItemIndex] = null;
            }

            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            //归位
            transform.SetParent(originalParent);
            transform.position = originalParent.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }



    }



}
