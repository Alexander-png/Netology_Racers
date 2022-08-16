using Cars_5_5.Assets.Internal.Scripts.Data;
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
        private TuningMenuComponent ParentMenu
        {
            get
            {
                if (_parentMenu == null)
                {
                    _parentMenu = Menu as TuningMenuComponent;
                }
                return _parentMenu;
            }
        }

        private PlayerCarData _currentPlayerCarData;

        private void Start()
        {
            LoadCarData();
            RefreshVisualData();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ParentMenu.ValueChanging += OnValueChangingHandler;
            ParentMenu.SelectionChanging += OnSelectionChangingHandler;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            ParentMenu.ValueChanging -= OnValueChangingHandler;
            ParentMenu.SelectionChanging -= OnSelectionChangingHandler;
        }

        protected override void OnMenuOptionSelected(Actions selectedAction)
        {
            switch (selectedAction)
            {
                case Actions.ExitToMainMenu:
                    SaveCarData();
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
            switch (_selectedAction)
            {
                case Actions.ChangeMaxThrottle:
                    _currentPlayerCarData.MaxMotorTorgue += value * 50;
                    break;
                case Actions.ChangeMaxTurnAngle:
                    _currentPlayerCarData.MaxTurnAngle += value * 0.5f;
                    break;
                case Actions.ChangeMaxBrakeAxis:
                    _currentPlayerCarData.MaxBrakeTorgue += value * 50;
                    break;
                case Actions.ChangeCarMass:
                    _currentPlayerCarData.CarMass += value * 50;
                    break;
                case Actions.ChangeDownForce:
                    _currentPlayerCarData.Downforce += value * 0.5f;
                    break;
            }
            RefreshVisualData();
        }

        private void RefreshVisualData()
        {
            ParentMenu.OnValuesChanged(_currentPlayerCarData);
        }

        private void LoadCarData()
        {
            _currentPlayerCarData = PlayerTuningData.LoadPlayerCarData();
        }

        private void SaveCarData()
        {
            PlayerTuningData.SavePlayerCarData(_currentPlayerCarData);
        }
    }
}
