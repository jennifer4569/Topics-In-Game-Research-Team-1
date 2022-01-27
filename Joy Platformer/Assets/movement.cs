using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class movement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public Camera cam;
    private bool canMoveAgain = true;
    public Vector3 newCamAngle;
    
     public float seconds = 5;
     public float timer;
     public Vector3 goal;
     public Vector3 Difference;
     public Vector3 start;
     public float percent ;
     private bool moving = false;

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

                 //print("clicked" + hit.normal + hit.normal +  GetComponent<Collider>().bounds.size);
                //GameObject.FindWithTag("me").transform.position = objectHit;
                
                
                newCamAngle = hit.normal;//(gameObject.transform.position - hit.point).normalized;
                //cam.transform.rotation = Quaternion.LookRotation (forward, Vector3.up);

                
                canMoveAgain = false;
                moving = true;
                start = gameObject.transform.position;
                goal = objectHit;
                Difference = goal - start;
                seconds = 3.0f;
                timer = 0.0f;
            }
        }
          if(Input.GetMouseButtonUp(0)){
              print("move");
                canMoveAgain = true;    
          }
        cam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.75f, gameObject.transform.position.z);

        if(timer <= seconds){
            timer += Time.deltaTime;
            percent = timer/seconds;
            if(percent > 0.9f){
                cam.transform.rotation = Quaternion.LookRotation (newCamAngle, Vector3.up);
            }
            gameObject.transform.position = start + Difference * percent;
        }
          
        if(canMoveAgain == true){
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                Vector3 objectHit = hit.point;
                GameObject.FindWithTag("Player").transform.position = objectHit;
            }


                groundedPlayer = controller.isGrounded;
                if (groundedPlayer && playerVelocity.y < 0)
                {
                    playerVelocity.y = 0f;
                }
        
                Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                controller.Move(move * Time.deltaTime * playerSpeed);
        
                if (move != Vector3.zero)
                {
                    gameObject.transform.forward = move;
                }
        
                // Changes the height position of the player..
                if (Input.GetButtonDown("Jump") && groundedPlayer)
                {
                    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                }
        
                playerVelocity.y += gravityValue * Time.deltaTime;
                print(playerVelocity.y);
                controller.Move(playerVelocity * Time.deltaTime);
        }
        }
          
        
      
    }
