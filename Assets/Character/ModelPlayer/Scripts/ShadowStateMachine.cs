using System.Collections;
using UnityEngine;

public class ShadowStateMachine : MonoBehaviour
{
    public enum stateMove
    {
        idle,

        walk,

        startJump,
        rising,
        falling,
        grounded,

        crouch,
        stayCrouch,
        uncrouch,

        death
    }
    public stateMove currentState;

    private bool jumpDelay;
    public int jumpCoyote;
    public bool jumpOne = false;
    public float jumpMaxTime;
    private bool jumpConsumed = false;

    public Grounded groundedDelay;
    public Grounded grounded;


    private Rigidbody2D rb;
    private float Horizontal;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetMovement();
        if (!GameManager.instance.isJumpPressed)
            jumpConsumed = false;
        StatSvicher();
    }

    void StatSvicher()
    {
        //print(currentState);
        switch (currentState)
        {
            case stateMove.idle:
                if (jumpDelay == true)
                {
                    jumpDelay = false;
                    currentState = stateMove.startJump;
                }
                else if (Horizontal != 0)
                {
                    currentState = stateMove.walk;
                }
                else if (GameManager.instance.isJumpPressed == true && !jumpConsumed)
                {
                    jumpConsumed = true;
                    currentState = stateMove.startJump;
                }
                break;

            case stateMove.startJump:
                jumpDelay = false;
                if (grounded.isGroundInt >= jumpCoyote)
                {
                    if (Time.time - GameManager.instance.jumpPressTime > jumpMaxTime || !GameManager.instance.isJumpPressed)
                    {
                        currentState = stateMove.rising;
                    }
                    jumpDelay = false;
                }
                else if (rb.linearVelocityY < 0)
                {
                    currentState = stateMove.falling;
                }
                break;
            case stateMove.rising:
                if (rb.linearVelocityY < -3)
                {
                    currentState = stateMove.falling;
                }
                break;
            case stateMove.falling:
                if (groundedDelay.isGroundDelay == true && GameManager.instance.isJumpPressed)
                {
                    jumpDelay = true;
                    groundedDelay.isGroundDelay = false;
                }
                else if (grounded.isGround == true)
                {
                    currentState = stateMove.grounded;
                }
                else if (GameManager.instance.isJumpPressed == true && grounded.isGroundInt < jumpCoyote && !jumpConsumed)
                {
                    jumpConsumed = true;
                    rb.linearVelocityY = 0;
                    currentState = stateMove.startJump;
                }
                break;
            case stateMove.grounded:
                if (Horizontal == 0)
                {
                    StartCoroutine(Delay(0, stateMove.idle));
                }
                else
                {
                    StartCoroutine(Delay(0, stateMove.walk));
                }
                break;

            case stateMove.walk:
                if (jumpDelay == true)
                {
                    currentState = stateMove.startJump;
                }
                else if (GameManager.instance.isJumpPressed == true && grounded.isGroundInt < jumpCoyote && !jumpConsumed)
                {
                    jumpConsumed = true;
                    currentState = stateMove.startJump;
                }
                else if (rb.linearVelocityY < -3)
                {
                    currentState = stateMove.falling;
                }
                else if (Horizontal == 0)
                {
                    currentState = stateMove.idle;
                }
                break;
        }
    }

    public float GetMovement()
    {
        Horizontal = Input.GetAxis("Horizontal");
        return Horizontal;
    }

    private IEnumerator Delay(float delayTime, stateMove newStateMove)
    {
        yield return new WaitForSeconds(delayTime);
        currentState = newStateMove;
    }
}
