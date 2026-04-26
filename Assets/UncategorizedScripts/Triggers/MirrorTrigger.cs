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
        SceneManager.LoadScene(scenesNumber);
    }
}
