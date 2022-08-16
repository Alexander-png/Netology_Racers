using Cars_5_5.Assets.Internal.Scripts.Data;
using Cars_5_5.UI.Menu.ActionTypes;
using Cars_5_5.UI.Menu.Base;
using System;
using UnityEngine.InputSystem;

namespace Cars_5_5.UI.TuningUI
{
    public class TuningMenuComponent : MenuComponent
    {
        public delegate void TuningValueMenuEventHandler(int value);
        public delegate void SelectionChangingTuningMenuEventHandler(Actions action);

        public event TuningValueMenuEventHandler ValueChanging;
        public event SelectionChangingTuningMenuEventHandler SelectionChanging;

        protected override void Start()
        {
            base.Start();
            SelectionChanging?.Invoke(MenuItems[SelectionIndex].ActionType);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            ValueChanging = null;
            SelectionChanging = null;
        }

        protected override void OnSelectionChanging(InputValue value)
        {
            base.OnSelectionChanging(value);
            SelectionChanging?.Invoke(MenuItems[SelectionIndex].ActionType);
        }

        private void OnValueChanging(InputValue value)
        {
            ValueChanging?.Invoke(Convert.ToInt32(value.Get<float>()));
        }

        private void UpdateValuesInMenuItems(PlayerCarData newData)
        {
            foreach (var item in MenuItems)
            {
                if (item is TuningMenuItem tuningItem)
                {
                    switch (tuningItem.ActionType)
                    {
                        case Actions.ChangeMaxThrottle:
                            tuningItem.Value = newData.MaxMotorTorgue.ToString();
                            break;
                        case Actions.ChangeMaxTurnAngle:
                            tuningItem.Value = newData.MaxTurnAngle.ToString();
                            break;
                        case Actions.ChangeMaxBrakeAxis:
                            tuningItem.Value = newData.MaxBrakeTorgue.ToString();
                            break;
                        case Actions.ChangeCarMass:
                            tuningItem.Value = newData.CarMass.ToString();
                            break;
                        case Actions.ChangeDownForce:
                            tuningItem.Value = newData.DownForce.ToString();
                            break;
                    }
                }
            }
        }

        public void OnValuesChanged(PlayerCarData newData)
        {
            UpdateValuesInMenuItems(newData);
        }
    }
}
