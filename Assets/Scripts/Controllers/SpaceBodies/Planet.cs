using System;
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
        [SerializeField] private Material _iceMaterial;
        [SerializeField] private Material _magmaMaterial;
        [SerializeField] private Material _ringsMaterial;
        [SerializeField] private Material _waterMaterial;
        [SerializeField] private Material _radioactiveMaterial;

        [SerializeField] private AudioClip _iceAudio;
        [SerializeField] private AudioClip _magmaAudio;
        [SerializeField] private AudioClip _ringsAudio;
        [SerializeField] private AudioClip _waterAudio;
        [SerializeField] private AudioClip _radioActiveAudio;

        private Dictionary<PlanetType, Material> _materialsByType;
        private Dictionary<PlanetType, AudioClip> _soundsByType;

        public override void Initialize()
        {
            Material randomMaterial = GetRandomMaterial();
            _meshRenderer.material = randomMaterial;
        }

        private Material GetRandomMaterial()
        {
            PlanetType planetType = RandomEnum.GetRandomFromEnum<PlanetType>();

            return _materialsByType[planetType];
        }

        private void Awake()
        {
            _materialsByType = new Dictionary<PlanetType, Material>
            {
                {PlanetType.Ice, _iceMaterial},
                {PlanetType.Magma, _magmaMaterial},
                {PlanetType.Rings, _ringsMaterial},
                {PlanetType.Water, _waterMaterial},
                {PlanetType.Radioactive, _radioactiveMaterial},
            };

            _soundsByType = new Dictionary<PlanetType, AudioClip>
            {
                {PlanetType.Ice, _iceAudio},
                {PlanetType.Magma, _magmaAudio},
                {PlanetType.Rings, _ringsAudio},
                {PlanetType.Water, _waterAudio},
                {PlanetType.Radioactive, _radioActiveAudio},
            };
        }
    }
}