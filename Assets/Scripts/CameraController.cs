using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    //[SerializeField] private float speed;
    //private float currentPosX;  //tells the camera where to go and follow
    //private Vector3 velocity = Vector3.zero;

    private void Update() {
        transform.position = new Vector3(player.transform.position.x,transform.position.y, transform.position.z); //follow the player on the x axis only  
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);  //gradually changes a vetcor and moves it toward desired goal over time
    }

}
