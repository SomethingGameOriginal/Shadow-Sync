using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KnightController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    public int speed;
    public float jumpForce;
    public string isKey;

    public StateMachine stateMachine;
    public Wall wall;
    public GameManager gameManager;

    public GameObject shadow;
    public GameObject shadowPrefab;
    public GameObject[] shadowSpawnObj;
    private bool isStop = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        rb.mass = 6;
    }

    void Update()
    {
        KnightAnimation();
    }
    void FixedUpdate()
    {
        if (isStop == false)
            KnightMovement();
    }

    void KnightAnimation()
    {
        if (stateMachine.currentState == StateMachine.stateMove.idle)
            animator.Play("Idle");
        else if (stateMachine.currentState == StateMachine.stateMove.walk)
            animator.Play("Walk");
        else if (stateMachine.currentState == StateMachine.stateMove.startJump)
            animator.Play("StartJump");
        else if (stateMachine.currentState == StateMachine.stateMove.falling)
            animator.Play("Falling");
        else if (stateMachine.currentState == StateMachine.stateMove.grounded)
            animator.Play("Grounded");
    }

    void KnightMovement()
    {
        HorizontalMovement();

        if (stateMachine.currentState == StateMachine.stateMove.idle)
            stateMachine.jumpOne = false;
        else if (stateMachine.currentState == StateMachine.stateMove.walk)
            stateMachine.jumpOne = false;
        else if (stateMachine.currentState == StateMachine.stateMove.startJump)
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce); //Прыжок
        else if (stateMachine.currentState == StateMachine.stateMove.grounded)
            stateMachine.jumpOne = false;
        else if (stateMachine.currentState == StateMachine.stateMove.rising)
            stateMachine.jumpOne = false;
        else if (stateMachine.currentState == StateMachine.stateMove.falling)
            stateMachine.jumpOne = false;
    }

    void HorizontalMovement()
    {
        rb.linearVelocity = new Vector2(stateMachine.GetMovement() * speed, rb.linearVelocityY);

        if (stateMachine.GetMovement() != 0)
            spriteRenderer.flipX = Mathf.Sign(stateMachine.GetMovement()) == -1;

        if (gameObject.tag == "Player")
            gameManager.isShadowWalk = !wall.isWall;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Key")
            isKey = collision.gameObject.name;
    }

    public void StopPlayer(int knSceneNumber)
    {
        isStop = true;
        stateMachine.currentState = StateMachine.stateMove.idle;
        stateMachine.enabled = false;
        rb.linearVelocityX = 0;
        StartCoroutine(UnStopPlayer(knSceneNumber));
    }
    IEnumerator UnStopPlayer(int knSceneNumber)
    {
        yield return new WaitForSeconds(1.6f);

        Destroy(shadow);
        shadow = Instantiate(shadowPrefab, shadowSpawnObj[knSceneNumber].transform.position, Quaternion.identity);

        ShadowController shadowController = shadow.GetComponent<ShadowController>();
        shadow.GetComponent<Animator>().Play("Awakening");
        shadowController.enabled = false;

        yield return new WaitForSeconds(1.6f);

        shadowController.enabled = true;
        isStop = false;
        stateMachine.enabled = true;
    }
}
