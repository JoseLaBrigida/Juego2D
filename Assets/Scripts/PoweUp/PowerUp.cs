using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Audio")]
        public AudioSource powerUpSound;

    [Header("Score Settings")]
        public int scorePA = 10;
        public int highScore = 50;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        var rb = GetComponent<Rigidbody2D>();

        // Desactivar la gravedad si el Rigidbody existe
        if (rb != null)
        {
            rb.gravityScale = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.SetActive(true);
        int aux = scorePA;
        scorePA = 0; // Evitar múltiples incrementos


        if (other.CompareTag("Player") && aux > 0)
        {
            // Incrementar la puntuación del jugador
            Data.instance.AddScore(aux);

            if (powerUpSound != null)
            {
                powerUpSound.Play();
                // Destruir el objeto PowerUp después de recogerlo
                gameObject.SetActive(false);
                Destroy(gameObject, powerUpSound.clip.length);
            }
            else
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
                
            }
            // Mostrar puntos dinámicos en la posición del PowerUp
            Data.instance.ShowDynamicPoints(aux, transform.position);
            aux = 0;
            
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
