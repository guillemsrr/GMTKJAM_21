using System;
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

        [SerializeField]
        private List<SpaceBodyControllerBase> _spaceBodies = new List<SpaceBodyControllerBase>();

        private TemplatePool<SpaceBodyControllerBase> _planetsPool;
        private TemplatePool<SpaceBodyControllerBase> _starsPool;
        private TemplatePool<SpaceBodyControllerBase> _blackHolesPool;
        private TemplatePool<SpaceBodyControllerBase> _asteroidsPool;
        private TemplatePool<SpaceBodyControllerBase> _commetsPool;

        private const float VISION_RADIUS = 50f;
        private const float EXTRA_RADIUS = VISION_RADIUS + 10f;
        private const float MINIMUM_BODY_DISTANCE = 5f;
        private const int NUMBER_BODIES = 70;

        private const int NUMBER_ASTEROIDS = 30;
        private const int NUMBER_COMMETS = 10;
        private const int NUMBER_PLANETS = 15;
        private const int NUMBER_STARS = 10;
        private const int NUMBER_BLACKHOLES = 5;
   

        private readonly WaitForSeconds POOL_CHECK_WAIT = new WaitForSeconds(0.5f);

        private float _minPlayerDistance = 5f;
        private float _maxPlayerDistance = VISION_RADIUS;
        private SpaceBodyRandomizer _spaceBodyRandomizer;
        
        
        private void Awake()
        {
            _spaceBodyReferences.Initialize();
            _spaceBodyRandomizer = new SpaceBodyRandomizer(_spaceBodyReferences, NUMBER_BODIES);


            GameObject asteroid = _spaceBodyReferences.GetSpaceBody(SpaceBodyControllerBase.SpaceBodyType.Asteroid).gameObject;
            _asteroidsPool = new TemplatePool<SpaceBodyControllerBase>();
            _asteroidsPool.Init(asteroid, transform, NUMBER_ASTEROIDS);

            GameObject commet = _spaceBodyReferences.GetSpaceBody(SpaceBodyControllerBase.SpaceBodyType.Commet).gameObject;
            _commetsPool = new TemplatePool<SpaceBodyControllerBase>();
            _commetsPool.Init(commet, transform, NUMBER_COMMETS);

            GameObject planet = _spaceBodyReferences.GetSpaceBody(SpaceBodyControllerBase.SpaceBodyType.Planet).gameObject;
            _planetsPool = new TemplatePool<SpaceBodyControllerBase>();
            _planetsPool.Init(planet, transform, NUMBER_PLANETS);

            GameObject star = _spaceBodyReferences.GetSpaceBody(SpaceBodyControllerBase.SpaceBodyType.Star).gameObject;
            _starsPool = new TemplatePool<SpaceBodyControllerBase>();
            _starsPool.Init(star, transform, NUMBER_STARS);

            GameObject blackhole = _spaceBodyReferences.GetSpaceBody(SpaceBodyControllerBase.SpaceBodyType.BlackHole).gameObject;
            _blackHolesPool = new TemplatePool<SpaceBodyControllerBase>();
            _blackHolesPool.Init(blackhole, transform, NUMBER_BLACKHOLES);

            StartCoroutine(PoolCoroutine());
        }

        private void Start()
        {
            InitialGeneration();
        }

        private void InitialGeneration()
        {
            for (int i = 0; i < NUMBER_PLANETS; i++)
            {
                CreateSpaceBody(_planetsPool);
            }

            for (int i = 0; i < NUMBER_STARS; i++)
            {
                CreateSpaceBody(_starsPool);
            }

            for (int i = 0; i < NUMBER_BLACKHOLES; i++)
            {
                CreateSpaceBody(_blackHolesPool);
            }

            for (int i = 0; i < NUMBER_ASTEROIDS; i++)
            {
                CreateSpaceBody(_asteroidsPool);
            }
            for (int i = 0; i < NUMBER_COMMETS; i++)
            {
                CreateSpaceBody(_commetsPool);
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
                for (int i = numberBodies; i >= 0; i--)
                {
                    if (_spaceBodies.Count < i) continue;
                    if (_spaceBodies[i] == null)
                    {
                        _spaceBodies.RemoveAt(i);
                        continue;
                    }

                    float distance = (_spaceBodies[i].Position - _playerTransform.position).magnitude;

                    if (distance < VISION_RADIUS) continue;
                    if (distance > EXTRA_RADIUS)
                    {
                        switch(_spaceBodyRandomizer.GetRandomSpaceBody())
                        {
                            case SpaceBodyControllerBase.SpaceBodyType.Planet:
                                DestroySpaceBody(_planetsPool, _spaceBodies[i]);
                                CreateSpaceBody(_planetsPool);
                                break;
                            case SpaceBodyControllerBase.SpaceBodyType.Star:
                                DestroySpaceBody(_starsPool, _spaceBodies[i]);
                                CreateSpaceBody(_starsPool);
                                break;
                            case SpaceBodyControllerBase.SpaceBodyType.Asteroid:
                                DestroySpaceBody(_asteroidsPool, _spaceBodies[i]);
                                CreateSpaceBody(_asteroidsPool);
                                break;
                            case SpaceBodyControllerBase.SpaceBodyType.Commet:
                                DestroySpaceBody(_commetsPool, _spaceBodies[i]);
                                CreateSpaceBody(_commetsPool);
                                break;
                            case SpaceBodyControllerBase.SpaceBodyType.BlackHole:
                                DestroySpaceBody(_blackHolesPool, _spaceBodies[i]);
                                CreateSpaceBody(_blackHolesPool);
                                break;
                            default:
                                break;
                        }

                    }
                }
            }
        }

        private void DestroySpaceBody(TemplatePool<SpaceBodyControllerBase> objectsPool, SpaceBodyControllerBase spaceBody)
        {
            _spaceBodies.Remove(spaceBody);
            objectsPool.ReturnToPool(spaceBody);
        }

        public void DestroySpaceBody(SpaceBodyControllerBase.SpaceBodyType spaceBodyType, SpaceBodyControllerBase spaceBody)
        {
            switch (spaceBodyType)
            {
                case SpaceBodyControllerBase.SpaceBodyType.Planet:
                    DestroySpaceBody(_planetsPool, spaceBody);
                    CreateSpaceBody(_planetsPool);
                    break;
                case SpaceBodyControllerBase.SpaceBodyType.Star:
                    DestroySpaceBody(_starsPool, spaceBody);
                    CreateSpaceBody(_starsPool);
                    break;
                case SpaceBodyControllerBase.SpaceBodyType.Asteroid:
                    DestroySpaceBody(_asteroidsPool, spaceBody);
                    CreateSpaceBody(_asteroidsPool);
                    break;
                case SpaceBodyControllerBase.SpaceBodyType.Commet:
                    DestroySpaceBody(_commetsPool, spaceBody);
                    CreateSpaceBody(_commetsPool);
                    break;
                case SpaceBodyControllerBase.SpaceBodyType.BlackHole:
                    DestroySpaceBody(_blackHolesPool, spaceBody);
                    CreateSpaceBody(_blackHolesPool);
                    break;
                default:
                    break;
            }
        }

        private void CreateSpaceBody(TemplatePool<SpaceBodyControllerBase> objectsPool)
        {
            Vector3 randomPosition = GetPosition();            
            SpaceBodyControllerBase spaceBody = objectsPool.Instantiate(randomPosition, Quaternion.identity);
            spaceBody.Initialize();
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
                if (!spaceBody) return false;
                float distance = Vector3.Distance(spaceBody.Position, bodyPosition);
                if (distance < MINIMUM_BODY_DISTANCE)
                {
                    return true;
                }
            }

            return false;
        }

        private void OnDestroy()
        {
            _planetsPool.DestroyAll();
            _asteroidsPool.DestroyAll();
            _starsPool.DestroyAll();
            _blackHolesPool.DestroyAll();
        }
    }
}