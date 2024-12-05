using System.Collections;
using TMPro;
using UnityEngine;

public class MoneySample : MonoSingleton<MoneySample>
{
    public int Money;

    [SerializeField] private TMP_Text currencyText;

    private int currentUiValue = 0;
    private Coroutine animationCoroutine;

    public void ChangeMoney(int value)
    {
        int targetValue = Money + value;
        Money = targetValue;

        // 기존 애니메이션이 진행 중이면 중단
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        // 새로운 애니메이션 시작
        animationCoroutine = StartCoroutine(AnimateMoney(currentUiValue, targetValue));
    }

    private IEnumerator AnimateMoney(int startValue, int targetValue)
    {
        float duration = 0.5f; // 애니메이션 지속 시간
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // `Lerp`로 현재 값을 계산
            currentUiValue = Mathf.RoundToInt(Mathf.Lerp(startValue, targetValue, t));
            currencyText.text = $"${currentUiValue:N0}";

            yield return null;
        }

        // 최종 값 설정
        currentUiValue = targetValue;
        currencyText.text = $"${currentUiValue:N0}";

        animationCoroutine = null; // 애니메이션 완료
    }
}
