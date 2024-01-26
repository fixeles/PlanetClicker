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
        [SerializeField, Range(-180, 180)] private float tilt;
        [SerializeField, Range(-180, 180)] private float orbitAngle;
        [SerializeField, Min(0.01f)] private float periapsis = 0.01f;
        [SerializeField, Min(0)] private float apoapsis;
        [SerializeField, Min(0)] private float dstMultiplier;

        [SerializeField] private float orbitState;
        [SerializeField] private float orbitPerSecond;
        [SerializeField] private float selfRotationState;
        [SerializeField] private float selfRotationPerSecond;

        public PlanetMotionData(float size)
        {
            dstMultiplier = 2 / size;
            SetRandomValues();
        }

        [field: SerializeField] public Vector3 Position { get; private set; }
        [field: SerializeField] public Quaternion Rotation { get; private set; }

        public void Update(Vector3 parentPosition)
        {
            orbitState += Time.deltaTime * orbitPerSecond;
            if (orbitState is > 1 or < 1)
            {
                orbitState %= 1;
                //add points
            }
            selfRotationState += Time.deltaTime * selfRotationPerSecond;
            if (selfRotationState is > 1 or < 1)
            {
                selfRotationState %= 1;
                //add points
            }

            var xAxis = new Vector3(Mathf.Cos(orbitAngle * Mathf.Deg2Rad), Mathf.Sin(orbitAngle * Mathf.Deg2Rad), 0);
            var yAxis = Vector3.forward;

            var orbitPos = Orbit.CalculatePointOnOrbit(periapsis, apoapsis, orbitState);
            Position = (xAxis * orbitPos.x + yAxis * orbitPos.y) * dstMultiplier + parentPosition;
            Rotation = Quaternion.Euler(0, 0, -tilt) * Quaternion.Euler(0, -selfRotationState * 360, 0);
        }

        private void SetRandomValues()
        {
            tilt = Random.Range(-180, 180);
            orbitAngle = Random.Range(-180, 180);
            periapsis = Random.Range(0, 100);
            apoapsis = Random.Range(0, 100);

            const float maxAbsSpeed = 0.1f;
            orbitPerSecond = Random.Range(-maxAbsSpeed, maxAbsSpeed);
            orbitState = Random.Range(0f, 1f);
            selfRotationPerSecond = Random.Range(-maxAbsSpeed, maxAbsSpeed);
            selfRotationState = Random.Range(0f, 1f);
        }
    }
}