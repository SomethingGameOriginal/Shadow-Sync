using UnityEngine;
using UnityEngine.SceneManagement;

public class ShadowATTACK : MonoBehaviour
{
    private Animator animator;
    private ShadowStateMachine shadowStateMachine;
    void Start()
    {
        animator = GetComponent<Animator>();
        shadowStateMachine = GetComponent<ShadowStateMachine>();
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
            shadowStateMachine.currentState = ShadowStateMachine.stateMove.death;
        }
    }
}
