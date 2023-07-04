using UnityEngine;

public class Lights : MonoBehaviour
{
    [SerializeField]
    private Light[] _headLights = null;

    private bool _isCurrentState = false;

    private void OnEnable()
    {
        ChangingLight.NightCome += ChangingLight_NightCome;
    }
    
    private void OnDisable()
    {
        ChangingLight.NightCome -= ChangingLight_NightCome;
    }

    private void ChangingLight_NightCome(bool on)
    {
        if (_isCurrentState == on)
        {
            return;
        }

        _isCurrentState = on;
        
        OnHeadlights();
    }
    
    private void OnHeadlights()
    {
        for (int i = 0; i < _headLights.Length; i++)
        {
            _headLights[i].gameObject.SetActive(_isCurrentState);
        }
    }
}