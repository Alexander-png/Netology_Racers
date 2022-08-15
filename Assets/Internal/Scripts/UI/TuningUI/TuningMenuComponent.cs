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

        private void Start()
        {
            UpdateValues();
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
            UpdateValues();
        }

        private void UpdateValues()
        {
            foreach (var item in MenuItems)
            {
                if (item is TuningMenuItem tuningItem)
                {
                    // tuningItem.Value = TODO: load values from player prefs.
                }
            }
        }
    }
}
