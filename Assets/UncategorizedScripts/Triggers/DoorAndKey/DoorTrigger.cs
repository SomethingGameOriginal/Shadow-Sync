using System.Collections;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private Collider2D cr2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private int side;
    public GameObject key;
    void Start()
    {
        cr2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (spriteRenderer.flipX == true)
            side = -1;
        else
            side = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            KnightController knightControllerScripts = collision.gameObject.GetComponent<KnightController>();
            if (knightControllerScripts == null || key == null)
                return;
            if (knightControllerScripts.isKey == key.name)
            {
                Animator keyAnimator = key.GetComponent<Animator>();
                StartCoroutine(Open());

                keyAnimator.Play("Open");

                Destroy(cr2D);
                Destroy(key, 1);

                key.transform.position = gameObject.transform.position + new Vector3(-0.6f * side, 0, 0);
            }
        }
    }

    private IEnumerator Open()
    {
        yield return new WaitForSeconds(0.5f);
        animator.Play("openDoor");
    }
}
