using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Back : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(BackXD);
    }
    void Update()
    {
        
    }
    void BackXD()
    {
        SceneManager.LoadScene(0);
    }
}
