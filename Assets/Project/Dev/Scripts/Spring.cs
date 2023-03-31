using System;
using System.Collections;
using Project.Dev.Scripts;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField]
    private float _angularFrequency = 0;

    [SerializeField]
    private float _dampingRatio = 0;

    private float _startDamping = 0;

    private Vector3 _position = Vector3.zero;
    
    private void Awake()
    {
        _startDamping = _dampingRatio;
    }

    private void OnEnable()
    {
        Urus.Drove += Urus_Drove;
    }

    private void OnDisable()
    {
        Urus.Drove -= Urus_Drove;
    }

    
    private void Urus_Drove(Vector3 position)
    {
        _position = position;
    }

    
    public void Position()
    {
        StartCoroutine(SpringPosition());
        
         _dampingRatio = _startDamping;
    }
    
    private IEnumerator SpringPosition()
    {
        var delay = new WaitForSeconds(_angularFrequency);

        while (_dampingRatio >= 0)
        {
            var rightPosition = new Vector3(_position.x + _dampingRatio, _position.y, _position.z);
            var leftPosition = new Vector3(_position.x - _dampingRatio, _position.y, _position.z);
        
            transform.position = rightPosition;

            yield return delay;
            
            transform.position = leftPosition;
        
            _dampingRatio -= 0.1f;
        }
    }
}
