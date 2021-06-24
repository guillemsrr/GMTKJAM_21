using UnityEngine;

namespace UI
{
    public class ReturnController: MonoBehaviour
    {
        public void ReturnToMenu()
        {
            LevelManager.Instance.LoadMenu();
        }
    }
}