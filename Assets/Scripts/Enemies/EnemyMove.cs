using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // Variables de movimiento
    [Header("Posiciones")]
    [SerializeField] private Transform puntoA;
    [SerializeField] private Transform puntoB;

    [Header("Velocidad")]
    [SerializeField] private float velocidad = 2f;

    private bool yendoHaciaB = true;
    private SpriteRenderer sprite;

    private bool mirandoDerecha = true;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (!sprite)
            sprite = GetComponent<SpriteRenderer>();

        if (puntoA == null)
        {
            puntoA = new GameObject("PuntoA").transform;
            puntoA.position = transform.position;
        }


    }

    // Update is called once per frame
    void Update()
    {
        // Determinar el destino actual
        Vector2 posicionActual = transform.position;


        Vector2 destino = yendoHaciaB ? puntoB.position : puntoA.position;
        transform.position = Vector2.MoveTowards(posicionActual, destino, velocidad * Time.deltaTime);

        // Cambiar de dirección si se ha llegado al destino
        if ((Vector2)transform.position == destino)
        {
            Girar(!yendoHaciaB);
            yendoHaciaB = !yendoHaciaB;
        }
    }
    
    private void Girar(bool aIzquierda)
    {
        mirandoDerecha = !mirandoDerecha;
        if (sprite)
            sprite.flipX = aIzquierda;
    }
}
