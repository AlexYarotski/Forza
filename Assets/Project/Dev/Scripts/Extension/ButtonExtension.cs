using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public static class ButtonExtension
{
    private const float Size = -0.2f;
    private const float Duration = 0.1f;
    private const int Vibrato = 0;
    private const int Elasticity = 0;

    public static void AddListener(this Button button, UnityAction unityAction)
    {
        button.onClick.AddListener(() => ClickedButton(button));

        button.onClick.AddListener(unityAction);
    }

    private static void ClickedButton(Button button)
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(button.image.DOColor(Color.gray, Duration));
        sequence.Join(button.transform.DOPunchScale(new Vector3(Size, Size, Size), Duration, Vibrato,
            Elasticity).SetEase(Ease.Linear));
        sequence.Append(button.image.DOColor(Color.white, Duration));
    }
}

