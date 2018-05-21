using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {

    Inventory inv;
    public int id;

	// Use this for initialization
    void Start () {
        inv = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
        inv.AddItem(id);
        Destroy(this.gameObject);
	}
}
