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
        [SerializeField, Range(-90, 90)] private float tilt;
        [SerializeField, Min(0.01f)] private float periapsis = 0.01f;
        [SerializeField, Min(0)] private float apoapsis;
        [SerializeField] private Vector2 orbitTilt;

        [SerializeField] private float orbitLoopState;
        [SerializeField] private float orbitPerSecond;
        [SerializeField] private float selfRotationState;
        [SerializeField] private float selfRotationPerSecond;

        public PlanetMotionData(float size, float satellitesCount)
        {
            const float step = 5;
            var satellitesOffset = size * step;
            var distance = satellitesOffset * (satellitesCount + 1) + step * size;
            var ellipseDistortion = Random.Range(0.4f, 1f);
            var isLeftDeform = Random.Range(0, 2);
            if (isLeftDeform == 1)
            {
                periapsis = distance;
                apoapsis = distance * ellipseDistortion;
            }
            else
            {
                periapsis = distance * ellipseDistortion;
                apoapsis = distance;
            }
            SetRandomValues();
        }

        [field: SerializeField] public Vector3 Position { get; private set; }
        [field: SerializeField] public Quaternion Rotation { get; private set; }

        public void Update(Vector3 parentPosition)
        {
            orbitLoopState += Time.deltaTime * orbitPerSecond;
            if (orbitLoopState is > 1 or < 1)
            {
                orbitLoopState %= 1;
                //add points
            }
            selfRotationState += Time.deltaTime * selfRotationPerSecond;
            if (selfRotationState is > 1 or < 1)
            {
                selfRotationState %= 1;
                //add points
            }

            var xAxis = new Vector3(Mathf.Cos(orbitTilt.x * Mathf.Deg2Rad), Mathf.Sin(orbitTilt.x * Mathf.Deg2Rad), 0);
            var yAxis = new Vector3(0, 1, orbitTilt.y).normalized;

            var orbitPos = Orbit.CalculatePointOnOrbit(periapsis, apoapsis, orbitLoopState);
            Position = xAxis * orbitPos.x + yAxis * orbitPos.y + parentPosition;
            Rotation = Quaternion.Euler(0, 0, -tilt) * Quaternion.Euler(0, -selfRotationState * 360, 0);
        }

        private void SetRandomValues()
        {
            tilt = Random.Range(-60, 60);
            const float orbitMaxAbsAngle = 30;
            orbitTilt = new Vector2(
                Random.Range(0, orbitMaxAbsAngle),
                Random.Range(0, orbitMaxAbsAngle));

            var availableSpeed = new Vector2(0.05f, 0.15f);
            orbitPerSecond = Random.Range(availableSpeed.x, availableSpeed.y);
            orbitLoopState = Random.Range(0f, 0.5f);
            selfRotationPerSecond = Random.Range(availableSpeed.x, availableSpeed.y);
            selfRotationState = Random.Range(0f, 0.5f);
        }
    }
}