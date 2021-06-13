using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Controllers
{
    public class PlayerVisualsHandler: MonoBehaviour
    {
        private float  MAX_SCALE = 3.5f;
        private float SCALE_SPEED = 2f;
        private float UNSCALE_SPEED = 7f;
        
        private float _scale = 1;
        

        private Coroutine _scaleCoroutine;
        
        public void ChangeVisualFromLevelUp(int level)
        {
            if (_scaleCoroutine != null)
            {
                StopCoroutine(_scaleCoroutine);
            }

            _scaleCoroutine = StartCoroutine(ScaleAnimation(level + 1, SCALE_SPEED));
        }
        
        public void ChangeVisualFromEating(SpaceBodyControllerBase.SpaceBodyType eatenType)
        {
            
        }

        public void DeathVisuals()
        {
            
        }

        public void BoostScaleOver()
        {
            if (_scale > MAX_SCALE)
            {
                ScaleAnimation(MAX_SCALE, UNSCALE_SPEED);
            }
        }

        private IEnumerator ScaleAnimation(float targetScale, float speed)
        {
            while (_scale != targetScale)
            {
                yield return null;
                _scale = math.lerp(_scale, targetScale, speed*Time.deltaTime);
                transform.localScale = Vector3.one * _scale;
            }
        }
    }
}