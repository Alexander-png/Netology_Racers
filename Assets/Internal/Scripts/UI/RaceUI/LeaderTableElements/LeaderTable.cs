using Cars_5_5.RaceUI.LeaderTableElements;
using Cars_5_5.UI.Base;
using UnityEngine;
using System.Collections.Generic;
using System;
using Cars_5_5.Data;

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

        public override void SetVisible(bool value)
        {
            gameObject.SetActive(value);
            if (value)
            {
                RefreshTable();
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
            List<LeaderTableRecordData> records = PlayerResultLoader.GetRecords();
            records.Sort((a, b) => TimeSpan.Compare(a.BestLap, b.BestLap));

            float recordHeight = (_recordPrefab.transform as RectTransform).rect.height;

            for (int i = 0; i < records.Count; i++)
            {
                LeaderTableRecord record = Instantiate(_recordPrefab, _recordContainer.transform);
                record.SetData(records[i]);
                record.transform.localPosition -= new Vector3(0, (recordHeight + _stepBetweenRecords) * i, 0);
            }
        }
    }
}
