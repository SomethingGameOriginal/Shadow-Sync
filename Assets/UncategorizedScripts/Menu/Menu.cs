using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button btnContinue;
    public Button btnSelectLevel;
    public Button btnSettings;
    public Button btnQuit;
    void Start()
    {
        btnContinue.onClick.AddListener(ContinueGame);
        btnSelectLevel.onClick.AddListener(SelectLevel);
        btnSettings.onClick.AddListener(Settings);
        btnQuit.onClick.AddListener(Quit);
    }
    void Update()
    {
        
    }
    void ContinueGame()
    {
        SceneManager.LoadScene(3);
    }
    void SelectLevel()
    {
        SceneManager.LoadScene(1);
    }
    void Settings()
    {
        SceneManager.LoadScene(2);
    }
    void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
