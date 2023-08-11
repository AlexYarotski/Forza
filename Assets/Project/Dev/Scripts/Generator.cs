using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Generator : MonoBehaviour
{
    private readonly List<Chunk> ChunkList = new List<Chunk>();

    private readonly Dictionary<ChunkType, List<PooledType>> ChunkDictionary =
        new Dictionary<ChunkType, List<PooledType>>()
        {
            {
                ChunkType.Chunk, new List<PooledType>()
                {
                    PooledType.Chunk, PooledType.Chunk1, PooledType.Chunk2, PooledType.Chunk3, PooledType.Chunk4,
                    PooledType.Chunk5, PooledType.Chunk6, PooledType.Chunk7, PooledType.Chunk8, PooledType.Chunk9,
                    PooledType.Chunk10,PooledType.Chunk11,PooledType.Chunk12,PooledType.Chunk13,PooledType.Chunk14,
                    PooledType.Chunk15,
                }
            },
            {
                ChunkType.Environment, new List<PooledType>()
                {
                    PooledType.Environment, PooledType.Environment1, PooledType.Environment2, PooledType.Environment3,
                    PooledType.Environment4, PooledType.Environment5
                }
            }
        };

    [SerializeField]
    private PoolManager _poolManager = null;

    [SerializeField]
    private Car _car = null;

    [Space]
    [SerializeField]
    private ChunkType _chunkType = default;

    [Space]
    [SerializeField]
    private int _quantityAtStart = 0;

    [SerializeField]
    private float _distanceForSpawnChunk = 0;

    [SerializeField]
    private float _distanceForDeleteChunk = 0;

    private Chunk _firstChunk = null;
    private Chunk _lastChunk = null;

    private PooledType _pooledType = default;
    private PooledType _startChunk = default;

    private void Start()
    {
        ChooseChunk();
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

    private void ChooseChunk()
    {
        switch (_chunkType)
        {
            case ChunkType.Chunk:
                _startChunk = PooledType.Chunk;
                break;

            case ChunkType.Environment:
                _startChunk = PooledType.Environment;
                break;

            default:
                _startChunk = default;
                Debug.LogError("The selected type does not exist!");
                break;
        }
    }

    private void StartGenerator()
    {
        for (int i = 0; i < _quantityAtStart; i++)
        {
            ChunkList.Add(_poolManager.GetObject<Chunk>(_startChunk, Vector3.zero));
            ChunkList[i].transform.position = SetChunkPosition(ChunkList[i]);

            _lastChunk = ChunkList[i];
        }
    }

    private void SpawnChunk()
    {
        PooledType[] pooledTypes = ChunkDictionary[_chunkType].ToArray();

        var createChunk = _poolManager.GetObject<Chunk>(RandomTypeChunk(pooledTypes), Vector3.zero);

        createChunk.transform.position = SetChunkPosition(createChunk);

        ChunkList.Add(createChunk);
        _lastChunk = createChunk;
    }

    private Vector3 SetChunkPosition(Chunk chunk)
    {
        if (_lastChunk == null)
        {
            _firstChunk = chunk;

            return Vector3.zero;
        }

        var posAxisZ = (_lastChunk.transform.localScale.z + chunk.transform.localScale.z) / 2;

        return new Vector3(_lastChunk.transform.position.x, _lastChunk.transform.position.y,
            posAxisZ + _lastChunk.transform.position.z);
    }

    private PooledType RandomTypeChunk(PooledType[] pooledTypes)
    {
        var randomType = Random.Range(0, pooledTypes.Length);

        while (_pooledType == pooledTypes[randomType])
        {
            randomType = Random.Range(0, pooledTypes.Length);
        }

        _pooledType = pooledTypes[randomType];

        return pooledTypes[randomType];
    }

    private void DeleteChunk()
    {
        _firstChunk.Free();
        ChunkList.RemoveAt(0);

        _firstChunk = ChunkList[0];
    }
}