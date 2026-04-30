using UnityEngine;

public class Grounded : MonoBehaviour
{
    public bool isGround = false;
    public int isGroundInt;
    public bool isGroundDelay = false;
    public string coliderName;
    void Start()
    {
        
    }

    void Update()
    {
        print(isGround);
        if (isGround == true)
        {
            isGroundInt = 0;
        }
        else
        {
            isGroundInt++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {
            if (coliderName == "DelayJump")
            {
                isGroundDelay = true;
            }
            else if (coliderName == "Grounded")
            {
                isGround = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {
            isGround = false;
            isGroundDelay = false;
        }
    }
}
