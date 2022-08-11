using Cars_5_5.UI.MainMenu.ActionTypes;
using UnityEngine;
using UnityEngine.UI;

namespace Cars_5_5.UI.MainMenu.Items
{
    public class MenuItemMarker : MonoBehaviour
    {
        [SerializeField]
        private Image _marker;
        [SerializeField]
        private bool _isSelected;
        [SerializeField]
        private Actions _actionType;
        [SerializeField]
        private int _selectionIndex;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnSelectedChanged(value);
            }
        }

        public Actions ActionType => _actionType;

        public int SelectionIndex => _selectionIndex;

        private void Start()
        {
            if (_isSelected)
            {
                IsSelected = _isSelected;
            }
        }

        private void OnSelectedChanged(bool value)
        {
            _marker.enabled = value;
        }
    }
}