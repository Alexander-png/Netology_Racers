using Cars_5_5.UI.RaceUI.LeaderTableElements;
using System;
using TMPro;
using UnityEngine;

namespace Cars_5_5.RaceUI.LeaderTableElements
{
    public class LeaderTableRecord : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _racerNameText;
        [SerializeField]
        private TMP_Text _bestLapTimeText;

        private TimeSpan _bestLapTime;
        private string _racerName;

        public string RacerName
        {
            get => _racerName;
            set
            {
                _racerName = value;
                _racerNameText.text = value;
            }
        }

        public TimeSpan BestLap
        {
            get => _bestLapTime;
            set
            {
                _bestLapTime = value;
                _bestLapTimeText.text = _bestLapTime.ToString(@"mm\:ss\:fff");
            }
        }

        public void SetData(LeaderTableRecordData data)
        {
            RacerName = data.RacerName;
            BestLap = data.BestLap;
        }
    }
}
