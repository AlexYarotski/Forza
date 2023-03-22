using UnityEngine;

public class Lights : MonoBehaviour
{
    [SerializeField]
    private Light _leftHeadlight = null;
    
    [SerializeField]
    private Light _rightHeadlight = null;

    [SerializeField]
    private ChangingLight _changingLight = null;

    private void FixedUpdate()
    {
        if (_changingLight.IsNight)
        {
            _leftHeadlight.GetComponent<Light>().enabled = true;
            _rightHeadlight.GetComponent<Light>().enabled = true;
        }
        else
        {
            _leftHeadlight.GetComponent<Light>().enabled = false;
            _rightHeadlight.GetComponent<Light>().enabled = false;
        }
    }
}
