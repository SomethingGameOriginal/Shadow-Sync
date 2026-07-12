using UnityEngine;

public class ShadowController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    public int speed;
    public float jumpForce;

    public ShadowStateMachine stateMachine;
    private GameManager gameManager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        rb.mass = 6;
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
        if (stateMachine.currentState == ShadowStateMachine.stateMove.idle)
            animator.Play("Idle");
        else if (stateMachine.currentState == ShadowStateMachine.stateMove.walk)
            animator.Play("Walk");
        else if (stateMachine.currentState == ShadowStateMachine.stateMove.startJump)
            animator.Play("StartJump");
        else if (stateMachine.currentState == ShadowStateMachine.stateMove.falling)
            animator.Play("Falling");
        else if (stateMachine.currentState == ShadowStateMachine.stateMove.grounded)
            animator.Play("Grounded");
        else if (stateMachine.currentState == ShadowStateMachine.stateMove.death)
            animator.Play("Death");
    }

    void KnightMovement()
    {
        //Движение влево, право
        if (gameManager.isShadowWalk == true && stateMachine.currentState != ShadowStateMachine.stateMove.death)
            HorizontalMovement();

        else
            rb.linearVelocityX = 0;

        if (stateMachine.currentState == ShadowStateMachine.stateMove.idle)
            stateMachine.jumpOne = false;
        else if (stateMachine.currentState == ShadowStateMachine.stateMove.walk)
            stateMachine.jumpOne = false;
        else if (stateMachine.currentState == ShadowStateMachine.stateMove.startJump)
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce); //Прыжок
        else if (stateMachine.currentState == ShadowStateMachine.stateMove.grounded)
            stateMachine.jumpOne = false;
        else if (stateMachine.currentState == ShadowStateMachine.stateMove.rising)
            stateMachine.jumpOne = false;
        else if (stateMachine.currentState == ShadowStateMachine.stateMove.falling)
            stateMachine.jumpOne = false;
    }

    void HorizontalMovement()
    {
        rb.linearVelocity = new Vector2(stateMachine.GetMovement() * speed, rb.linearVelocityY);

        if (stateMachine.GetMovement() != 0)
            spriteRenderer.flipX = Mathf.Sign(stateMachine.GetMovement()) == -1;
    }
}
