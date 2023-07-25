using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : Singleton<ParticleManager>
{
    private readonly Vector3 DistanceToHood = new Vector3(0, 0.7f, 1f);

    private readonly Dictionary<ParticleType, ParticleSystem> ParticleDictionary = new Dictionary<ParticleType, ParticleSystem>();

    [SerializeField]
    private ParticleSystem _onCarHitParticlePrefab = null;
    
    private new void Awake()
    {
        base.Awake();
        
        _onCarHitParticlePrefab = Instantiate(_onCarHitParticlePrefab, transform);
        
        ParticleDictionary.Add(ParticleType.CarSmoke, _onCarHitParticlePrefab);
    }

    private void OnEnable()
    {
        Car.Drove += Car_Drove;
    }

    private void OnDisable()
    {
        Car.Drove -= Car_Drove;
    }

    public void Play(ParticleType type)
    {
        var particle = ParticleDictionary[type];
        
        particle.Play();
    }
    
    private void Car_Drove(Vector3 position)
    {
        _onCarHitParticlePrefab.transform.position = position + DistanceToHood;
    }
}