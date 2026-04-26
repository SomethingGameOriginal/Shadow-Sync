using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 speed;
    public float smootTime;
    public Vector2 boundsX;
    public Vector2 boundsY;
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 targetPosition = player.transform.position;
        Vector3 deltaCameraPosition = targetPosition - transform.position;
        if(deltaCameraPosition.x > boundsX.x && deltaCameraPosition.x < boundsX.y)
        {
            targetPosition.x = transform.position.x;
        }
        if (deltaCameraPosition.y > boundsY.x && deltaCameraPosition.y < boundsY.y)
        {
            targetPosition.y = transform.position.y;
        }
        Vector3 firstPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref speed, smootTime);
        transform.position = new Vector3(firstPosition.x, firstPosition.y, -10);
    }
}
