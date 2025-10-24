using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // Variables de movimiento
    [Header("Posiciones")]
    private Transform puntoA;
    [SerializeField] private Transform puntoB;

    [Header("Velocidad")]
    [SerializeField] private float velocidad = 2f;

    [Header("Puntos")]
    public int scorePoints = 20;

    private bool yendoHaciaB = true;
    private SpriteRenderer sprite;

    private bool mirandoDerecha = true;
    private Rigidbody2D rb;

    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();



        if (puntoA == null)
        {
            GameObject a = new GameObject("PuntoA_" + name);
            a.transform.position = transform.position;
            puntoA = a.transform;
        }
        
        if (!sprite)
            sprite = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        // Determinar el destino actual
        Vector2 destino = yendoHaciaB ? puntoB.position : puntoA.position;
        Vector2 posicionActual = transform.position;
        

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
