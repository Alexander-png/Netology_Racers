using Cars_5_5.Assistance;
using Cars_5_5.UI.Menu.ActionTypes;
using Cars_5_5.UI.Menu.Handlers.Base;
using UnityEngine;

namespace Cars_5_5.UI.Menu.Handlers
{
    public class MainMenuHandler : BaseMenuHandler
    {
        protected override void OnMenuOptionSelected(Actions selectedAction)
        {
            switch (selectedAction)
            {
                case Actions.Start_Game:
                    SceneHelper.SwitchScene("Track");
                    break;
                case Actions.Tuning:
                    Debug.Log("TODO: tuning");
                    break;
                case Actions.Settings:
                    Debug.Log("TODO: settings");
                    break;
                case Actions.Exit:
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.ExitPlaymode();
#else
                    Application.Quit(0);
#endif
                    break;
            }
        }
    }
}
