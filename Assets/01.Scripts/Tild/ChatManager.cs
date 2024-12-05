using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;

public class ChatManager : MonoSingleton<ChatManager>
{
    [SerializeField] private CanvasGroup _baseNpcText;

    private void Awake()
    {
        _baseNpcText.alpha = 0;
        _baseNpcText.GetComponentInChildren<TMP_Text>().text = "";
    }

    public void StartChat(string text, float rate)
    {
        // 이전 애니메이션을 취소
        _baseNpcText.DOKill();

        // 새 메시지 표시
        StartCoroutine(DisplayMessage(text, rate));
    }

    private IEnumerator DisplayMessage(string text, float rate)
    {
        TMP_Text currentText = _baseNpcText.GetComponentInChildren<TMP_Text>();

        currentText.text = text;
        _baseNpcText.alpha = 0;
        _baseNpcText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -135f);

        _baseNpcText.DOFade(1, 0.5f);
        _baseNpcText.GetComponent<RectTransform>().DOAnchorPosY(0, 0.3f).SetEase(Ease.OutBack);

        yield return new WaitForSeconds(rate);

        _baseNpcText.DOFade(0, 0.5f);
        _baseNpcText.GetComponent<RectTransform>().DOAnchorPosY(-150f, 0.3f).SetEase(Ease.InBack);

        yield return new WaitForSeconds(0.8f); // 애니메이션 시간을 맞추기 위한 대기 시간
    }
}
