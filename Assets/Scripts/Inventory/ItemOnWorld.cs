using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item thisItem;
    public Inventory inventory;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(gameObject);
        }
    }

    public void AddNewItem()
    {
        if (!inventory.itemList.Contains(thisItem))
        {
            for (int i = 0; i < inventory.itemList.Count; i++)
            {
                if (inventory.itemList[i] == null)
                {
                    inventory.itemList[i] = thisItem;
                    break;
                }
            }
        }
        else
        {
            thisItem.itemNum += 1;
        }

        InventoryController.RefreshItem();
    }
}