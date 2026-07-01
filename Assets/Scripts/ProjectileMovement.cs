using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed = 50.0f;
    [SerializeField] private float lifespan = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(speed * transform.forward, ForceMode.Impulse);
        Destroy(gameObject, lifespan);
    }
}
