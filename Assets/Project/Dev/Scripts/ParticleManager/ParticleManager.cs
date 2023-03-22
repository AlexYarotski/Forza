using System;
using System.Collections.Generic;
using Project.Dev.Scripts;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [Serializable]
    public class ParticleConfig
    {
        [field: SerializeField]
        public ParticleType ParticleType
        {
            get;
            private set;
        }
    
        [field: SerializeField]
        public ParticleSystem ParticleSystem
        {
            get;
            private set;
        }
    }
    
    private Dictionary<ParticleType, ParticleSystem> _particleDictionary =
        new Dictionary<ParticleType, ParticleSystem>();

    [SerializeField]
    private ParticleSystem _onCarHitParticlePrefab = null;

    public static ParticleManager Instance
    {
        get; 
        private set;
    }
    
    private ParticleSystem _onCarHit = null;

    private void Awake()
    {
        _onCarHit = Instantiate(_onCarHitParticlePrefab, transform);
        _particleDictionary.Add(ParticleType.CarSmoke, _onCarHit);
        
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance == this)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnEnable()
    {
        Barrier.Hit += Barrier_Hit;
        Car.Died += Urus_Died;
    }
    
    private void OnDisable()
    {
        Barrier.Hit -= Barrier_Hit;
        Car.Died -= Urus_Died;
    }
    
    public ParticleSystem Emit(ParticleType particleType, Vector3 pos)
    {
        var system = _particleDictionary[particleType];

        system.transform.position = pos;
        
        system.Play();

        return system;
    }
    
    private void Barrier_Hit(Vector3 obj)
    {
        _onCarHit.Play();
    }

    private void Urus_Died(float obj)
    {
        _onCarHit.gameObject.SetActive(false);
    }
}