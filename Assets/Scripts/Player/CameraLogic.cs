using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    private Transform playerPosition;
    private Vector3 offset = new Vector3(0, 0, 10);

    private void Awake()
    {
        playerPosition = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        transform.position = playerPosition.position - offset;
    }
}
