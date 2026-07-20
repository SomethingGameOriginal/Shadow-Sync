using UnityEngine;

public class Wall : MonoBehaviour
{
    public bool isWall = false;
    private BoxCollider2D cr2D;
    private int wallContactCount = 0;

    void Start()
    {
        cr2D = GetComponent<BoxCollider2D>();
        cr2D.size = new Vector2(0.5f, 0.8f);
        cr2D.offset = new Vector2(0, -0.215f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            wallContactCount++;
            isWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            wallContactCount--;
            if (wallContactCount <= 0)
            {
                wallContactCount = 0;
                isWall = false;
            }
        }
    }
}