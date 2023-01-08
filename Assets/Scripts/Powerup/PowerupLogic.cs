using UnityEngine;

public class PowerupLogic : MonoBehaviour
{
    private float lifetime = 10f;

    private void Start()
    {
        Destroy(gameObject, lifetime);    
    }
}
