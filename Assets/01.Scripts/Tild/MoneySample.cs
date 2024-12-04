using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneySample : MonoSingleton<MoneySample>
{
    public int Money;

    [SerializeField] TMP_Text currencyText;

    private int m_UiValue = 0;
    private Queue<int> moneyChangeQueue = new Queue<int>(); // 요청을 저장할 큐
    private bool isAnimating = false; // 애니메이션 실행 상태 확인

    private float baseDelay = 0.01f;

    public void ChangeMoney(int value)
    {
        moneyChangeQueue.Enqueue(value); // 요청을 큐에 추가

        if (!isAnimating)
            StartCoroutine(ProcessQueue());
    }

    private IEnumerator ProcessQueue()
    {
        isAnimating = true;

        while (moneyChangeQueue.Count > 0)
        {
            int value = moneyChangeQueue.Dequeue(); // 큐에서 요청을 꺼냄
            int startValue = m_UiValue; // 현재 UI 값
            int targetValue = Money + value;

            Money = targetValue; // Money 업데이트
            yield return StartCoroutine(Counting(startValue, targetValue)); // 애니메이션 실행
        }

        isAnimating = false; // 큐가 비었으면 애니메이션 상태 해제
    }

    private IEnumerator Counting(int startValue, int targetValue)
    {
        int currentValue = startValue;
        int step = Mathf.Max(1, Mathf.Abs(targetValue - startValue) / 100);
        float delay = Mathf.Clamp(baseDelay, 0.01f, 0.05f);

        while (currentValue != targetValue)
        {
            if (currentValue < targetValue)
                currentValue += step;
            else
                currentValue -= step;

            if ((currentValue > targetValue && step > 0) || (currentValue < targetValue && step < 0))
                currentValue = targetValue;

            m_UiValue = currentValue;
            currencyText.text = $"${m_UiValue:N0}";

            yield return new WaitForSeconds(delay);
        }

        m_UiValue = targetValue;
        currencyText.text = $"${m_UiValue:N0}";
    }
}
    