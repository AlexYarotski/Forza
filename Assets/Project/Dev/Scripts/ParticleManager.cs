using Project.Dev.Scripts;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private static readonly Vector3 DistanceToHood = new Vector3(0, 0.7f, 0.7f);

    [SerializeField]
    private ParticleSystem _onCarHitParticlePrefab = null;

    private ParticleSystem _onCarHit = null;

    private Vector3 _hoodPosition = Vector3.zero;

    private void Awake()
    {
        _onCarHit = Instantiate(_onCarHitParticlePrefab, transform);
    }

    private void OnEnable()
    {
        Barrier.Hit += Barrier_Hit;
        Urus.Drove += Urus_Drove;
        Urus.Died += Urus_Died;
    }
    
    private void OnDisable()
    {
        Barrier.Hit -= Barrier_Hit;
        Urus.Drove -= Urus_Drove;
        Urus.Died -= Urus_Died;
    }

    private void Barrier_Hit(Vector3 obj)
    {
        _onCarHit.Play();
    }

    private void Urus_Drove(Vector3 drove)
    {
        _hoodPosition = drove + DistanceToHood;
        
        _onCarHit.transform.position = _hoodPosition;
    }

    private void Urus_Died(float obj)
    {
        _onCarHit.gameObject.SetActive(false);
    }
}