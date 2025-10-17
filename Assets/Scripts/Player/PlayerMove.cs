using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField]
    private float velocidadMovimiento = 6f;

    private Vector2 entradaMovimiento;

    private Rigidbody2D rb;

    private SpriteRenderer sprite;

    public bool mirandoDerecha = true;

    private bool enSuelo = true;

    [Header("Sonidos")]
        [SerializeField] private AudioSource jumpSound;
        [SerializeField] private AudioSource walkSound;
        

    [Header("Salto")]
        [SerializeField] private float fuerzaSalto = 7f;

    public bool getEnsuelo { get { return enSuelo; } }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!sprite)
            sprite = GetComponent<SpriteRenderer>();
    }

        
    public void OnJump(InputValue valor)
    {
        if (!enSuelo)
            return;

        if (!jumpSound.isPlaying)
        {  
            jumpSound.Play();  
        }

        var v = rb.linearVelocity;
        v.y = 0f;
        rb.linearVelocity = v;
        rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
    }

    // Método que recoge la entrada del jugador para moverse.
    public void OnMove(InputValue valor)
    {
        entradaMovimiento = valor.Get<Vector2>();

        if (entradaMovimiento.x > 0 && !mirandoDerecha)
            Girar(false);
        else if (entradaMovimiento.x < 0 && mirandoDerecha)
            Girar(true);
    }

    public void PararMovimiento(bool parado)
    {
        if (parado)
        {
            GetComponent<PlayerInput>().enabled = false;
        }
        else
        {
            GetComponent<PlayerInput>().enabled = true;
        }

        
    }


    private void FixedUpdate()
    {
        var v = rb.linearVelocity;
        v.x = entradaMovimiento.x * velocidadMovimiento;
        rb.linearVelocity = v;
    }


    // Update is called once per frame
    void Update()
    {
        bool andando = Mathf.Abs(entradaMovimiento.x) > 0.1f && enSuelo;

        if (andando && !walkSound.isPlaying)
        {
            walkSound.Play();
        }
        else if (!andando && walkSound.isPlaying)
        {
            walkSound.Pause();
        }

    }

    // Método que invierte la dirección en la que mira el personaje.
    private void Girar(bool aIzquierda)
    {
        mirandoDerecha = !mirandoDerecha;
        if (sprite)
            sprite.flipX = aIzquierda;
    }

    //Me todo que detecta si el personaje está tocando el suelo.
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Suelo"))
        {
            enSuelo = false;
        }
    }

}
