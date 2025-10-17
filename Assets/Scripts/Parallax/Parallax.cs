using UnityEngine;

public class Parallax : MonoBehaviour
{

    [Range(0f, 1f)]
        public float parallaxEffect;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    // LateUpdate is called once per frame, after Update has been called on all objects
    void LateUpdate()
    {
        // Calcular el movimiento del fondo basado en el movimiento de la cámara
        Vector3 backgroundMovement = cameraTransform.position - lastCameraPosition;
        // Mover el fondo en la dirección opuesta al movimiento de la cámara, escalado por el efecto de paralaje
        transform.position += new Vector3(backgroundMovement.x * parallaxEffect,
        backgroundMovement.y * parallaxEffect, 0);
        // Actualizar la última posición de la cámara
        lastCameraPosition = cameraTransform.position;
    }
}
