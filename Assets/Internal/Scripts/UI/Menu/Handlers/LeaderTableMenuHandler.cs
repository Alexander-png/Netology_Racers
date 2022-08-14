using Cars_5_5.Assistance;
using Cars_5_5.UI.Menu.ActionTypes;
using Cars_5_5.UI.Menu.Handlers.Base;
using System;

namespace Cars_5_5.UI.Menu.Handlers
{
    public class LeaderTableMenuHandler : BaseMenuHandler
    {
        public EventHandler RaceRestartSelected;

        protected override void OnDisable()
        {
            base.OnDisable();
            RaceRestartSelected = null;
        }

        protected override void OnMenuOptionSelected(Actions selectedAction)
        {
            switch (selectedAction)
            {
                case Actions.RestartRace:
                    RaceRestartSelected?.Invoke(this, EventArgs.Empty);
                    break;
                case Actions.ExitToMainMenu:
                    SceneHelper.SwitchScene("MenuScene");
                    break;
            }
        }
    }
}
