using Cars_5_5.UI.Menu.ActionTypes;
using Cars_5_5.UI.Menu.Items;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cars_5_5.UI.Menu.Base
{
    [RequireComponent((typeof(PlayerInput)))]
    public class MenuComponent : MonoBehaviour
    {
        [SerializeField]
        private MenuItemMarker[] _menuItems;

        protected MenuItemMarker[] MenuItems
        {
            get
            {
                MenuItemMarker[] toReturn = new MenuItemMarker[_menuItems.Length];
                Array.Copy(_menuItems, toReturn, _menuItems.Length);
                return toReturn;
            }
        }

        private int _selectionIndex = 0;

        protected int SelectionIndex => _selectionIndex;

        public delegate void MenuEventHandler(Actions selectedAction);

        public event MenuEventHandler OptionSelected;

        protected virtual void OnDisable()
        {
            OptionSelected = null;
        }

        protected virtual void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            FindAndSortMenuItems();
            if (_menuItems?.Length != 0)
            {
                try
                {
                    MenuItemMarker selectedMarker = _menuItems.First(i => i.IsSelected);
                    _selectionIndex = Array.IndexOf(_menuItems, selectedMarker);
                }
                catch (InvalidOperationException)
                {

                }
            }
        }

        private void FindAndSortMenuItems()
        {
            try
            {
                _menuItems = FindObjectsOfType<MenuItemMarker>();
                Array.Sort(_menuItems, new Comparison<MenuItemMarker>((item1, item2) => item1.SelectionIndex.CompareTo(item2.SelectionIndex)));
            }
            catch (InvalidOperationException)
            {
                Debug.LogError("No selected item found, or no menu items found!");
            }
        }

        protected virtual void OnSelectionChanging(InputValue value)
        {
            int newIndex = _selectionIndex + Convert.ToInt32(value.Get<float>());

            if (newIndex == _selectionIndex)
            {
                return;
            }

            _menuItems[_selectionIndex].IsSelected = false;
            _selectionIndex = newIndex;

            if (_selectionIndex < 0)
            {
                _selectionIndex = _menuItems.Length - 1;
            }
            if (_selectionIndex > _menuItems.Length - 1)
            {
                _selectionIndex = 0;
            }
            _menuItems[_selectionIndex].IsSelected = true;
        }

        protected virtual void OnSelect(InputValue value)
        {
            OptionSelected?.Invoke(_menuItems[_selectionIndex].ActionType);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            FindAndSortMenuItems();
        }
#endif
    }
}

