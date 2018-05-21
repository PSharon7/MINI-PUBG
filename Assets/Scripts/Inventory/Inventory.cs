using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public ItemDatabase database;

    int maxAmount = 64;
    int slotAmount = 9;
    int storageAmount = 36;

    public GameObject slot;
    public GameObject invItem;
    public GameObject hotbarPanel;
    public GameObject inventoryPanel;

    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();


	// Use this for initialization
	void Start () {
        database = gameObject.GetComponent<ItemDatabase>();

        for (int i = 0; i < slotAmount; i++)
        {
            items.Add(database.GetItemByID(-1));
            slots.Add(Instantiate(slot));
            slots[i].GetComponent<SlotScript>().slotNumber = i;
            slots[i].transform.SetParent(hotbarPanel.transform);
            slots[i].GetComponent<RectTransform>().transform.localScale = Vector3.one;
        }

        for (int i = slotAmount; i < storageAmount; i++)
        {
            items.Add(database.GetItemByID(-1));
            slots.Add(Instantiate(slot));
            slots[i].GetComponent<SlotScript>().slotNumber = i;
            slots[i].transform.SetParent(inventoryPanel.transform);
            slots[i].GetComponent<RectTransform>().transform.localScale = Vector3.one;
        }

        AddItem(0);AddItem(0);AddItem(0);
        AddItem(1);
    }

	private void Update()
	{
        if(Input.GetKeyDown(KeyCode.Tab)){
            ToggleInventory();
        }
	}

	public void AddItem(int id){
        Item itemToAdd = database.GetItemByID(id);
        if(itemToAdd.Stackable && checkInventory(itemToAdd)){
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                }
            }
        }
        else{
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i].ID == -1){
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(invItem);
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().curSlot = i;
                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.name = itemToAdd.Name;
                    itemObj.transform.localScale = Vector3.one;
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    break;
                }
            }
        }
    }

    bool checkInventory(Item item){
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].ID == item.ID){
                if(slots[i].transform.GetChild(0).GetComponent)
                return true;
            }
        }
        return false;
    }

    public void ToggleInventory(){
        inventoryPanel.SetActive(!inventoryPanel.active);
    }
}
