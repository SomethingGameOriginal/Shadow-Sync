using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VentilationPipes : MonoBehaviour // это для объекта hatch если что!!!
{
    public GameObject mirror;
    private MirrorTrigger mirrorTrigger;
    private BoxCollider2D boxCollider2D;
    private Animator animator;
    public int levelNumber;

    Camera cameraM;
    void Start()
    {
        mirrorTrigger = mirror.GetComponent<MirrorTrigger>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        cameraM = Camera.main;
    }
    void Update()
    {
        if (mirrorTrigger.isShadowDie)
        {
            boxCollider2D.enabled = false;
            animator.Play("OpenHatch");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerPrefs.DeleteKey("savePlayerTransformX");
            PlayerPrefs.DeleteKey("savePlayerTransformY");

            PlayerPrefs.DeleteKey("CameraX");
            PlayerPrefs.DeleteKey("CameraY");
            PlayerPrefs.DeleteKey("CameraZ");
            PlayerPrefs.DeleteKey("CameraSize");

            SceneManager.LoadScene(levelNumber + 2);
        }
    }
}
