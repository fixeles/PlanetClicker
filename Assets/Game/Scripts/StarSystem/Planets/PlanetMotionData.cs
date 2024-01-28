using System;
using Game.Scripts.StarSystem.Common;
using SolarSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.StarSystem.Planets
{
    [Serializable]
    public class PlanetMotionData : IMotionData
    {
        public event Action AxisTurnEvent;
        public event Action OrbitTurnEvent;
        [SerializeField, Range(-90, 90)] private float tilt;
        [SerializeField, Min(0.01f)] private float periapsis = 0.01f;
        [SerializeField, Min(0)] private float apoapsis;
        [SerializeField] private Vector2 orbitTilt;

        [SerializeField] private float orbitState;
        [SerializeField] private float orbitPerSecond;
        [SerializeField] private float axisState;
        [SerializeField] private float axisPerSecond;

        [field: SerializeField] public Vector3 Position { get; private set; }
        [field: SerializeField] public Quaternion Rotation { get; private set; }

        public PlanetMotionData(float orbitRadius, int depth)
        {
            var ellipseDistortion = Random.Range(0.5f, 1f);
            var isLeftDeform = Random.Range(0, 2);
            if (isLeftDeform == 1)
            {
                periapsis = orbitRadius;
                apoapsis = orbitRadius * ellipseDistortion;
            }
            else
            {
                periapsis = orbitRadius * ellipseDistortion;
                apoapsis = orbitRadius;
            }

            orbitPerSecond = Random.Range(0.2f, 0.4f) / orbitRadius * depth;
            orbitState = Random.Range(0, 0.5f);
            axisPerSecond = Random.Range(0.5f, 0.15f);
            axisState = Random.Range(0, 0.5f);

            SetRandomValues();
        }

        public float OrbitPerSecond => orbitPerSecond;
        public float AxisPerSecond => axisPerSecond;

        public void Update(Vector3 parentPosition)
        {
            orbitState += Time.deltaTime * orbitPerSecond;
            if (orbitState is > 1 or < 0)
            {
                orbitState %= 1;
                OrbitTurnEvent?.Invoke();
            }
            axisState += Time.deltaTime * axisPerSecond;
            if (axisState is > 1 or < 0)
            {
                axisState %= 1;
                AxisTurnEvent?.Invoke();
            }

            var xAxis = new Vector3(Mathf.Cos(orbitTilt.x * Mathf.Deg2Rad), Mathf.Sin(orbitTilt.x * Mathf.Deg2Rad), 0);
            var yAxis = new Vector3(0, 1, orbitTilt.y).normalized;

            var orbitPos = Orbit.CalculatePointOnOrbit(periapsis, apoapsis, orbitState);
            Position = xAxis * orbitPos.x + yAxis * orbitPos.y + parentPosition;
            Rotation = Quaternion.Euler(0, 0, -tilt) * Quaternion.Euler(0, -axisState * 360, 0);
        }

        private void SetRandomValues()
        {
            tilt = Random.Range(-60, 60);
            const float orbitMaxAbsAngle = 30;
            orbitTilt = new Vector2(
                Random.Range(0, orbitMaxAbsAngle),
                Random.Range(1, orbitMaxAbsAngle));
        }
    }
}