using System.Collections.Generic;
using Project.Dev.Scripts.PoolSystem;
using Project.Dev.Scripts.SceneContext;
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
        
        private int _quantityAtStart = 0;
        private int _distanceForSpawnChunks = 0;
        
        private Chunk _lastChunk = null;

        private void Awake()
        {
            var setting = SceneContext.SceneContext.Inctance.ChunkGeneratorSetting;

            _quantityAtStart = setting.QuantityAtStart;
            _distanceForSpawnChunks = setting.DistanceForSpawnChunks;
            
            StartGenerator();
        }

        private void FixedUpdate()
        {
            var distance = _lastChunk.transform.position.z - _car.transform.position.z;

            if (distance <= _distanceForSpawnChunks)
            {
                SpawnChunk();
            }
        }

        private void StartGenerator()
        {
            for (int i = 0; i < _quantityAtStart; i++)
            {
                ChunkList.Add(_poolManager.GetRandomObject<Chunk>(PooledType.Road, Vector3.zero));
                ChunkList[i].transform.position = SetChunkPosition(ChunkList[i]);

                _lastChunk = ChunkList[i];
            }
        }

        private Vector3 SetChunkPosition(Chunk chunk)
        {
            if (_lastChunk == null)
            {
                return Vector3.zero;
            }

            var posAxisZ = (_lastChunk.transform.localScale.z + chunk.transform.localScale.z) / 2;

            return new Vector3(0, 0, posAxisZ + _lastChunk.transform.position.z);
        }

        private void SpawnChunk()
        {
            var createChunk = _poolManager.GetObject<Chunk>(PooledType.Road, Vector3.zero);

            createChunk.transform.position = SetChunkPosition(createChunk);

            _lastChunk = createChunk;

            ChunkList.Add(createChunk);

            DeleteChunk();
        }

        private void DeleteChunk()
        {
            ChunkList[0].Free();
            ChunkList.RemoveAt(0);
        }
    }
}