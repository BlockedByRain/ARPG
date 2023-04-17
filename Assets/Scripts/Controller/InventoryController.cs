using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoSingleton<InventoryController>
{
    public GameObject inventoryPanel;

    public bool isOpen;


    public Inventory inventory;
    public GameObject slotGrid;
    public GameObject slotPrefab;
    public Text itemInfo;

    public List<GameObject> slots = new List<GameObject>();

    private void OnEnable()
    {
        RefreshItem();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            OpenInventory();
        }


        UpdateCursor();
    }




    public static void RefreshItem()
    {
        //删除所有子物体并重新生成来实现刷新数据，会有性能问题，不过这样比较简单
        for (int i = 0; i < Instance.slotGrid.transform.childCount; i++)
        {
            if (Instance.slotGrid.transform.childCount == 0)
            {
                break;
            }
            Destroy(Instance.slotGrid.transform.GetChild(i).gameObject);
            Instance.slots.Clear();
        }

        //重新生成
        for (int i = 0; i < Instance.inventory.itemList.Count; i++)
        {

            Instance.slots.Add(Instantiate(Instance.slotPrefab));
            Instance.slots[i].transform.SetParent(Instance.slotGrid.transform);
            Instance.slots[i].GetComponent<Slot>().slotIndex = i;

            //修正大小
            Instance.slots[i].transform.localScale = new Vector3(1, 1, 1);

            Instance.slots[i].GetComponent<Slot>().SetupSlot(Instance.inventory.itemList[i]);

        }



    }


    public static void UpdateItemInfo(string itemDescription)
    {
        Instance.itemInfo.text = itemDescription;
    }


    private static void ResetItemInfo()
    {
        Instance.itemInfo.text = "";
    }

    public void CloseInventory()
    {
        isOpen = false;
        inventoryPanel.SetActive(isOpen);
    }

    private void OpenInventory()
    {
        if (isOpen==false)
        {
            isOpen = true;
            inventoryPanel.SetActive(isOpen);
        }
        else
        {
            isOpen = !isOpen;
            inventoryPanel.SetActive(isOpen);
        }


    }

    private void UpdateCursor()
    {
        if (isOpen)
        {
            //显示鼠标
            Cursor.lockState = CursorLockMode.None;
        }
        else if (!isOpen)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }




}
