using System.Collections;
using UnityEngine;

namespace Controllers.VFX
{
    public class TemporalVFX: MonoBehaviour
    {
        [SerializeField] private float _deathTimer = 3f;

        private void Start()
        {
            StartCoroutine(DestroyTimer());
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(_deathTimer);
            
            Destroy(this);
        }
    }
}