using System;
using Game.Scripts.Money.Upgrades;
using Game.Scripts.StarSystem.Common;
using SolarSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.StarSystem.Planets
{
    [Serializable]
    public class PlanetMotionData : IMotionData
    {
        private readonly Upgrade _axisUpgrade;
        private readonly Upgrade _orbitUpgrade;
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

        public PlanetMotionData(float orbitRadius, int depth, Upgrade axisUpgrade, Upgrade orbitUpgrade)
        {
            _axisUpgrade = axisUpgrade;
            _orbitUpgrade = orbitUpgrade;
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

            orbitPerSecond = 0.02f / orbitRadius * depth;
            orbitState = Random.Range(-0.5f, 0.5f);
            axisPerSecond = 0.03f * depth;
            axisState = Random.Range(-0.5f, 0.5f);

            SetRandomValues();
        }

        public float OrbitPerSecond => orbitPerSecond + StaticData.Upgrade.AdditionalOrbitSpeed(_orbitUpgrade.Level);
        public float AxisPerSecond => axisPerSecond + StaticData.Upgrade.AdditionalAxisSpeed(_axisUpgrade.Level);

        public void Update(Vector3 parentPosition)
        {
            orbitState += Time.deltaTime * AxisPerSecond;
            if (orbitState is > 1 or < -1)
            {
                orbitState %= 1;
                OrbitTurnEvent?.Invoke();
            }

            axisState += Time.deltaTime * OrbitPerSecond;
            if (axisState is > 1 or < -1)
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