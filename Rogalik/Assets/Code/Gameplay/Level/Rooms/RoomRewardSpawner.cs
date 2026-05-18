using UnityEngine;
using Zenject;

namespace GamePlay
{
    public class RoomRewardSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] _rewardPrefabs;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField, Range(0f, 1f)] private float _spawnChance = 0.2f;

        private DiContainer _container;    

        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }

        public void TrySpawnReward()
        {
            if(_rewardPrefabs == null || _rewardPrefabs.Length == 0)
                return;

            if (Random.value > _spawnChance)
                return;

            GameObject rewardPrefab = _rewardPrefabs[Random.Range(0, _rewardPrefabs.Length)];
            
            if(rewardPrefab == null)
                return;
            
            Vector3 spawnPosition = _spawnPoint != null ? _spawnPoint.position : transform.position;
            _container.InstantiatePrefab(rewardPrefab, spawnPosition, Quaternion.identity, null);   

            Debug.Log($"Spawned reward: {rewardPrefab.name} at {spawnPosition}");
        }   
    }
}
