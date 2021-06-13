using System.Collections.Generic;
using UnityEngine;
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
        

        public override void Initialize()
        {
            ApplyRandomVisuals();
        }

        private void ApplyRandomVisuals()
        {
            PlanetType planetType = RandomEnum.GetRandomFromEnum<PlanetType>();
            PlanetVisual planetVisual = _planetVisualsByType[planetType];

            _meshRenderer.material = planetVisual.Material;
            _meshFilter.mesh = planetVisual.Mesh;
            _eatAudio = planetVisual.EatAudioClip;
        }

        private void Awake()
        {
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