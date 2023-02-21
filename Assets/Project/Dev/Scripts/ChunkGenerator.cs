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
        private int _distanceForSpawnChanks = 0;

        private Chunk _priorChunk = null;

        private void Awake()
        {
            StartGenerator();
        }

        private void FixedUpdate()
        {
            SpawnChunk();
        }

        private void StartGenerator()
        {
            for (int i = 0; i < _quantityAtStart; i++)
            {
                ChunkList.Add(_poolManager.GetObject<Chunk>(PooledType.Road, Vector3.zero));
                ChunkList[i].transform.position = SetChunkPosition(ChunkList[i]);

                _priorChunk = ChunkList[i];
            }
        }

        private void SpawnChunk()
        {
            var distance = _priorChunk.transform.position.z - _car.transform.position.z;
            
            if (distance <= _distanceForSpawnChanks)
            {
                ChunkList.Add(_poolManager.GetObject<Chunk>(PooledType.Road, Vector3.zero));
                ChunkList[ChunkList.Count].transform.position = SetChunkPosition(ChunkList[ChunkList.Count]);
            }
        }

        private void DeleteChunk()
        {
            
        }

        private Vector3 SetChunkPosition(Chunk chunk)
        {
            if (_priorChunk == null)
            {
                return Vector3.zero;
            }

            var posAxisZ = (_priorChunk.transform.localScale.z + chunk.transform.localScale.z) / 2;
            
            return new Vector3(0, 0, posAxisZ + _priorChunk.transform.position.z);
        }
    }
}