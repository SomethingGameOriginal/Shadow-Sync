using System.Collections;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShadowATTACK : MonoBehaviour
{
    private Animator animator;
    private ShadowStateMachine shadowStateMachine;
    private GameObject playerDeath;
    private GameObject player;

    public bool killPlayer;
    void Start()
    {
        animator = GetComponent<Animator>();
        shadowStateMachine = GetComponent<ShadowStateMachine>();

        player = GameObject.Find("PlayerController");
        playerDeath = player.transform.Find("Death").gameObject;
        killPlayer = true;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && killPlayer == true)
        {
            animator.Play("ATTACK");

            player.GetComponent<SpriteRenderer>().enabled = false;
            player.GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<KnightController>().enabled = false;
            player.GetComponent<StateMachine>().enabled = false;
            playerDeath.SetActive(true);

            StartCoroutine(RestartingScene());
        }
        else if (collision.tag == "mirror")
        {
            killPlayer = false;
            StartCoroutine(DelayedDeath());
        }
    }

    IEnumerator RestartingScene()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator DelayedDeath()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        shadowStateMachine.currentState = ShadowStateMachine.stateMove.death;
    }
}
