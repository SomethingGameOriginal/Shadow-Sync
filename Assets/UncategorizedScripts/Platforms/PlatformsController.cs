using System.Collections;
using UnityEngine;

public class PlatformsController : MonoBehaviour
{
    public float rayDistance;
    public float fallSpeed;
    public LayerMask targerlayer;

    private bool isFalling;

    private Rigidbody2D rb;
    private Collider2D cr;
    private Coroutine fallCoroutine;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cr = GetComponent<Collider2D>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, targerlayer);

        //Debug.DrawRay(transform.position, Vector2.down * rayDistance, Color.blue);

        if (hit.collider != null)
        {
            cr.isTrigger = true;
        }
    }

    void FixedUpdate()
    {
        if (rb.bodyType != RigidbodyType2D.Static)
        {
            Vector2 newVelocity = rb.linearVelocity;
            newVelocity.y = -fallSpeed;

            rb.linearVelocity = newVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFalling && collision.gameObject.tag == "Player" || collision.gameObject.tag == "Shadow")
        {
            isFalling = true;
            fallCoroutine = StartCoroutine(TimerFalling());
        }
    }

    IEnumerator TimerFalling()
    {
        yield return new WaitForSeconds(2);
        rb.bodyType = RigidbodyType2D.Dynamic;
        isFalling = false;
        fallCoroutine = null;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isFalling == true && collision.gameObject.tag != "Player" || collision.gameObject.tag != "Shadow")
        {
            if (fallCoroutine != null)
            {
                StopCoroutine(fallCoroutine);
                isFalling = false;
            }
        }
    }
}
