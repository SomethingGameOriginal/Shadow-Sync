using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public BoxCollider2D bcOne;
    public BoxCollider2D bcTwo;

    public Sprite unpress;
    public Sprite press;

    private GameObject doorOne;
    private GameObject doorTwo;
    void Start()
    {
        spriteRenderer = GameObject.Find("Button").GetComponent<SpriteRenderer>();

        bcOne = GameObject.Find("ButtonDoorOne").GetComponent<BoxCollider2D>();
        bcTwo = GameObject.Find("ButtonDoorTwo").GetComponent<BoxCollider2D>();

        doorOne = GameObject.Find("ButtonDoorOne");
        doorTwo = GameObject.Find("ButtonDoorTwo");

        Door(doorTwo, true);
        bcTwo.enabled = false;
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Grounded")
        {
            spriteRenderer.sprite = press;

            Door(doorOne, true);
            Door(doorTwo, false);

            bcOne.enabled = false;
            bcTwo.enabled = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Grounded")
        {
            spriteRenderer.sprite = press;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Grounded")
        {
            spriteRenderer.sprite = unpress;

            Door(doorOne, false);
            Door(doorTwo, true);

            bcOne.enabled = true;
            bcTwo.enabled = false;
        }
    }

    private void Door(GameObject doorObJect, bool Open = false)
    {
        Animator animator = doorObJect.GetComponent<Animator>();
        if (Open == false)
        {
            animator.Play("Idle");
        }
        else
        {
            animator.Play("InstantOpen");
        }
    }
}
