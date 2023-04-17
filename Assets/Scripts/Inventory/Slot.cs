using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotIndex;
    public Item slotItem;
    public Image slotImage;
    public Text slotNum;
    public string slotInfo;

    public GameObject itemInSlot;

    public void ItemOnClicked()
    {
        InventoryController.UpdateItemInfo(slotInfo);
    }

    public void SetupSlot(Item item)
    {
        if (item==null)
        {
            itemInSlot.SetActive(false);
            return;
        }
        else
        {
            slotImage.sprite = item.itemImage;
            slotNum.text=item.itemNum.ToString();
            slotInfo = item.itemInfo;
        }
    }

}
