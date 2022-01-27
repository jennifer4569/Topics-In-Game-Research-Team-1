using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class movement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 10f;
    private float jumpHeight = 150f;
    private float gravityValue = -9.81f;
    public Camera cam;
    private bool canMoveAgain = true;

    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }
    
    void subReset_Player_Speed() {
        canMoveAgain = true;
    }

    void Update()
    {

     if (Input.GetMouseButtonDown(0)){ // if left button pressed...
            print("click");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                 Vector3 objectHit = hit.point + (hit.normal);// +  GetComponent<Collider>().bounds.size);
                 //gameobject.transform.position = objectHit;
                 print("clicked" + hit.normal + hit.normal +  GetComponent<Collider>().bounds.size);
                GameObject.FindWithTag("me").transform.position = objectHit;
                canMoveAgain = false;
                Invoke ("subReset_Player_Speed", 3);

              // the object identified by hit.transform was clicked
              // do whatever you want
            }
          }

        if(canMoveAgain == true){

            groundedPlayer = controller.isGrounded;
            Vector3 move = new Vector3(Input.GetAxis("Horizontal") * playerSpeed, 0, Input.GetAxis("Vertical") * playerSpeed);

            //Changes the height position of the player..
            if (Input.GetButtonDown("Jump") && groundedPlayer)
            {
                move.y = jumpHeight;
            }
            move.y = move.y + Physics.gravity.y * .5f;
            controller.Move(move * Time.deltaTime);
       }
    }
}
