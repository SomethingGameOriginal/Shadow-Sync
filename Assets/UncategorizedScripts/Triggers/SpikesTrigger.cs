using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikesTrigger : MonoBehaviour
{
    private GameObject playerDeath;
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("PlayerController");
        playerDeath = player.transform.Find("Death").gameObject;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.GetComponent<SpriteRenderer>().enabled = false;
            player.GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<KnightController>().enabled = false;
            player.GetComponent<StateMachine>().enabled = false;
            playerDeath.SetActive(true);

            StartCoroutine(RestartingScene());
        }
    }
    IEnumerator RestartingScene()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
