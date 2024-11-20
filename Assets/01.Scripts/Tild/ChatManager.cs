using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
public class ChatManager : MonoSingleton<ChatManager>
{
    public bool endText = false;
    public AudioSource typeAudio;

    private TMP_Text _baseNpcText;
    private CanvasGroup _bubbleChat;

    private void Awake()
    {
        _baseNpcText = GetComponentInChildren<TMP_Text>();
        _bubbleChat = GetComponent<CanvasGroup>();

    }

    public void StartChat(string text, float rate)
    {
        StartCoroutine(Typing(text, rate, _baseNpcText));
        endText = true;
    }

    private IEnumerator Typing(string text, float rate, TMP_Text descText)
    {

        _bubbleChat.DOFade(1, 1);
        for (int i = 0; i <= text.Length; i++)
        {
            descText.text = text.Substring(0, i);
            if (descText.text.Length > 0 && descText.text[descText.text.Length - 1] != ' ') ;// typeAudio.Play();
            yield return new WaitForSecondsRealtime(rate);
        }
         
        yield return new WaitForSeconds(1.5f);
        endText = false;
        _bubbleChat.DOFade(0, 1);
        
       
    }

}
