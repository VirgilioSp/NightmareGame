using UnityEditor.Rendering;
using UnityEngine;

public class openChest : MonoBehaviour
{
    private Animator chestAnimator;
    private bool isPlayerNear = false;
    private bool isOpen = false;

    public GameObject gem;

    void Start()
    {
        chestAnimator = GetComponent<Animator>();
        gem.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && !isOpen && Input.GetKeyDown(KeyCode.E))
        {
            chestAnimator.SetTrigger("Open");
            isOpen = true;
            spawnGem();
        }
    }

    void spawnGem()
    {
        gem.SetActive(true);
        StartCoroutine(riseGem());
    }

    System.Collections.IEnumerator riseGem()
    {
        Vector3 startPos = gem.transform.position;
        Vector3 endPos = startPos + new Vector3(0, 2f, 0);
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = new Vector3(0.5f, 0.5f, 0.5f);

        float duration = .5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            gem.transform.position = Vector3.Lerp(startPos, endPos, t);
            gem.transform.localScale = Vector3.Lerp(startScale, endScale, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        gem.transform.position = endPos;
        gem.transform.localScale = endScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    public void closeChest()
    {
        chestAnimator.SetTrigger("Close");
        GetComponent<BoxCollider2D>().enabled = false;
    }
}