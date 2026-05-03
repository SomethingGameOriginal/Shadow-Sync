using UnityEngine;

public class KnightController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    public int speed;
    public float jumpForce;
    public bool isKey = false;

    public StateMachine stateMachine;
    public Wall wall;
    public GameManager gameManager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        rb.mass = 1;
        //boxCollider2D.size = new Vector2(0.17f, 0.76f);
        //boxCollider2D.edgeRadius = 0.1f;
    }

    void Update()
    {
        KnightAnimation();
    }
    void FixedUpdate()
    {
        KnightMovement();
    }

    void KnightAnimation()
    {
        if (stateMachine.currentState == StateMachine.stateMove.idle)
        {
            animator.Play("Idle");
        }
        else if (stateMachine.currentState == StateMachine.stateMove.walk)
        {
            animator.Play("Walk");
        }
        else if (stateMachine.currentState == StateMachine.stateMove.startJump)
        {
            animator.Play("StartJump");
        }
        else if (stateMachine.currentState == StateMachine.stateMove.falling)
        {
            animator.Play("Falling");
        }
        else if (stateMachine.currentState == StateMachine.stateMove.grounded)
        {
            animator.Play("Grounded");
        }
    }

    void KnightMovement()
    {
        HorizontalMovement();

        if (stateMachine.currentState == StateMachine.stateMove.idle)
        {
            stateMachine.jumpOne = false;
        }
        else if (stateMachine.currentState == StateMachine.stateMove.walk)
        {
            stateMachine.jumpOne = false;
        }
        else if (stateMachine.currentState == StateMachine.stateMove.startJump)
        {
            //Прыжок
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
            //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        }
        else if (stateMachine.currentState == StateMachine.stateMove.grounded)
        {
            stateMachine.jumpOne = false;
        }
        else if (stateMachine.currentState == StateMachine.stateMove.rising)
        {

        }
        else if (stateMachine.currentState == StateMachine.stateMove.falling)
        {
            stateMachine.jumpOne = false;
        }
    }

    void HorizontalMovement()
    {
        rb.linearVelocity = new Vector2(stateMachine.GetMovement() * speed, rb.linearVelocityY);

        if (stateMachine.GetMovement() != 0)
        {
            spriteRenderer.flipX = Mathf.Sign(stateMachine.GetMovement()) == -1;
        }

        if (gameObject.tag == "Player")
        {
            gameManager.isShadowWalk = !wall.isWall;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Key")
        {
            isKey = true;
        }
    }
}
