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

        
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

       
        animationCoroutine = StartCoroutine(AnimateMoney(currentUiValue, targetValue));
    }

    private IEnumerator AnimateMoney(int startValue, int targetValue)
    {
        float duration = 0.5f; // 지속 시간
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

          
            currentUiValue = Mathf.RoundToInt(Mathf.Lerp(startValue, targetValue, t));
            currencyText.text = $"${currentUiValue:N0}";

            yield return null;
        }

        
        currentUiValue = targetValue;
        currencyText.text = $"${currentUiValue:N0}";

        animationCoroutine = null; 
    }
}
