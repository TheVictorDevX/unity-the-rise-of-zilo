using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity=Vector3.zero;

    [SerializeField] private Transform player;


    // Update is called once per frame
    void Update()
    {
        transform.position=new Vector3(player.position.x, transform.position.y, transform.position.z);
    }

    public void SnapToPlayer()
    {
        // This instantly aligns the camera with the player's X position
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }
}
