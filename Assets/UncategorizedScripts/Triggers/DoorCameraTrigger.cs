using System.Collections;
using UnityEngine;

public class DoorCameraTrigger : MonoBehaviour
{
    private Camera cameraM;
    public GameObject cameraPos;
    public float speed;
    public MirrorTrigger mirrorTriggerScript;

    private BoxCollider2D boxCollider2D;
    private BoxCollider2D bC2D_isTrigger;
    private bool isDoorOpen;

    private Animator animator;
    public int thisSceneNumber;

    public float targetCameraSize;
    void Start()
    {
        cameraM = Camera.main;
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        foreach (var coll in GetComponents<BoxCollider2D>())
        {
            if (coll.isTrigger)
            {
                bC2D_isTrigger = coll;
                break;
            }
        }

        if (PlayerPrefs.HasKey("CameraX"))
        {
            cameraM.transform.position = new Vector3(PlayerPrefs.GetFloat("CameraX"), PlayerPrefs.GetFloat("CameraY"), PlayerPrefs.GetFloat("CameraZ"));

            cameraM.orthographicSize = PlayerPrefs.GetFloat("CameraSize");
        }
    }

    void Update()
    {
        if (mirrorTriggerScript.isShadowDie == true && isDoorOpen == false && mirrorTriggerScript.mirrorSceneNumber + 1 == thisSceneNumber)
        {
            isDoorOpen = true;
            boxCollider2D.enabled = false;
            bC2D_isTrigger.enabled = true;

            animator.Play("openDoor");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SavePositionPlayer(collision);
            StartCoroutine(CameraMoven(cameraPos.transform.position, speed));
        }
    }
    IEnumerator CameraMoven(Vector3 toPosition, float deration)
    {
        bC2D_isTrigger.enabled = false;

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

        SaveCameraPosition();

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
    void SavePositionPlayer(Collider2D collision)
    {
        float savePlayerX = collision.transform.position.x;
        float savePlayerY = collision.transform.position.y;

        PlayerPrefs.SetFloat("savePlayerTransformX", savePlayerX);
        PlayerPrefs.SetFloat("savePlayerTransformY", savePlayerY);
        PlayerPrefs.Save();
    }
    void SaveCameraPosition()
    {
        PlayerPrefs.SetFloat("CameraX", cameraM.transform.position.x);
        PlayerPrefs.SetFloat("CameraY", cameraM.transform.position.y);
        PlayerPrefs.SetFloat("CameraZ", cameraM.transform.position.z);
        PlayerPrefs.SetFloat("CameraSize", cameraM.orthographicSize);
        PlayerPrefs.Save();
    }
}
