using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public int shootingDamage = 1;
    public float shootingRange = 50.0f;
    public AudioClip shootingAudio;
    public float shootingInteval = 1.0f;

    private Animator animator;
    private LineRenderer gunLine;

    private float timer;
    private Ray ray;
    private RaycastHit hit;

	// Use this for initialization
	void Start () {
        animator = GetComponentInParent<Animator>();
        gunLine = GetComponent<LineRenderer>();
        timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.J) && timer > shootingInteval)
        {
            timer = 0.0f;
            animator.SetBool("isShooting", true);
            Invoke("shoot", 0.5f);
        }
        else
        {
            timer += Time.deltaTime;
            gunLine.enabled = false;
            animator.SetBool("isShooting", false);

        }
	}

    void shoot(){
        AudioSource.PlayClipAtPoint(shootingAudio, transform.position);
        ray.origin = Camera.main.transform.position;
        ray.direction = Camera.main.transform.forward;
        gunLine.SetPosition(0, transform.position);

        if(Physics.Raycast(ray, out hit, shootingRange)){
            if(hit.collider.gameObject.tag == "Enemy"){
                EnemyHealth enemyHealth = hit.collider.gameObject.GetComponent<EnemyHealth>();
                if (enemyHealth != null){
                    enemyHealth.beAttacked(shootingDamage);
                }
                if (enemyHealth.health > 0){
                    hit.collider.gameObject.transform.position += transform.forward * 2;
                }
            }
            gunLine.SetPosition(1, hit.point);
        }
    }
}
