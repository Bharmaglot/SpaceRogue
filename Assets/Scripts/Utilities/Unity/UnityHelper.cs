using UnityEngine;
using Utilities.Mathematics;

namespace Utilities.Unity
{
    public static class UnityHelper
    {
        public const byte MaxCountSpawnTries = 10;

        public static bool IsAnyObjectAtPosition(Vector3 position, float radius)
        {
            var collider = Physics2D.OverlapCircle(position, radius);
            return collider is not null;
        }

        public static bool Approximately(float a, float b, float precision) => Mathf.Abs(b - a) <= precision;

        public static bool Approximately(Vector3 original, Vector3 other, float precision) => 
            Approximately(original.x, other.x, precision) && 
            Approximately(original.y, other.y, precision);

        public static bool VectorAngleLessThanAngle(Vector3 targetDirection, Vector3 currentDirection, float angle)
        {
            return Vector2.SignedAngle(targetDirection, currentDirection) <= angle;
        }
        
        public static bool DirectionInsideAngle(Vector3 targetDirection, Vector3 currentDirection, float angle)
        {
            return Mathf.Abs(Vector2.SignedAngle(targetDirection, currentDirection)) <= angle / 2;
        }

        public static bool IsObjectVisible(this Camera camera, Bounds bounds)
        {
            return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), bounds);
        }

        public static Vector3 GetEmptySpawnPoint(Vector3 spawnPoint, Vector3 unitSize, int spawnCircleRadius)
        {
            var unitSpawnPoint = spawnPoint + (Vector3)(Random.insideUnitCircle * spawnCircleRadius);
            var unitMaxSize = unitSize.MaxVector3CoordinateOnPlane();

            var tryCount = 0;
            while (IsAnyObjectAtPosition(unitSpawnPoint, unitMaxSize) && tryCount <= MaxCountSpawnTries)
            {
                unitSpawnPoint = spawnPoint + (Vector3)(Random.insideUnitCircle * spawnCircleRadius);
                tryCount++;
            }

            return unitSpawnPoint;
        }

        public static bool InDetectionRadius(Vector3 ownPosition, Vector3 targetPosition, float detectionRadius)
        {
            var sqrMagnitude = (ownPosition - targetPosition).sqrMagnitude;
            var sqrDetectionRadius = detectionRadius * detectionRadius;

            return sqrMagnitude <= sqrDetectionRadius;
        }
    }
}