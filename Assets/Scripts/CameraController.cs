using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{

    public GameObject player;


    private void Update() {
        //follows player along the x axis 
        transform.position = new Vector3(player.transform.position.x,transform.position.y, transform.position.z); //follow the player on the x axis only  
    }

}
