using UnityEngine;
using UnityEngine.SceneManagement;

public class ShadowATTACK : MonoBehaviour
{
    private Animator animator;
    private StateMachine stateMachine;
    void Start()
    {
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<StateMachine>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.Play("ATTACK");
            Destroy(collision.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (collision.tag == "mirror")
        {
            //stateMachine.enabled = false;

            //Rigidbody2D rb = GetComponent<Rigidbody2D>();
            //rb.simulated = false;

            animator.Play("Death");
        }
    }
}
