using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollider : MonoBehaviour
{

    [SerializeField] private float tiempoEspera; // Tiempo de espera antes de reiniciar el nivel
    private PlayerMove playerMove;
    private PlayerAnimation playerAnimation;

    [Header("Sonidos")]
            [SerializeField] private AudioSource hitSound;


    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    // Detectar colisiones con otros objetos
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            StartCoroutine(PararYReiniciar());
        }
    }

    private IEnumerator PararYReiniciar()
    {
        // Time.timeScale = 0f; // Pausar el juego

        if (!hitSound.isPlaying)
        {
            hitSound.Play();
        }
        
        playerAnimation.Death(); // Reproducir la animación de muerte



        playerMove.PararMovimiento(true); // Desactivar el script de movimiento del jugador

        yield return new WaitForSecondsRealtime(tiempoEspera); // Esperar en tiempo real
        Time.timeScale = 1f; // Reanudar el juego

        playerMove.PararMovimiento(false); // Activar el script de movimiento del jugador

        // Reiniciar el nivel
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
