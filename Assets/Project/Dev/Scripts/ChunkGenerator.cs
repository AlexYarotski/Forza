using Project.Dev.Scripts.PoolSystem;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public class ChunkGenerator : MonoBehaviour
    {
        [SerializeField]
        private PoolManager _poolManager = null;

        [SerializeField]
        private Car _car = null;
        
        [SerializeField]
        private int _quantityAtStart = 0;
        
        private Chunk _priorChunk = null;

        private void Awake()
        {
            Generator();
        }

        private void Generator()
        {
            for (var i = 0; i < _quantityAtStart; i++)
            {
                var createChunk = _poolManager.GetObject<Chunk>(PooledType.Road, Vector3.zero);

                createChunk.transform.position = CurrentPosition(createChunk);
            }
        }

        private Vector3 CurrentPosition(Chunk createChunk)
        {
            if (_priorChunk == null)
            {
                _priorChunk = createChunk;
                
                return Vector3.zero;
            }
            else
            {
                var posAxisZ = (_priorChunk.transform.localScale.x + createChunk.transform.localScale.x) / 2;

                return new Vector3(transform.position.x, transform.position.y, posAxisZ);
            }
        }
    }
}