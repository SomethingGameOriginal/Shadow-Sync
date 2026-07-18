using UnityEngine;

public class Wall : MonoBehaviour
{
    public bool isWall = false;
    private BoxCollider2D cr2D;
    void Start()
    {
        cr2D = GetComponent<BoxCollider2D>();
        cr2D.size = new Vector2(0.5f, 0.8f);
        cr2D.offset = new Vector2(0, -0.215f);
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
            isWall = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isWall = false;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Wall")
    //        isWall = true;
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    isWall = false;
    //}
}
