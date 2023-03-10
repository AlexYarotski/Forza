using UnityEngine;

public class Wheel : MonoBehaviour
{
    private readonly int IsRotate = Animator.StringToHash("IsRotate");

    [SerializeField]
    private Animator _animator = null;

    private void Awake()
    {
        _animator.SetBool(IsRotate,true);
    }
}
