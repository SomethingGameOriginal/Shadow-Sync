using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isShadowWalk;

    public bool isJumpPressed;

    public float jumpPressTime;
    public bool jumpDelayFinish;
    public bool jumpRequest; //запрос на прыжок
    void Start()
    {
        if (instance == null)
            instance = this;

        isShadowWalk = true;
    }
    void Update()
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
}
