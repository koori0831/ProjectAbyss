using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
public class ChatManager : MonoSingleton<ChatManager>
{
    public bool endText = false;
    public AudioSource typeAudio;
    

    public void StartChat(TMP_Text targetText, string text, float rate)
    {
        StartCoroutine(Typing(text, rate, targetText));
        endText = true;
    }

    private IEnumerator Typing(string text, float rate, TMP_Text descText)
    {
        for (int i = 0; i <= text.Length; i++)
        {
            descText.text = text.Substring(0, i);
            if (descText.text.Length > 0 && descText.text[descText.text.Length - 1] != ' ') ;// typeAudio.Play();
            yield return new WaitForSecondsRealtime(rate);
        }

        yield return new WaitForSeconds(1.5f);
        endText = false;
    }

}
