using Cars_5_5.BotAssistance;
using Cars_5_5.Input.Base;
using UnityEngine;

namespace Cars_5_5.Input
{
    public class BotInputHandler : BaseCarInput
    {
        [SerializeField]
        private WaypointComponent _currentWaypoint;

        public void SetFirstWaypoint(WaypointComponent waypoint)
        {
            if (_currentWaypoint == null)
            {
                _currentWaypoint = waypoint;
            }   
        }

        private void Update()
        {
            BotLogic();
        }

        private void BotLogic()
        {
            SwitchToNextPointIfNeeded();
            SteeringLogic();
            VerticalAxisLogic();
        }

        private void SwitchToNextPointIfNeeded()
        {
            if (CalculateDistanceToCurrentPoint() < _currentWaypoint.ArriveRadius)
            {
                _currentWaypoint = _currentWaypoint.NextPoints[Random.Range(0, _currentWaypoint.NextPoints.Length)];
            }
        }

        private float CalculateDistanceToCurrentPoint()
        {
            return Vector3.Distance(transform.position, _currentWaypoint.transform.position);
        }

        private void SteeringLogic()
        {
            float maxSteering = WheelBehaviour.MaxSteeringAngle;

            Vector3 dirToMovePosition = (_currentWaypoint.transform.position - transform.position).normalized;
            float angleToDir = Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);
            
            if (Mathf.Abs(angleToDir) > maxSteering)
            {
                WheelBehaviour.SteeringAxis = angleToDir < 0 ? -1 : 1;
            }
            else
            {
                WheelBehaviour.SteeringAxis = angleToDir / maxSteering;
            }
        }

        private void VerticalAxisLogic()
        { 
            WheelBehaviour.VerticalAxis = 0.4f;
        }
    }
}
