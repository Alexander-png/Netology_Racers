using Cars_5_5.BotAssistance;
using Cars_5_5.Input.Base;
using UnityEngine;

namespace Cars_5_5.Input
{
    public class BotInputHandler : BaseCarInput
    {
        private const float MaxTurnAxis = 1f;

        [SerializeField]
        private WaypointComponent _currentWaypoint;

        [SerializeField, Space(15)]
        private float _uTurnAngle = 120;
        [SerializeField]
        private float _maxThrottleDistance = 60f;

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
            float angleToDir = CalculateAngleForTurn();

            if (Mathf.Abs(angleToDir) > maxSteering)
            {
                WheelBehaviour.SteeringAxis = angleToDir < 0 ? -MaxTurnAxis : MaxTurnAxis;
            }
            else
            {
                WheelBehaviour.SteeringAxis = angleToDir / maxSteering;
            }
        }

        private float CalculateAngleForTurn()
        {
            Vector3 dirToMovePosition = (_currentWaypoint.transform.position - transform.position).normalized;
            return Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);
        }

        private void VerticalAxisLogic()
        {
            float angleToDir = CalculateAngleForTurn();
            if (Mathf.Abs(angleToDir) > _uTurnAngle)
            {
                WheelBehaviour.VerticalAxis = -1f;
            }
            else
            {
                float distanceToTargetPoint = Vector3.Distance(transform.position, _currentWaypoint.transform.position);
                float currentSpeed = CarObserver.SignedCarSpeed;
                if (CanSetFullThrottle(distanceToTargetPoint))
                {
                    WheelBehaviour.VerticalAxis = 1f;
                }
                else
                {
                    
                }
            }
            //WheelBehaviour.VerticalAxis = 0.4f;
        }

        private bool CanSetFullThrottle(float distanceToTargetPoint)
        {
            float currentSpeed = CarObserver.SignedCarSpeed;
            return distanceToTargetPoint > _maxThrottleDistance;
        }
    }
}
