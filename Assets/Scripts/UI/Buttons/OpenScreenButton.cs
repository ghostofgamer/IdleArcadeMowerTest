using UnityEngine;

namespace UI.Buttons
{
    public class OpenScreenButton : AbstractButton
    {
        [SerializeField] private AbstractScreen _screen;

        public override void Click()
        {
            if (_screen != null)
                _screen.OpenScreen();
        }
    }
}