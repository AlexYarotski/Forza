using System.Collections.Generic;
using Project.Dev.Scripts;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private readonly Vector3 DistanceToHood = new Vector3(0, 0.7f, 1f);

    private readonly Dictionary<ParticleType, ParticleSystem> ParticleDictionary = new Dictionary<ParticleType, ParticleSystem>();

    [SerializeField]
    private ParticleSystem _onCarHitParticlePrefab = null;
    
    public static ParticleManager Instance
    {
        get; 
        private set;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance == this)
        {
            Destroy(gameObject);
        }
        
        _onCarHitParticlePrefab = Instantiate(_onCarHitParticlePrefab, transform);
        
        ParticleDictionary.Add(ParticleType.CarSmoke, _onCarHitParticlePrefab);
    }

    private void OnEnable()
    {
        Urus.Drove += Urus_Drove;
    }

    private void OnDisable()
    {
        Urus.Drove -= Urus_Drove;
    }

    public void Play(ParticleType type)
    {
        var particle = ParticleDictionary[type];
        
        particle.Play();
    }
    
    private void Urus_Drove(Vector3 position)
    {
        _onCarHitParticlePrefab.transform.position = position + DistanceToHood;
    }
}