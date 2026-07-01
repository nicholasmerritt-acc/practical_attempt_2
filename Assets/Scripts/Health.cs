using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 3;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
