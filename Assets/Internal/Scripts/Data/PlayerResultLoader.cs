using Cars_5_5.UI.RaceUI.LeaderTableElements;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_5_5.Data
{
    public class PlayerResultLoader
    {
        private const string PlayerResultsKey = "LeaderTableData";
        private static List<LeaderTableRecordData> _records;

        private static void UpdateRecords()
        {
            string serializedRecord = JsonConvert.SerializeObject(_records);
            PlayerPrefs.SetString(PlayerResultsKey, serializedRecord);
            GetRecords();
        }

        public static List<LeaderTableRecordData> GetRecords()
        {
            if (_records == null)
            {
                _records = JsonConvert.DeserializeObject<List<LeaderTableRecordData>>(PlayerPrefs.GetString(PlayerResultsKey));
                if (_records == null)
                {
                    _records = new List<LeaderTableRecordData>();
                }
            }
            return new List<LeaderTableRecordData>(_records);
        }

        public static void AddRecord(LeaderTableRecordData newRecord)
        {
            if (_records == null)
            {
                GetRecords();
            }
            _records.Add(newRecord);
            UpdateRecords();
        }
    }
}
