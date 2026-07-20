using UnityEngine;
using UnityEngine.SceneManagement;

public class VentilationPipes : MonoBehaviour // это для объекта hatch если что!!!
{
    public GameObject mirror;
    private MirrorTrigger mirrorTrigger;
    private BoxCollider2D boxCollider2D;
    private Animator animator;
    public int levelNumber;
    void Start()
    {
        mirrorTrigger = mirror.GetComponent<MirrorTrigger>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
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
            SceneManager.LoadScene(levelNumber + 2);
        }
    }
}
