using System;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MissionHandler: MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        [SerializeField] private SpaceBodyLogo _commetLogo;
        [SerializeField] private SpaceBodyLogo _asteroidLogo;
        [SerializeField] private SpaceBodyLogo _magmaLogo;

        private Dictionary<SpaceBodyControllerBase.SpaceBodyType, SpaceBodyLogo> _spaceBodyLogosDictionary;
        private SpaceBodyLogo _logo;
        
        public void SetMissionType(SpaceBodyControllerBase.SpaceBodyType type)
        {
            return;
            _logo = _spaceBodyLogosDictionary[type];
            _image.sprite = _logo.Unaquired;
        }
        
        public void Accomplish()
        {
            return;
            _image.sprite = _logo.Aquired;
        }

        private void Awake()
        {
            _spaceBodyLogosDictionary = new Dictionary<SpaceBodyControllerBase.SpaceBodyType, SpaceBodyLogo>
            {
                {SpaceBodyControllerBase.SpaceBodyType.Asteroid, _asteroidLogo},
                //TODO
            };
        }
    }
}