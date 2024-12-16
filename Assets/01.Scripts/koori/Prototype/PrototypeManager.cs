using UnityEngine;
using UnityEngine.SceneManagement;

public class PrototypeManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("PrototypePlay");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
