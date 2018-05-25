using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int health = 10;
    public bool isAlive = true;

	// Update is called once per frame
	void Update () {
        if(health <= 0){
            isAlive = false;
        }
	}

    public void beAttacked(int damage){
        health -= damage;
        if(health < 0){
            health = 0;
        }
    }
}
