using Cars_5_5.UI.Menu.ActionTypes;
using Cars_5_5.UI.Menu.Core;
using UnityEngine;

namespace Cars_5_5.UI.Menu.Handlers.Base
{
    [RequireComponent(typeof(MenuComponent))]
    public abstract class BaseMenuHandler : MonoBehaviour
    {
        private MenuComponent _menu;
        private MenuComponent Menu
        {
            get
            {
                if (_menu == null)
                {
                    _menu = GetComponent<MenuComponent>();
                }   
                return _menu; 
            }
        }

        protected virtual void OnEnable()
        {
            Menu.OptionSelected += OnMenuOptionSelected;
        }

        protected virtual void OnDisable()
        {
            Menu.OptionSelected -= OnMenuOptionSelected;
        }

        protected abstract void OnMenuOptionSelected(Actions selectedAction);
    }
}
