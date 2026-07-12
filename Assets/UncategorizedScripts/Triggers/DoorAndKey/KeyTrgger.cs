using UnityEngine;
using UnityEngine.UI;

public class KeyTrgger : MonoBehaviour
{
    private KnightController knightController;

    private bool isPlayerMove = false;
    private GameObject player;
    private int side;
    private bool isfly = true;

    public int speed;
    public Vector3 offset;
    public string doorName;
    void Start()
    {
        
    }
    void Update()
    {
        if (player == null || isPlayerMove == false)
            return;
        if (isfly == false)
            return;
        Vector2 direction = ((player.transform.position + offset * side) - transform.position).normalized;
        float distans = ((player.transform.position + offset * side) - transform.position).magnitude;
        if (distans > 0.2f)
            transform.Translate(direction * speed * Time.deltaTime);
        Sider();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;
            knightController = player.GetComponent<KnightController>();
            isPlayerMove = true;
        }
        else if (collision.tag == "Door" && collision.gameObject.name == doorName)
        {
            isfly = false;
        }
    }

    private void Sider()
    {
        if (!knightController)
        {
            return;
        }
        if (knightController.spriteRenderer.flipX == true)
            side = 1;
        else
            side = -1;
    }
}
