using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    private static string nextScene;
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
            //알아서 보여줄거 처리
            yield return null;
        }
        op.allowSceneActivation = true;
    }
}
