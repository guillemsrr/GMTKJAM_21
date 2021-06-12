using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelGenerator
{
    public class LevelGenerator: MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private SpaceBodyReferences _spaceBodyReferences;

        private List<SpaceBodyControllerBase> _spaceBodies = new List<SpaceBodyControllerBase>();

        private const float VISION_RADIUS = 50f;
        private const float EXTRA_RADIUS = VISION_RADIUS + 10f;
        private const float MINIMUM_BODY_DISTANCE = 5f;
        private const int NUMBER_BODIES = 50;

        private readonly WaitForSeconds POOL_CHECK_WAIT = new WaitForSeconds(0.5f);

        private float _minPlayerDistance = 5f;
        private float _maxPlayerDistance = VISION_RADIUS;
        private SpaceBodyRandomizer _spaceBodyRandomizer;
        
        
        private void Awake()
        {
            StartCoroutine(PoolCoroutine());
            _spaceBodyReferences.Initialize();
            _spaceBodyRandomizer = new SpaceBodyRandomizer(_spaceBodyReferences, NUMBER_BODIES);
        }

        private void Start()
        {
            InitialGeneration();
        }

        private void InitialGeneration()
        {
            for (int i = 0; i < NUMBER_BODIES; i++)
            {
                CreateSpaceBody();
            }

            _maxPlayerDistance = EXTRA_RADIUS;
            _minPlayerDistance = VISION_RADIUS;
        }

        private IEnumerator PoolCoroutine()
        {
            while (true)
            {
                yield return POOL_CHECK_WAIT;

                int numberBodies = _spaceBodies.Count - 1;
                for(int i = numberBodies; i >= 0; i--)
                {
                    if (_spaceBodies.Count < i) continue;
                    
                    float distance = (_spaceBodies[i].Position - _playerTransform.position).magnitude;
                    
                    if (distance < VISION_RADIUS) continue;
                    if (distance > EXTRA_RADIUS)
                    {
                        DestroySpaceBody(_spaceBodies[i]);
                        CreateSpaceBody();
                    }
                }
            }
        }

        private void DestroySpaceBody(SpaceBodyControllerBase spaceBody)
        {
            _spaceBodyRandomizer.Remove(spaceBody.Type);
            _spaceBodies.Remove(spaceBody);
            spaceBody.Destroy();
        }

        private void CreateSpaceBody()
        {
            Vector3 randomPosition = GetPosition();
            SpaceBodyControllerBase spaceBodyModel = _spaceBodyRandomizer.GetRandomSpaceBody();
            SpaceBodyControllerBase spaceBody =
                Instantiate(spaceBodyModel, randomPosition, Quaternion.identity, transform);
            _spaceBodies.Add(spaceBody);
        }

        private Vector3 GetPosition()
        {
            Vector3 randomPosition;
            int save = 0;
            do
            {
                randomPosition = GetPlayerDistancedPosition();
                save++;
                if (save > 1000)
                {
                    Debug.LogError("ERROR");
                    return randomPosition;
                }
            }
            while (IsBelowMinimumBodyDistance(randomPosition));

            return randomPosition;
        }

        private Vector3 GetPlayerDistancedPosition()
        {
            Vector3 randomPosition;
            float playerDistance;
            int save = 0;
            do
            {

                float x = Random.Range(-EXTRA_RADIUS, EXTRA_RADIUS);
                float z = Random.Range(-EXTRA_RADIUS, EXTRA_RADIUS);
                randomPosition = _playerTransform.position + new Vector3(x, 0, z);
                playerDistance = Vector3.Distance(_playerTransform.position, randomPosition);
                
                save++;
                if (save > 1000)
                {
                    Debug.LogError("ERROR");
                    return randomPosition;
                }
            }
            while (playerDistance > _maxPlayerDistance || playerDistance < _minPlayerDistance);

            return randomPosition;
        }

        private bool IsBelowMinimumBodyDistance(Vector3 bodyPosition)
        {
            foreach (SpaceBodyControllerBase spaceBody in _spaceBodies)
            {
                float distance = Vector3.Distance(spaceBody.Position, bodyPosition);
                if (distance < MINIMUM_BODY_DISTANCE)
                {
                    return true;
                }
            }

            return false;
        }
    }
}