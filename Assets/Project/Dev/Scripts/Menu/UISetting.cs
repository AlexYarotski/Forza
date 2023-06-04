using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
{
    private const float Duration = 0.2f;

    [SerializeField]
    private Button _cancel = null;

    private void Awake()
    {
        gameObject.SetActive(false);

        transform.localScale = Vector3.zero;
        _cancel.AddListener(Cancel);
    }

    private void Cancel()
    {
        transform.DOScale(Vector3.zero, Duration)
            .OnComplete(() => gameObject.SetActive(false));

        Time.timeScale = 1;
    }
}
