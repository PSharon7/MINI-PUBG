using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 6;
    public float jumpSpeed = 8;
    public float gravity = 20;
    Vector3 moveDir = Vector3.zero;

    float yRotation = 0;
    float yRot;
    float xRot;
    public float sensitivity = 4;
    Camera cam;

    float maxRange = 7;
    public bool canInteract;

    Inventory inv;

	// Use this for initialization
	void Start () {
        canInteract = true;
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        inv = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () {
        MovePlayer();

        if (canInteract){
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            MouseLook();

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                RaycastHit hit;
                BlockScript block;

                if (Physics.Raycast(ray, out hit, maxRange))
                {
                    if (block = hit.transform.GetComponent<BlockScript>())
                    {
                        block.BreakBlock();
                    }
                }
            }

            if (Input.GetMouseButtonDown(1)){
                if(inv.items[inv.hoverIndex].Placeable == true){
                    Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                    RaycastHit hit;
                    BlockScript block;

                    if (Physics.Raycast(ray, out hit, maxRange)){
                        Vector3 spawnPos = Vector3.zero;

                        float xDiff = hit.point.x - hit.transform.position.x;
                        float yDiff = hit.point.y - hit.transform.position.y;
                        float zDiff = hit.point.z - hit.transform.position.z;

                        if(Mathf.Abs(yDiff) == 0.5f){
                            spawnPos = hit.transform.position + (Vector3.up * yDiff) * 2;
                        }
                        else if(Mathf.Abs(xDiff) == 0.5f){
                            spawnPos = hit.transform.position + (Vector3.right * xDiff) * 2;
                        }
                        else if (Mathf.Abs(zDiff) == 0.5f){
                            spawnPos = hit.transform.position + (Vector3.forward * zDiff) * 2;
                        }

                        Instantiate(inv.items[inv.hoverIndex].Object, spawnPos, Quaternion.identity);
                        inv.RemoveItem();
                    }
                }
            }
        }
        else{
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
	}

    void MovePlayer(){
        CharacterController character = GetComponent<CharacterController>();

        if(character.isGrounded){
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= moveSpeed;

            if(Input.GetKeyDown(KeyCode.Space)){
                moveDir.y = jumpSpeed;
            }
        }

        moveDir.y -= gravity * Time.deltaTime;
        character.Move(moveDir * Time.deltaTime);
    }

    void MouseLook(){
        yRot = -Input.GetAxis("Mouse Y") * sensitivity;
        xRot = Input.GetAxis("Mouse X") * sensitivity;
        yRotation += yRot;
        yRotation = Mathf.Clamp(yRotation, -80, 80);

        if(xRot != 0){
            transform.eulerAngles += new Vector3(0, xRot, 0);
        }
        if(yRot != 0){
            cam.transform.eulerAngles = new Vector3(yRotation, transform.eulerAngles.y, 0);
        }

    }
}
