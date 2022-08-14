using System;
using UnityEngine;

namespace Cars_5_5.BotAssistance
{
    public class WaypointComponent : MonoBehaviour
    {
        [SerializeField]
        private WaypointComponent[] _nextPoints;
        [SerializeField]
        private bool _isFirst;
        [SerializeField]
        private float _arriveRadius;

        public WaypointComponent[] NextPoints
        {
            get
            {
                if (_nextPoints == null)
                {
                    return null;
                }
                WaypointComponent[] toReturn = new WaypointComponent[_nextPoints.Length];
                Array.Copy(_nextPoints, toReturn, _nextPoints.Length);
                return toReturn;
            }
        }

        public bool IsFirst => _isFirst;

        public float ArriveRadius => _arriveRadius;

        public void OnDrawGizmos()
        {
            Color pointColor = Color.blue;
            pointColor.a = 0.5f;
            Gizmos.color = pointColor;
            Gizmos.DrawSphere(transform.position, ArriveRadius);
        }
    }
}
