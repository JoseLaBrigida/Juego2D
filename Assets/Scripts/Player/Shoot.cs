using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{

    [Header("Disparo")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePointRight;
    [SerializeField] private Transform firePointLeft;

    private PlayerMove playerMove;
    private PlayerAnimation playerAnimation;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }


    public void OnShoot(InputValue value)
    {
        if (!value.isPressed)
            return;

        if (bulletPrefab == null || firePointRight == null || firePointLeft == null)
            return;

        StartCoroutine(ShootTime());
    }
    
    private IEnumerator ShootTime()
    {

        
        playerAnimation.Shoot();

        if (!playerMove.mirandoDerecha)
        {
            Instantiate(bulletPrefab, firePointLeft.position, firePointLeft.rotation);
        }
        else
        {
            Instantiate(bulletPrefab, firePointRight.position, firePointRight.rotation);
        }

        yield return new WaitForSeconds(20000000f);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
