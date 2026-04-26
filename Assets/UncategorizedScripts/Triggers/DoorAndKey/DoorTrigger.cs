using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private Collider2D cr2D;
    public GameObject key;
    void Start()
    {
        cr2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            KnightController knightControllerScripts = collision.gameObject.GetComponent<KnightController>();
            if (knightControllerScripts.isKey == true)
            {
                Destroy(cr2D);
                Destroy(key);
            }
        }
    }
}
