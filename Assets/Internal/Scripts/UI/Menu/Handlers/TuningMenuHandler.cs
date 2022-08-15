using Cars_5_5.Assistance;
using Cars_5_5.UI.Menu.ActionTypes;
using Cars_5_5.UI.Menu.Handlers.Base;
using Cars_5_5.UI.TuningUI;

namespace Cars_5_5.UI.Menu.Handlers
{
    public class TuningMenuHandler : BaseMenuHandler
    {
        private Actions _selectedAction;
        private TuningMenuComponent _parentMenu;

        protected override void OnMenuOptionSelected(Actions selectedAction)
        {
            switch (selectedAction)
            {
                case Actions.ExitToMainMenu:
                    SceneHelper.SwitchScene("MenuScene");
                    break;
            }
        }

        private void OnSelectionChangingHandler(Actions action)
        {
            _selectedAction = action;
        }

        private void OnValueChangingHandler(int value)
        {
            UnityEngine.Debug.Log(value);

            // TODO: change car values
            switch (_selectedAction)
            {
                case Actions.ChangeMaxThrottle:

                    break;
                case Actions.ChangeMaxTurnAngle:

                    break;
                case Actions.ChangeMaxBrakeAxis:

                    break;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _parentMenu = Menu as TuningMenuComponent;

            _parentMenu.ValueChanging += OnValueChangingHandler;
            _parentMenu.SelectionChanging += OnSelectionChangingHandler;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _parentMenu.ValueChanging -= OnValueChangingHandler;
            _parentMenu.SelectionChanging -= OnSelectionChangingHandler;
        }
    }
}
