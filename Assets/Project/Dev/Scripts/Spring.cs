using System;
using System.Collections;
using Project.Dev.Scripts;
using TMPro;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField]
    private float _angularFrequency = 0;

    [SerializeField]
    private float _dampingRatio = 0;

    private float _startDamping = 0;

    private bool _isCoroutineEnd = false;
    
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

    private void FixedUpdate()
    {
        if (_isCoroutineEnd)
        {
            StopCoroutine(SpringPosition());
        }
    }

    private void Urus_Drove(Vector3 position)
    {
        _position = position;
    }

    public IEnumerator SpringPosition()
    {
        _isCoroutineEnd = false;
        
        var delay = new WaitForSeconds(_angularFrequency);

        while (_dampingRatio >= 0)
        {
            var rightPosition = new Vector3(transform.position.x + _dampingRatio, transform.position.y, _position.z);
            var leftPosition = new Vector3(transform.position.x - _dampingRatio, transform.position.y, _position.z);

            transform.position = Vector3.Lerp(transform.position, rightPosition, 0.5f);

            yield return delay;
            
            transform.position = Vector3.Lerp(transform.position, leftPosition, 0.5f);
        
            _dampingRatio -= 0.1f;
        }
                
        _dampingRatio = _startDamping;
        _isCoroutineEnd = true;
    }
}
