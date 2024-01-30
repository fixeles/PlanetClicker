using System;
using Game.Scripts.Money.Upgrades;
using Game.Scripts.StarSystem.Common;
using SolarSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.StarSystem.Planets
{
    public class PlanetMotionData : SpaceBodyMotionData
    {
        private readonly Upgrade _orbitUpgrade;
        public event Action OrbitTurnEvent;
        private readonly float _periapsis;
        private readonly float _apoapsis;
        private readonly float _orbitPerSecond;
        private Vector2 _orbitTilt;
        private float _orbitState;

        public PlanetMotionData(float orbitRadius, int depth, Upgrade axisUpgrade, Upgrade orbitUpgrade) : base(axisUpgrade)
        {
            _orbitUpgrade = orbitUpgrade;
            var ellipseDistortion = Random.Range(0.5f, 1f);
            var isLeftDeform = Random.Range(0, 2);
            if (isLeftDeform == 1)
            {
                _periapsis = orbitRadius;
                _apoapsis = orbitRadius * ellipseDistortion;
            }
            else
            {
                _periapsis = orbitRadius * ellipseDistortion;
                _apoapsis = orbitRadius;
            }

            _orbitPerSecond = 0.02f / orbitRadius * depth;
            _orbitState = Random.Range(-0.5f, 0.5f);
            AxisPerSecond = 0.03f * depth;
            AxisState = Random.Range(-0.5f, 0.5f);

            SetRandomValues();
        }

        public float OrbitPerSecond => _orbitPerSecond + StaticData.Upgrade.AdditionalOrbitSpeed(_orbitUpgrade.Level);
        public float NextUpgradeOrbitPerSecond => _orbitPerSecond + StaticData.Upgrade.AdditionalOrbitSpeed(_orbitUpgrade.Level + 1);

        public override void Update(Vector3 parentPosition)
        {
            base.Update(parentPosition);
            _orbitState += Time.deltaTime * AxisPerSecond;
            if (_orbitState is > 1 or < -1)
            {
                _orbitState %= 1;
                OrbitTurnEvent?.Invoke();
            }

            var xAxis = new Vector3(Mathf.Cos(_orbitTilt.x * Mathf.Deg2Rad), Mathf.Sin(_orbitTilt.x * Mathf.Deg2Rad), 0);
            var yAxis = new Vector3(0, 1, _orbitTilt.y).normalized;

            var orbitPos = Orbit.CalculatePointOnOrbit(_periapsis, _apoapsis, _orbitState);
            Position = xAxis * orbitPos.x + yAxis * orbitPos.y + parentPosition;
            Rotation = Quaternion.Euler(0, 0, -AxisTilt) * Quaternion.Euler(0, -AxisState * 360, 0);
        }

        private void SetRandomValues()
        {
            AxisTilt = Random.Range(-60, 60);
            const float orbitMaxAbsAngle = 30;
            _orbitTilt = new Vector2(
                Random.Range(0, orbitMaxAbsAngle),
                Random.Range(1, orbitMaxAbsAngle));
        }
    }
}