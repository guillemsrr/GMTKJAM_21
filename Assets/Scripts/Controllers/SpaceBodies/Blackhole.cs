using UnityEngine;

namespace Controllers
{
    public class Blackhole: SpaceBodyControllerBase
    {
        public override void Destroy()
        {
            SetGravityForce(100f);
        }
        
    }
}