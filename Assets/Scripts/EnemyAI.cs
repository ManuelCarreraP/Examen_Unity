using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform; // Referencia a la bola 
    public float moveSpeed = 3.5f;
    public float detectionRange = 6.0f; // Distancia para cambiar de estado [cite: 10]

    void Update()
    {
        if (playerTransform == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance > detectionRange)
        {
            Debug.Log("Estado: LEJOS - El enemigo está estático.");
        }
        else
        {
            Debug.Log("Estado: CERCA - Persiguiendo a la bola.");
            
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("¡CONTACTO DETECTADO! La bola ha tocado al cubo.");
        }
    }
}