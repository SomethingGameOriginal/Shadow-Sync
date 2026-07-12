using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isShadowWalk;

    public bool isJumpPressed;

    public float jumpPressTime;
    public bool jumpRequest; //запрос на прыжок

    public int sceneNumber;
    void Start()
    {
        sceneNumber = 1;
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
            isJumpPressed = false;
            jumpRequest = true;
        }
    }
}
