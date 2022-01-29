using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public Transform eyes;

    void Update() {
        transform.position = eyes.transform.position;
    }
}
