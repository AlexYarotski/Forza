using System;
using UnityEngine;

public class ChangingLight : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    private float _timeOfDay = 0;

    [SerializeField]
    private float _dayDuration = 0;
    
    [SerializeField]
    private AnimationCurve _curve  = null;
    
    [SerializeField]
    private Light _sun = null;

    private float _sunIntensity = 0;

    private void Awake()
    {
        _sunIntensity = _sun.intensity;
    }

    private void FixedUpdate()
    {
        _timeOfDay += Time.deltaTime / _dayDuration;

        if (_timeOfDay >= 1)
        {
            _timeOfDay -= 1;
        }
        
        _sun.transform.localRotation = Quaternion.Euler(_timeOfDay * 360, 180, 0);
        _sun.intensity = _curve.Evaluate(_timeOfDay);
    }
}
