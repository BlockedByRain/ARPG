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
        //��¼��ʼ��
        originalParent = transform.parent;
        currentItemIndex = originalParent.GetComponent<Slot>().slotIndex;


        //������ʼ��
        transform.SetParent(transform.parent.parent);
        //ˢ��
        transform.position = eventData.position;

        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //ˢ��
        transform.position = eventData.position;

        //�������
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject target = eventData.pointerCurrentRaycast.gameObject;


        if (target.name == "BG")
        {
            //��λ
            transform.SetParent(originalParent);
            transform.position = originalParent.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }



        if (target.name == "ItemImage")
        {
            //����
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
            //��λ
            transform.SetParent(target.transform);
            transform.position = target.transform.position;

            //�޸�itemlist
            inventory.itemList[target.GetComponentInParent<Slot>().slotIndex] = inventory.itemList[currentItemIndex];

            //�޸�itemlist
            if (target.GetComponentInParent<Slot>().slotIndex != currentItemIndex)
            {
                inventory.itemList[currentItemIndex] = null;
            }

            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            //��λ
            transform.SetParent(originalParent);
            transform.position = originalParent.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }



    }



}
