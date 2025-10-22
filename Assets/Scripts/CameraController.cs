using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float speed;
    private float currentPosX;  //tells the camera where to go and follow
    private Vector3 velocity = Vector3.zero;

    private void Update() {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);  //gradually changes a vetcor and moves it toward desired goal over time
    }

    public void MoveToNextScene(Transform _newscene) {
        currentPosX = _newscene.position.x;
    }
}
