using Cars_5_5.Data;
using Cars_5_5.UI.Base;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cars_5_5.UI.RaceUI.LeaderTableElements
{
    public class AddRecordDialog : BaseUIElement
    {
        [SerializeField]
        private TMP_InputField _inputField;

        public EventHandler RecordAdded;

        public TimeSpan BestTimeLap { get; set; }

        private void OnDisable()
        {
            RecordAdded = null;
        }

        public override void SetVisible(bool value)
        {
            gameObject.SetActive(value);
            if (value)
            {
                _inputField.Select();
            }
        }

        private void OnAccept(InputValue input)
        {
            AddPlayerRecord();
            RecordAdded?.Invoke(this, EventArgs.Empty);
        }

        private void AddPlayerRecord()
        {
            LeaderTableRecordData newRecord = new LeaderTableRecordData()
            {
                RacerName = _inputField.text,
                BestLap = BestTimeLap,
            };
            PlayerResult.AddRecord(newRecord);
        }
    }
}
