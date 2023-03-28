using System.Collections.Generic;
using Project.Dev.Scripts.PoolSystem;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public class ChunkGenerator : MonoBehaviour
    {
        private readonly List<Chunk> ChunkList = new List<Chunk>();

        [SerializeField]
        private PoolManager _poolManager = null;

        [SerializeField]
        private Car _car = null;

        [SerializeField]
        private int _quantityAtStart = 0;

        [SerializeField]
        private float _distanceForSpawnChunk = 0;

        [SerializeField]
        private float _distanceForDeleteChunk = 0;

        private Chunk _firstChunk = null;
        private Chunk _lastChunk = null;

        private void Awake()
        {
            StartGenerator();
        }

        private void Update()
        {
            var distanceToLast = _lastChunk.transform.position.z - _car.transform.position.z;
            var distanceToFirst = _car.transform.position.z - _firstChunk.transform.position.z;

            if (distanceToLast <= _distanceForSpawnChunk)
            {
                SpawnChunk();
            }

            if (distanceToFirst >= _distanceForDeleteChunk)
            {
                DeleteChunk();
            }
        }

        private void StartGenerator()
        {
            for (int i = 0; i < _quantityAtStart; i++)
            {
                ChunkList.Add(_poolManager.GetObject<Chunk>(PooledType.Chunk, Vector3.zero));
                ChunkList[i].transform.position = SetChunkPosition(ChunkList[i]);
                
                _lastChunk = ChunkList[i];
            }
        }

        private Vector3 SetChunkPosition(Chunk chunk)
        {
            if (_lastChunk == null)
            {
                _firstChunk = chunk;

                return Vector3.zero;
            }

            var posAxisZ = (_lastChunk.transform.localScale.z + chunk.transform.localScale.z) / 2;

            return new Vector3(0, 0, posAxisZ + _lastChunk.transform.position.z);
        }

        private void SpawnChunk()
        {
            PooledType[] randomRangeTypes = {PooledType.Chunk, PooledType.Chunk1, PooledType.Chunk2,
                PooledType.Chunk3, PooledType.Chunk4, PooledType.Chunk5};
            var createChunk = _poolManager.GetObject<Chunk>(randomRangeTypes[Random.Range(0,randomRangeTypes.Length)], Vector3.zero);

            createChunk.transform.position = SetChunkPosition(createChunk);

            ChunkList.Add(createChunk);
            _lastChunk = createChunk;
        }

        private void DeleteChunk()
        {
            _firstChunk.Free();
            ChunkList.RemoveAt(0);

            _firstChunk = ChunkList[0];
        }
    }
}