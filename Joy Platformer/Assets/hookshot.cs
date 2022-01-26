using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookshot : MonoBehaviour{
    public Camera cam;

    void Update(){
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit)) {
            print(hit);
            Vector3 objectHit = hit.point;
            GameObject.FindWithTag("Player").transform.position = objectHit;

            // Do something with the object that was hit by the raycast.
        }
    }
    }