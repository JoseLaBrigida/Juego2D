using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Ajustes de movimiento")]
        [SerializeField] private int speed;

    [Header("Tiempo de vida")]
        [SerializeField] private float lifeTime;

    [Header("Sonidos")]
        [SerializeField] private AudioSource shootSound;
        [SerializeField] private AudioSource explosionSound;

    [Header("Efectos")]
        [SerializeField] private GameObject explosionEffect;    
    private Rigidbody2D rb;

    void Start()
    {
        if(shootSound != null)
        {
            shootSound.Play();
        }

        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }


            if (explosionSound != null)
            {
                explosionSound.Play();
            }

            Data.instance.AddScore(collision.GetComponent<EnemyMove>().scorePoints);
            Data.instance.ShowDynamicPoints(collision.GetComponent<EnemyMove>().scorePoints, collision.transform.position);
            
            Destroy(collision.gameObject);
            Destroy(gameObject, 3f);
        }
    }
}
