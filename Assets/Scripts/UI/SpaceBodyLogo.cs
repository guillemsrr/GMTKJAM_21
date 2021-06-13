using UnityEngine;

namespace UI
{
    [System.Serializable]
    public class SpaceBodyLogo
    {
        [SerializeField] private Sprite _unaquired;
        [SerializeField] private Sprite _aquired;

        public Sprite Unaquired => _unaquired;
        public Sprite Aquired => _aquired;
    }
}