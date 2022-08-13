using Cars_5_5.CarComponents.Assistance;
using Cars_5_5.Input;
using Cars_5_5.UI.Base;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Cars_5_5.UI.RaceUI
{
    public class SpeedometerComponent : BaseUIElement
    {
        private const int C_ValueScale = 4;

        [SerializeField]
        private float _updateValueFrequency = 0.03f;
        [SerializeField]
        private TMP_Text _speedometerText;

        private Coroutine _updateValueCoroutine;

        private CarObserver _playerCar;

        private void Start()
        {
            FindPlayerCar();
            _updateValueCoroutine = StartCoroutine(UpdateSpeedometerValueCoroutine());
        }

        private void OnDisable()
        {
            if (_updateValueCoroutine != null)
            {
                StopCoroutine(_updateValueCoroutine);
            }
        }

        private IEnumerator UpdateSpeedometerValueCoroutine()
        {
            while (true)
            {
                _speedometerText.text = Mathf.RoundToInt(_playerCar.SignedCarSpeed * C_ValueScale).ToString();
                yield return new WaitForSeconds(_updateValueFrequency);
            }
        }

        private void FindPlayerCar()
        {
            _playerCar = FindObjectOfType<PlayerInputHandler>().CarObserver;
        }

        public override void SetVisible(bool value)
        {
            _speedometerText.enabled = value;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _speedometerText = GetComponent<TMP_Text>();
        }
#endif
    }
}
