using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BarsController: MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        [Header("Life sprites")]
        [SerializeField] private Sprite _lowLife;
        [SerializeField] private Sprite _midLife;
        [SerializeField] private Sprite _fullLife;

        public void SetBars(int barsLevel)
        {
            if (barsLevel <= 0)
            {
                _image.gameObject.SetActive(false);
                return;
            }
            else
            {
                _image.gameObject.SetActive(true);
            }
            
            switch (barsLevel)
            {
                case 1:
                    _image.sprite = _lowLife;
                    break;
                case 2:
                    _image.sprite = _midLife;
                    break;
                case 3:
                    _image.sprite = _fullLife;
                    break;
            }
        }
    }
}