using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    private static string nextScene;
    [SerializeField] private Image bar;
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    private void Start()
    {
        StartCoroutine(LoadingCor());
    }

    private IEnumerator LoadingCor()
    {
        AsyncOperation op =  SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            if(op.progress >= 0.9f)
                break;
            bar.fillAmount = op.progress;
            yield return null;
        }

        DOVirtual.DelayedCall(10f,() => op.allowSceneActivation = true);
    }
}
