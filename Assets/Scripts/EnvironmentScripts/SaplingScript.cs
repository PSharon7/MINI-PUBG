using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaplingScript : MonoBehaviour {

    float timer;
    WorldGenerate env;

	// Use this for initialization
	void Start () {
        env = GameObject.FindGameObjectWithTag("Environment").GetComponent<WorldGenerate>();
        timer = Random.Range(10, 60);
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0){
            env.CreateTree(this.transform.position);
            Destroy(this.gameObject);
        }
	}
}
