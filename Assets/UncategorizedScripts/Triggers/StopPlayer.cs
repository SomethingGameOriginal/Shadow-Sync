using UnityEngine;

public class StopPlayer : MonoBehaviour
{
    private BoxCollider2D _BoxCollider;
    public int spSceneNumber;
    void Start()
    {
        _BoxCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            KnightController knightController = collision.gameObject.GetComponent<KnightController>();
            knightController.StopPlayer(spSceneNumber);
            _BoxCollider.enabled = false;
        }
    }
}
