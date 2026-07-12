using System.Collections;
using UnityEngine;

public class DoorCameraTrigger : MonoBehaviour
{
    private Camera cameraM;
    public GameObject cameraPos;
    public float speed;
    public MirrorTrigger mirrorTriggerScript;

    private BoxCollider2D boxCollider2D;
    private bool isDoorOpen;

    private Animator animator;
    public int thisSceneNumber;

    public float targetCameraSize;
    void Start()
    {
        cameraM = Camera.main;
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (mirrorTriggerScript.isShadowDie == true && isDoorOpen == false && mirrorTriggerScript.mirrorSceneNumber + 1 == thisSceneNumber)
        {
            isDoorOpen = true;
            boxCollider2D.enabled = false;

            animator.Play("openDoor");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(CameraMoven(cameraPos.transform.position, speed));
        }
    }
    IEnumerator CameraMoven(Vector3 toPosition, float deration)
    {
        Vector3 fromPosition = cameraM.transform.position;
        float fromSize = cameraM.orthographicSize;
        float timeMove = 0f;
        while (timeMove < deration)
        {
            float i = timeMove / deration;
            float j = Mathf.SmoothStep(0f, 1f, i);
            cameraM.transform.position = Vector3.Lerp(fromPosition, toPosition, j);
            cameraM.orthographicSize = Mathf.Lerp(fromSize, targetCameraSize, j);
            timeMove += Time.deltaTime;
            yield return null;
        }
        cameraM.transform.position = toPosition;
        cameraM.orthographicSize = targetCameraSize;
        if (thisSceneNumber != GameManager.instance.sceneNumber)
        {
            GameManager.instance.sceneNumber = thisSceneNumber;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isDoorOpen = false;
            boxCollider2D.enabled = true;
            animator.Play("idleDoor");

            if (mirrorTriggerScript.mirrorSceneNumber + 1 == thisSceneNumber)
            {
                mirrorTriggerScript.isShadowDie = false;
            }
        }
    }
}
