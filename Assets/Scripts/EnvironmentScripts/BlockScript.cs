using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {

    public enum blockTypes { 
        Solid,
        Gravity,
        Leaves
    }
    public blockTypes blockBehaviours;

    Inventory inv;
    public int id;

	// Use this for initialization
    void Start () {
        inv = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory>();
        if (blockBehaviours == blockTypes.Gravity) {
            gameObject.AddComponent<Rigidbody>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BreakBlock()
	{
        if (blockBehaviours == blockTypes.Leaves)
        {
            int chance = Random.Range(0, 100);
            if(chance > 90){
                inv.AddItem(id);
            }
            Destroy(this.gameObject);
        }
        else
        {
            inv.AddItem(id);
            Destroy(this.gameObject);
        }
	}
}
