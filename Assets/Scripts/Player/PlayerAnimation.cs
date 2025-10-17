using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{

    [Header("Componentes")]
    [SerializeField] private Animator animator;
    private PlayerMove playerMove;
    private Rigidbody2D rb;


    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        animator.SetFloat("x", value.Get<Vector2>().x);
        animator.SetFloat("y", value.Get<Vector2>().y);
    }

    public void Death()
    {
        animator.SetTrigger("death");
    }
    
    public void Shoot()
    {
        animator.ResetTrigger("shoot");
        animator.SetTrigger("shoot");
    }

    void Update()
    {
        animator.SetBool("onFloor", playerMove.getEnsuelo);
        animator.SetFloat("y", rb.linearVelocity.y);
    }

}
