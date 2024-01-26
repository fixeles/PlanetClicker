using UnityEngine;
using static UnityEngine.Mathf;

namespace SolarSystem
{
    public class Orbit
    {

        // Calculate point on orbit at t (going from 0 at the start of the orbit, to 1 at the end of the orbit)
        // The orbit is defined by the periapsis (distance of closest approach) and apoapsis (farthest distance)
        public static Vector2 CalculatePointOnOrbit(float periapsis, float apoapsis, float t)
        {
            // Calculate some parameters of the ellipse
            // (see en.wikipedia.org/wiki/Ellipse#Parameters)
            float semiMajorLength = (apoapsis + periapsis) / 2;
            float linearEccentricity = semiMajorLength - periapsis; // distance between centre and focus
            float eccentricity = linearEccentricity / semiMajorLength; // (0 = perfect circle, and up to 1 is increasingly elliptical) 
            float semiMinorLength = Sqrt(Pow(semiMajorLength, 2) - Pow(linearEccentricity, 2));

            // Angle to where body would be if it had a circular orbit
            float meanAnomaly = t * PI * 2;
            // Solve for eccentric anomaly (angle to where body actually is in its elliptical orbit)
            float eccentricAnomaly = SolveKepler(meanAnomaly, eccentricity);

            // Calculate point in orbit from angle
            float ellipseCentreX = -linearEccentricity;
            float pointX = Cos(eccentricAnomaly) * semiMajorLength + ellipseCentreX;
            float pointY = Sin(eccentricAnomaly) * semiMinorLength;

            return new Vector2(pointX, pointY);
        }


        // Newton-Rhapson method
        static float SolveKepler(float meanAnomaly, float eccentricity, int maxIterations = 100)
        {
            const float h = 0.0001f; // step size for approximating gradient of the function
            const float acceptableError = 0.00000001f;
            float guess = meanAnomaly;

            for (int i = 0; i < maxIterations; i++)
            {
                float y = KeplerEquation(guess, meanAnomaly, eccentricity);
                // Exit early if output of function is very close to zero
                if (Abs(y) < acceptableError)
                {
                    break;
                }
                // Update guess to value of x where the slope of the function intersects the x-axis
                float slope = (KeplerEquation(guess + h, meanAnomaly, eccentricity) - y) / h;
                float step = y / slope;
                guess -= step;
            }
            return guess;

            // Kepler's equation: M = E - e * sin(E)
            // M is the Mean Anomaly (angle to where body would be if its orbit was actually circular)
            // E is the Eccentric Anomaly (angle to where the body is on the ellipse)
            // e is the eccentricity of the orbit (0 = perfect circle, and up to 1 is increasingly elliptical) 
            float KeplerEquation(float E, float M, float e)
            {
                // Here the equation has been rearranged. We're trying to find the value for E where this will return 0.
                return M - E + e * Sin(E);
            }
        }
    }
}