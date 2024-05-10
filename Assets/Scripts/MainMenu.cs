using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnPlay()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void OnQUit()
    {
        Application.Quit();
    }
}
