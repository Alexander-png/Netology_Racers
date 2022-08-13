using Cars_5_5.UI.Base;
using System;
using UnityEngine.InputSystem;

namespace Cars_5_5.UI.RaceUI
{
    public class StartRaceInviter : BaseUIElement
    {
        public EventHandler StartAccept;

        private void OnDisable()
        {
            StartAccept = null;
        }

        private void OnAccept(InputValue value)
        {
            StartAccept?.Invoke(this, EventArgs.Empty);
        }

        public override void SetVisible(bool value)
        {
            gameObject.SetActive(value);
            if (!value)
            {
                StartAccept = null;
            }
        }
    }
}
