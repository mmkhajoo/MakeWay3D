using UnityEngine;
 
public static class ProjectileMath
{
    public static bool LaunchAngle(float speed, float distance, float yOffset, float gravity, out float angle0, out float angle1)
    {
        angle0 = angle1 = 0;
 
        float speedSquared = speed * speed;
 
        float operandA = Mathf.Pow(speed, 4);
        float operandB = gravity * (gravity * (distance * distance) + (2 * yOffset * speedSquared));
 
        // Target is not in range
        if (operandB > operandA)
            return false;
 
        float root = Mathf.Sqrt(operandA - operandB);
 
        angle0 = Mathf.Atan((speedSquared + root) / (gravity * distance));
        angle1 = Mathf.Atan((speedSquared - root) / (gravity * distance));
 
        return true;
    }
 
    /// <summary>
    /// Calculates the initial launch speed required to hit a target at distance with elevation yOffset.
    /// </summary>
    /// <param name="distance">Planar distance from origin to the target</param>
    /// <param name="yOffset">Elevation of the origin with respect to the target</param>
    /// <param name="gravity">Downwards force of gravity in m/s^2</param>
    /// <param name="angle">Initial launch angle in radians</param>
    public static float LaunchSpeed(float distance, float yOffset, float gravity, float angle)
    {
        float speed = (distance * Mathf.Sqrt(gravity) * Mathf.Sqrt(1 / Mathf.Cos(angle))) / Mathf.Sqrt(2 * distance * Mathf.Sin(angle) + 2 * yOffset * Mathf.Cos(angle));
 
        return speed;
    }
 
    public static Vector2[] ProjectileArcPoints(int iterations, float speed, float distance, float gravity, float angle)
    {
        float iterationSize = distance / iterations;
 
        float radians = angle;
 
        Vector2[] points = new Vector2[iterations + 1];
 
        for (int i = 0; i <= iterations; i++)
        {
            float x = iterationSize * i;
            float t = x / (speed * Mathf.Cos(radians));
            float y = -0.5f * gravity * (t * t) + speed * Mathf.Sin(radians) * t;
 
            Vector2 p = new Vector2(x, y);
 
            points[i] = p;
        }
 
        return points;
    }
}