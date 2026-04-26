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
        if (collision.tag != "Key" && collision.tag != "Door" && collision.tag != "mirror")
        {
            print("XD");
            isWall = true;
        }    
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isWall = false;
    }
}
