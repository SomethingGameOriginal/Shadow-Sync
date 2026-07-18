using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Toggle tglFullSreen;
    void Start()
    {
        tglFullSreen.isOn = Screen.fullScreen;
        tglFullSreen.onValueChanged.AddListener(onFullSreen);
    }
    void Update()
    {
        
    }
    void onFullSreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
