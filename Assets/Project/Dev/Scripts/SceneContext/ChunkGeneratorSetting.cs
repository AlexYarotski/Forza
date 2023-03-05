using UnityEngine;

namespace Project.Dev.Scripts.SceneContext
{
    [CreateAssetMenu(fileName = "ChunkGeneratorSetting", menuName = "Settings/ChunkGeneratorSetting", order = 0)]
    public class ChunkGeneratorSetting : ScriptableObject
    {
        [SerializeField]
        private int _quantityAtStart = 0;

        [SerializeField]
        private int distanceForSpawnChunks = 0;

        public int QuantityAtStart => _quantityAtStart;
        public int DistanceForSpawnChunks => distanceForSpawnChunks;
    }
}