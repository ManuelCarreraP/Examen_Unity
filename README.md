# Examen Unity: Proyecto Roll A Ball

## Resumen de Pasos Realizados

* **Escena**: Configuración de un entorno 3D con un plano (**Ground**), una esfera (**Player**) y un cubo (**Enemy**).
* **Físicas**: Incorporación del componente **Rigidbody** a la esfera para permitir el desplazamiento mediante la aplicación de fuerzas físicas a través del teclado.
* **Detección**: Implementación de un sistema de `Debug.Log` en el script para monitorizar y reportar en consola las colisiones entre el cubo y la bola.
* **IA de Persecución**: Creación de una lógica de comportamiento para el enemigo basada en dos estados: **Lejos** (estático) y **Cerca** (persecución), cambiando según la posición de la bola respecto al cubo.

## Cuestiones Técnicas del Examen

### Detección de contacto (Apartado 2a)
Para detectar el contacto entre el cubo y la bola, se han utilizado las funciones de colisión de Unity. Las variantes principales son:
1.  **OnCollisionEnter**: Se activa en el momento del impacto inicial.
2.  **OnCollisionStay**: Se mantiene activa mientras los objetos sigan en contacto.
3.  **OnCollisionExit**: Se activa cuando los objetos dejan de tocarse.

### IA y Estados (Apartado 2b)
 El cubo actúa como un enemigo con una máquina de estados simple
* **Estado "Lejos"**: El cubo permanece en su posición original si el jugador está fuera de rango
* **Estado "Cerca"**: El cubo persigue activamente la posición de la bola si esta entra en su radio de detección.

### Scripts Completos
### 1. Movimiento del Jugador `PlayerMovement.cs`
Este script aplica fuerza a la bola mediante el teclado.
```bash
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveH, 0.0f, moveV);
        rb.AddForce(direction * speed);
    }
}
```

### 2. Gestión del Enemigo `EnemyController.cs`
Este script combina la detección de colisiones y la persecución por estados ("Lejos" / "Cerca").
```bash
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform targetPlayer;
    public float moveSpeed = 3.5f;
    public float alertRange = 6.0f;

    void Update()
    {
        if (targetPlayer == null) return;

        // Cálculo de distancia para los Estados (Apartado 2b)
        float distance = Vector3.Distance(transform.position, targetPlayer.position);

        if (distance > alertRange)
        {
            // ESTADO: LEJOS
            Debug.Log("Estado: LEJOS. El cubo está en espera.");
        }
        else
        {
            // ESTADO: CERCA
            Debug.Log("Estado: CERCA. Iniciando persecución.");
            Vector3 direction = (targetPlayer.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    // Detección de contacto (Apartado 2a)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡CONTACTO DETECTADO! La bola ha tocado el cubo.");
        }
    }
}
```

