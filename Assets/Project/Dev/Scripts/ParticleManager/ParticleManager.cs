using Project.Dev.Scripts;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private readonly Vector3 DistanceToHood = new Vector3(0, 0.7f, 0.7f);

    [SerializeField]
    private ParticleSystem _onCarHitParticlePrefab = null;

    private ParticleSystem _onCarHit = null;

    private void Awake()
    {
        _onCarHit = Instantiate(_onCarHitParticlePrefab, transform);
    }
    
    private void OnEnable()
    {
        Barrier.Hit += Barrier_Hit;
        Urus.Drove += Urus_Drove;
    }

    private void OnDisable()
    {
        Barrier.Hit -= Barrier_Hit;
        Urus.Drove -= Urus_Drove;
    }

    private void Barrier_Hit(Vector3 obj)
    {
        _onCarHit.Play();
    }

    private void Urus_Drove(Vector3 position)
    {
        _onCarHit.transform.position = position + DistanceToHood;
    }
}