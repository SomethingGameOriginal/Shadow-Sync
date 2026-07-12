using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MirrorTrigger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite sprite;
    public Sprite spritePlayer;
    public int scenesNumber;

    public float delayToScane;
    public bool isShadowDie = false;
    public int mirrorSceneNumber;
    void Start()
    {
        Time.timeScale = 1;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shadow")
        {
            StartCoroutine(AnimationScene());
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            BoxCollider2D boxCollider2D = collision.GetComponent<BoxCollider2D>();
            boxCollider2D.enabled = false;
        }
        else if (collision.tag == "Player")
        {
            spriteRenderer.sprite = spritePlayer;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            spriteRenderer.sprite = sprite;
        }
    }

    IEnumerator AnimationScene()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(delayToScane);
        Time.timeScale = 1;
        isShadowDie = true;
    }
}
