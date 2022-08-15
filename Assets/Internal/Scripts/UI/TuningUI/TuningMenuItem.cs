using Cars_5_5.UI.Menu.Items;
using TMPro;
using UnityEngine;

namespace Cars_5_5.UI.TuningUI
{
    public class TuningMenuItem : MenuItemMarker
    {
        [SerializeField]
        private TMP_Text _value;

        public string Value
        {
            get => _value.text;
            set
            {
                if (_value != null)
                {
                    _value.text = value;
                }
            }
        }
    }
}
