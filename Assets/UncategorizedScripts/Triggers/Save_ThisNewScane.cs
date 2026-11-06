using UnityEngine;
using UnityEngine.SceneManagement;

public class Save_ThisNewScane : MonoBehaviour
{
    Camera cameraM;
    void Start()
    {
        cameraM = Camera.main;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerPrefs.DeleteKey("CameraX");
            PlayerPrefs.DeleteKey("CameraY");
            PlayerPrefs.DeleteKey("CameraZ");
            PlayerPrefs.DeleteKey("CameraSize");

            PlayerPrefs.DeleteKey("savePlayerTransformX");
            PlayerPrefs.DeleteKey("savePlayerTransformY");

            //PlayerPrefs.DeleteAll();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            float savePlayerX = collision.transform.position.x;
            float savePlayerY = collision.transform.position.y;
            PlayerPrefs.SetFloat("savePlayerTransformX", savePlayerX);
            PlayerPrefs.SetFloat("savePlayerTransformY", savePlayerY);

            PlayerPrefs.SetFloat("CameraX", cameraM.transform.position.x);
            PlayerPrefs.SetFloat("CameraY", cameraM.transform.position.y);
            PlayerPrefs.SetFloat("CameraZ", cameraM.transform.position.z);
            PlayerPrefs.SetFloat("CameraSize", cameraM.orthographicSize);
            PlayerPrefs.Save();
        }
    }
}
