using UnityEngine;
using System.Collections;

public class PillPickup : MonoBehaviour
{
    [Header("Configuración de la Pastilla")]
    public int healAmount = 2;              // Salud a recuperar al recoger la pastilla
    public float selfDestructTime = 5f;       // Tiempo en segundos antes de disparar la autodestrucción (animación "Limite")
    public float consumeAnimDuration = 1f;    // Duración de la animación "Consumir" (ajusta según tu clip)
    public float bloqueAnimDuration = 1f;     // Duración de la animación "Bloqueo" (para volver a Idle)
    public float limiteAnimDuration = 1f;     // Duración de la animación "Limite"

    private Animator animator;
    private bool inUse = false;              // Flag para evitar múltiples activaciones

    void Start()
    {
        // Obtiene el Animator que debe estar en el mismo GameObject.
        animator = GetComponent<Animator>();

        // ACTIVAR ANIMACIÓN DE APARICIÓN:
        // Al instanciar la pastilla, activamos el trigger "Aparecer". 
        // Nota: Se espera que en el Animator haya una transición desde "Idle" (o un estado spawn) a "Appear" 
        // al recibir este trigger, y que luego automáticamente vuelva a "Idle".
        animator.SetTrigger("Aparecer");

        // Programamos la autodestrucción si la pastilla no es recogida.
        Invoke("TriggerTimeLimit", selfDestructTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Si ya se ha procesado alguna acción, no se vuelve a ejecutar
        if (inUse) return;

        // Verifica que el objeto colisionante tenga el tag "Player"
        if (other.CompareTag("Player"))
        {
            inUse = true;  // Se marca para no activar otra acción simultánea
            CancelInvoke("TriggerTimeLimit"); // Se cancela el autodestrucción por tiempo

            // Se obtiene el componente de salud del jugador (asegúrate de que tu jugador tenga el script "playerHealth")
            playerHealth player = other.GetComponent<playerHealth>();
            if (player != null)
            {
                // Calculamos la salud máxima en función de la cantidad de pastillas (cada pastilla en el HUD representa 2 de salud)
                int maxHealth = player.pills.Length * 2;
                if (player.health < maxHealth)
                {
                    // Si el jugador no está en full vida:
                    // • Se cura con la cantidad definida.
                    // • Se activa la animación "Consumir".
                    // • Se espera la duración de la animación y luego se destruye la pastilla.
                    player.Heal(healAmount);
                    animator.SetTrigger("Consumir");
                    StartCoroutine(DestroyAfterAnimation(consumeAnimDuration));
                }
                else
                {
                    // Si el jugador está en full vida:
                    // • Se activa la animación "Bloqueo".
                    // • Luego, se espera la duración de la animación para volver al estado Idle,
                    //   de modo que la pastilla quede visible e interactuable nuevamente.
                    animator.SetTrigger("Bloqueo");
                    StartCoroutine(ResetToIdleAfterAnimation(bloqueAnimDuration));
                }
            }
        }
    }

    // Método para la autodestrucción (por tiempo)
    void TriggerTimeLimit()
    {
        if (!inUse)
        {
            inUse = true;
            animator.SetTrigger("Limite");
            StartCoroutine(DestroyAfterAnimation(limiteAnimDuration));
        }
    }

    // Corrutina que espera la duración de la animación y luego destruye la pastilla.
    IEnumerator DestroyAfterAnimation(float animDuration)
    {
        yield return new WaitForSeconds(animDuration);
        Destroy(gameObject);
    }

    // Corrutina para el caso de "Bloqueo": espera la animación, vuelve a Idle y reinicia la posibilidad de interactuar.
    IEnumerator ResetToIdleAfterAnimation(float animDuration)
    {
        yield return new WaitForSeconds(animDuration);
        // Cambia al estado "Idle". En el Animator se debe tener un estado denominado "Idle".
        animator.Play("Idle");

        // Se resetea el flag para permitir la interacción futura.
        inUse = false;

        // Opcional: se puede reactivar la autodestrucción si se desea.
        Invoke("TriggerTimeLimit", selfDestructTime);
    }
}