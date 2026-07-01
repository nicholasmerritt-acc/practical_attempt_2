using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private const float speed = 10.0f;
    private const float lifespan = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(speed * transform.forward, ForceMode.Impulse);
        Destroy(gameObject, lifespan);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

        }
    }
}
