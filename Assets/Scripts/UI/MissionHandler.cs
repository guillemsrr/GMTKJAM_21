using System;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class MissionHandler: MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        [SerializeField] private SpaceBodyLogo _commetLogo;
        [SerializeField] private SpaceBodyLogo _asteroidLogo;
        [SerializeField] private SpaceBodyLogo _magmaLogo;
        [SerializeField] private SpaceBodyLogo _iceLogo;
        [SerializeField] private SpaceBodyLogo _radioActiveLogo;
        [SerializeField] private SpaceBodyLogo _waterLogo;
        [SerializeField] private SpaceBodyLogo _tierraLogo;

        private SpaceBodyLogo _logo;
        private SpaceBodyControllerBase.SpaceBodyType _spaceBodyType;
        private Planet.PlanetType _planetType;
        
        public bool IsAccomplished { get; private set; }
        
        public void SetMissionType(SpaceBodyControllerBase.SpaceBodyType type)
        {
            _spaceBodyType = type;
            switch (type)
            {
                case SpaceBodyControllerBase.SpaceBodyType.Asteroid:
                    _logo = _asteroidLogo;
                    break;
                case SpaceBodyControllerBase.SpaceBodyType.Commet:
                    _logo = _commetLogo;
                    break;
                case SpaceBodyControllerBase.SpaceBodyType.Planet:
                    SetPlanetSubType();
                    break;
            }

            if (_logo == null)
            {
                Debug.LogError(type);
            }
            
            _image.sprite = _logo.Unaquired;
        }
        
        public void Accomplish()
        {
            IsAccomplished = true;
            _image.sprite = _logo.Aquired;
        }

        public bool Compare(SpaceBodyControllerBase spaceBodyControllerBase)
        {
            if (spaceBodyControllerBase.Type == _spaceBodyType)
            {
                if (spaceBodyControllerBase.Type == SpaceBodyControllerBase.SpaceBodyType.Planet && _planetType != ((Planet) spaceBodyControllerBase).SubType)
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        private void SetPlanetSubType()
        {
            _planetType = RandomEnum.GetRandomFromEnum<Planet.PlanetType>();
            switch (_planetType)
            {
                case Planet.PlanetType.Ice:
                    _logo = _iceLogo;
                    break;
                case Planet.PlanetType.Magma:
                    _logo = _magmaLogo;
                    break;
                case Planet.PlanetType.Radioactive:
                    _logo = _radioActiveLogo;
                    break;
                case Planet.PlanetType.Rings:
                    _logo = _tierraLogo;
                    break;
                case Planet.PlanetType.Water:
                    _logo = _waterLogo;
                    break;
            }
        }
    }
}