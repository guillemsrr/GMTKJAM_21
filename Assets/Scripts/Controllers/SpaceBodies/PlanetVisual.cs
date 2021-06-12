using UnityEngine;

namespace Controllers
{
    [System.Serializable]
    public class PlanetVisual
    {
        [SerializeField] private Material _material;
        [SerializeField] private AudioClip _eatAudioClip;
        [SerializeField] private Mesh _mesh;

        public AudioClip EatAudioClip => _eatAudioClip;
        public Material Material => _material;
        public Mesh Mesh => _mesh;
    }
}