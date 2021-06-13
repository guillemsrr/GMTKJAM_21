using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Controllers
{
    public class Planet: SpaceBodyControllerBase
    {
        public enum PlanetType
        {
            Ice,
            Magma,
            Rings,
            Water,
            Radioactive
        }
        
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private MeshFilter _meshFilter;

        [Header("Visuals")]
        [SerializeField] private PlanetVisual _icePlanetVisuals;
        [SerializeField] private PlanetVisual _magmaPlanetVisuals;
        [SerializeField] private PlanetVisual _ringsPlanetVisuals;
        [SerializeField] private PlanetVisual _waterPlanetVisuals;
        [SerializeField] private PlanetVisual _radioActivePlanetVisuals;
        
        private Dictionary<PlanetType, PlanetVisual> _planetVisualsByType;

        [SerializeField]
        private GameObject _sfx;
        
        public PlanetType SubType { get; private set; }

        public override void Initialize()
        {
            ApplyRandomVisuals();
        }

        private void ApplyRandomVisuals()
        {
            SubType = RandomEnum.GetRandomFromEnum<PlanetType>();
            PlanetVisual planetVisual = _planetVisualsByType[SubType];

            _meshRenderer.material = planetVisual.Material;
            _meshFilter.mesh = planetVisual.Mesh;
            _eatAudio = planetVisual.EatAudioClip;

            if (_sfx != null) GameObject.Destroy(_sfx);
            if (planetVisual.Sfx)
            {
                _sfx = GameObject.Instantiate(planetVisual.Sfx, transform.position,transform.rotation ,transform);
            }

        }       

        private void Awake()
        {
            base.Awake();
            
            _planetVisualsByType = new Dictionary<PlanetType, PlanetVisual>
            {
                {PlanetType.Ice, _icePlanetVisuals},
                {PlanetType.Magma, _magmaPlanetVisuals},
                {PlanetType.Rings, _ringsPlanetVisuals},
                {PlanetType.Water, _waterPlanetVisuals},
                {PlanetType.Radioactive, _radioActivePlanetVisuals},
            };
        }
    }
}