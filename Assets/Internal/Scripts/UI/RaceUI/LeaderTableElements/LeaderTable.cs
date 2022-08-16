using Cars_5_5.RaceUI.LeaderTableElements;
using Cars_5_5.UI.Base;
using UnityEngine;
using System.Collections.Generic;
using System;
using Cars_5_5.Data;
using Cars_5_5.UI.Menu.Handlers;

namespace Cars_5_5.UI.RaceUI.LeaderTableElements
{
    public class LeaderTable : BaseUIElement
    {
        [SerializeField]
        private LeaderTableRecord _recordPrefab;

        [SerializeField, Space(15)]
        private RecordContainer _recordContainer;
        [SerializeField]
        private int _stepBetweenRecords = 10;
        [SerializeField]
        private LeaderTableMenuHandler _tableMenu;

        public EventHandler OnRaceRestartSelectedInMenu;

        private void OnDisable()
        {
            OnRaceRestartSelectedInMenu = null;
        }

        public override void SetVisible(bool value)
        {
            gameObject.SetActive(value);
            if (value)
            {
                RefreshTable();
                SubscribeToTableMenuEvents();
            }
        }

        private void RefreshTable()
        {
            Clear();
            Load();
        }

        private void Clear()
        {
            for (int i = 0; i < _recordContainer.transform.childCount; i++)
            {
                Destroy(_recordContainer.transform.GetChild(i).gameObject);
            }
        }

        private void Load()
        {
            List<LeaderTableRecordData> records = PlayerResult.GetRecords();
            records.Sort((a, b) => TimeSpan.Compare(a.BestLap, b.BestLap));

            float recordHeight = (_recordPrefab.transform as RectTransform).rect.height;

            float recordContainerHeight = 0;

            for (int i = 0; i < records.Count; i++)
            {
                LeaderTableRecord record = Instantiate(_recordPrefab, _recordContainer.transform);
                record.SetData(records[i]);
                record.transform.localPosition -= new Vector3(0, (recordHeight + _stepBetweenRecords) * i, 0);
                recordContainerHeight += recordHeight + _stepBetweenRecords;
            }
            RectTransform containerTransform = _recordContainer.transform as RectTransform;
            containerTransform.sizeDelta = new Vector2(containerTransform.rect.width, recordContainerHeight);
        }

        private void SubscribeToTableMenuEvents()
        {
            _tableMenu.RaceRestartSelected += OnRaceRestartSelected;
        }

        private void OnRaceRestartSelected(object sender, EventArgs e)
        {
            OnRaceRestartSelectedInMenu?.Invoke(this, EventArgs.Empty);
        }
    }
}
