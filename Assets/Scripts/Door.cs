using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousScene;
    [SerializeField] private Transform nextScene;
    [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {    //making sure the collision that triggers door is with the player moving through it 
            if(collision.transform.position.x < transform.position.x) {      //player is coming from the left, go into right side scene (next scene) 
               cam.MoveToNextScene(previousScene);
            } 
            else {
                cam.MoveToNextScene(nextScene);
            }
        }
    }
}
