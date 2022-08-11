using Cars_5_5.Assistance;
using Cars_5_5.UI.MainMenu.ActionTypes;
using Cars_5_5.UI.MainMenu.Items;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Cars_5_5.UI.MainMenu
{
    public class MainMenuComponent : MonoBehaviour
    {
        [SerializeField]
        private MenuItemMarker[] _menuItems;

        private int _selectionIndex = 0;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
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

        private void OnSelectionChanging(InputValue value)
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

        private void OnSelect(InputValue value)
        {
            ProcessSelection();
        }

        private void ProcessSelection()
        {
            switch (_menuItems[_selectionIndex].ActionType)
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
                    EditorApplication.ExitPlaymode();
#else
                    Application.Quit(0);
#endif
                    break;
            }
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            FindAndSortMenuItems();
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
#endif
    }
}

