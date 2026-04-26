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
        uncrouch
    }
    public stateMove currentState;

    private bool isJumpPressed;
    private bool jumpDelay;
    private float jumpPressTime;
    private bool jumpDelayFinish;
    public int jumpCoyote;
    public bool jumpRequest; //запрос на прыжок
    public bool jumpOne = false;
    public float jumpMaxTime;

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
        GetInput();
        GetMovement();
        StatSvicher();
    }

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
            jumpRequest = false; //сброс прыжка
            //запоминает момент времени когда нажали на ПРОБЕЕЕЕЕЕЕЛ
            jumpPressTime = Time.time;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpDelayFinish = false;
            isJumpPressed = false;
            jumpRequest = true;
        }
    }

    void StatSvicher()
    {
        //print(currentState);
        switch (currentState)
        {
            case stateMove.idle:
                if (jumpDelay == true && !jumpDelayFinish)
                {
                    jumpDelayFinish = true;
                    currentState = stateMove.startJump;
                }
                else if (Horizontal != 0)
                {
                    currentState = stateMove.walk;
                }
                else if (isJumpPressed == true && !jumpDelayFinish)
                {
                    jumpDelayFinish = true;
                    currentState = stateMove.startJump;
                }
                break;

            case stateMove.startJump:
                if (grounded.isGroundInt >= jumpCoyote)
                {
                    if (Time.time - jumpPressTime > jumpMaxTime || !isJumpPressed)
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
                if (rb.linearVelocityY < 0)
                {
                    currentState = stateMove.falling;
                }
                break;
            case stateMove.falling:
                if (groundedDelay.isGroundDelay == true && isJumpPressed)
                {
                    jumpDelay = true;
                    groundedDelay.isGroundDelay = false;
                }
                else if (grounded.isGround == true)
                {
                    currentState = stateMove.grounded;
                }
                else if (isJumpPressed == true && grounded.isGroundInt < jumpCoyote)
                {
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
                if (jumpDelay == true && !jumpDelayFinish)
                {
                    jumpDelayFinish = true;
                    currentState = stateMove.startJump;
                }
                else if (isJumpPressed == true && grounded.isGroundInt < jumpCoyote && !jumpDelayFinish)
                {
                    jumpDelayFinish = true;
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
