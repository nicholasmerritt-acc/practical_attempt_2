using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private const int playerDamage = 1;
    private const int projectileDamage = 1;

    public Transform target;
    public Scorer scorer;

    [SerializeField] private int speed = 5;
    [SerializeField] private int damage = 1;
    [SerializeField] private int points = 10;

    void Update()
    {
        if (target == null)
        {
            return;
        }

        //MOVE TOWARDS #STUDY
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(playerDamage);
            Destroy(gameObject);
        } 
        else if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            GetComponent<Health>().TakeDamage(projectileDamage);
        }
    }

    private void OnDestroy()
    {
        if (scorer != null)
        {
            scorer.AddScore(points);
        }
    }
}
