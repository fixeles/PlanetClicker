using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.Serialization.DTO
{
    public class PlanetMotionDTO : SpaceBodyMotionDTO
    {
        [JsonConverter(typeof(Vector2Serializer))]
        public readonly Vector2 OrbitTilt;
        public readonly float Periapsis;
        public readonly float Apoapsis;
        public readonly float OrbitPerSecond;
        public readonly float OrbitState;

        public PlanetMotionDTO(
            Vector2 orbitTilt,
            float axisState,
            float axisTilt,
            float axisPerSecond,
            float periapsis,
            float apoapsis,
            float orbitPerSecond,
            float orbitState) : base(axisState, axisTilt, axisPerSecond)
        {
            OrbitTilt = orbitTilt;
            Periapsis = periapsis;
            Apoapsis = apoapsis;
            OrbitPerSecond = orbitPerSecond;
            OrbitState = orbitState;

        }
    }
}