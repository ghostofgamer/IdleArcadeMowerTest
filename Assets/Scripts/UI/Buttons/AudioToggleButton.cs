using AudioContent;
using UnityEngine;

namespace UI.Buttons
{
    public class AudioToggleButton : MonoBehaviour
    {
        [SerializeField] private bool _isSFX;
        
        public void Click()
        {
            if (_isSFX)
                AudioService.Instance.ToggleSFX();
            else
                AudioService.Instance.ToggleMusic();
        }
    }
}