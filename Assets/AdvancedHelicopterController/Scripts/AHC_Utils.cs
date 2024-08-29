using UnityEngine;

namespace alelavoie
{
    class AHC_Utils
    {
        /// <summary>
        /// Makes sure the target vector does not form, with the normal vector, an angle smaller or bigger than minAngle or maxAngle. If the 
        /// angle is smaller, the target vector will be rotated away from the normal in order to match the minAngle. If it's bigger, the target
        /// vector will be rotate towards the normal in order to match the maxAngle. 
        /// </summary>
        public static Vector3 ClampOrientation(Vector3 normal, Vector3 target, float minAngle, float maxAngle)
        {
            if (minAngle > maxAngle)
            {
                return target.normalized;
            }

            float actualAngle = Vector3.Angle(normal, target);
            
            if (actualAngle <= maxAngle && actualAngle >= minAngle)
            {               
                return target.normalized;
            }
            
            if (actualAngle < minAngle)
            {
                float angleDiff = minAngle - actualAngle;
                return Vector3.RotateTowards(target, -normal, angleDiff * Mathf.Deg2Rad, 0.0f).normalized;
            }
            else //Actual Angle > max angle
            {
                float angleDiff = actualAngle - maxAngle;
                return Vector3.RotateTowards(target, normal, angleDiff * Mathf.Deg2Rad, 0.0f).normalized;
            }
        }

        public static Vector3 DampenOrientation(Vector3 normal, Vector3 currentDirection, float maxAngle)
        {
            float angle = Vector3.Angle(normal, currentDirection);
            if (angle > maxAngle)
            {
                return currentDirection.normalized;
            }
            float targetAngle = Mathf.Lerp(0f, maxAngle, Mathf.Pow(Mathf.InverseLerp(0f, maxAngle, angle), 2));
            float deltaAngle = angle - targetAngle;
            return Vector3.RotateTowards(currentDirection, normal, deltaAngle * Mathf.Deg2Rad, 0.0f).normalized;
        }
        public static Vector3 BoostOrientation(Vector3 normal, Vector3 currentDirection, float maxAngle)
        {
            float angle = Vector3.Angle(normal, currentDirection);
            if (angle > maxAngle)
            {
                return currentDirection.normalized;
            }
            float targetAngle = Mathf.Lerp(0f, maxAngle, Mathf.Sqrt(Mathf.InverseLerp(0f, maxAngle, angle)));
            float deltaAngle = angle - targetAngle;
            return Vector3.RotateTowards(currentDirection, normal, deltaAngle * Mathf.Deg2Rad, 0.0f).normalized;
        }
    }
}
