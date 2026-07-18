using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    private Button button;
    public int levelNumber;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadLevel);
    }
    void Update()
    {
        
    }
    void LoadLevel()
    {
        SceneManager.LoadScene(levelNumber + 2);
    }
}
