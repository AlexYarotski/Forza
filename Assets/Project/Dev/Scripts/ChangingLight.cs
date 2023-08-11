using System;
using UnityEngine;

public class ChangingLight : MonoBehaviour
{
    public static event Action<bool> NightCome = delegate {  };
    
    [SerializeField]
    [Range(0, 1)]
    private float _timeOfDay = 0;

    [SerializeField]
    private float _dayDuration = 0;
    
    [SerializeField]
    private AnimationCurve _curve  = null;
    
    [SerializeField]
    private Light _sun = null;

    private bool _isNight = false;

    private void FixedUpdate()
    {
        _timeOfDay += Time.deltaTime / _dayDuration;

        CheckOfDay();
        
        _sun.transform.localRotation = Quaternion.Euler(_timeOfDay * 360, 90, 0);
        _sun.intensity = _curve.Evaluate(_timeOfDay);
    }

    private void CheckOfDay()
    {
        if (_timeOfDay >= 1 && _isNight)
        {
            _timeOfDay -= 1;

            _isNight = false;
            
            NightCome(_isNight);
        }
        else if (_timeOfDay >= 0.5f && _isNight == false)
        {
            _isNight = true;
            
            NightCome(_isNight);
        }
    }
}