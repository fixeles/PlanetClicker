using System;
using Game.Scripts.StarSystem.Common;
using Game.Scripts.StarSystem.Stars;

namespace Game.Scripts.StarSystem.Planets
{
    public class Planet : SpaceBody
    {
        private readonly float _orbitRadius;

        public Planet(SpaceBody parent)
        {
            float initialRadiusMultiplier;
            switch (parent)
            {
                case Star:
                    initialRadiusMultiplier = 6;
                    Size = parent.Size / 2 - SatelliteSizeStep / 2 * (parent.SatellitesCount + 1);
                    break;
                case Planet:
                    initialRadiusMultiplier = 6;
                    Size = parent.Size - SatelliteSizeStep * (parent.SatellitesCount + 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (parent.SatellitesCount > 0)
            {
                var lastSatellite = parent.Satellites[^1];
                _orbitRadius = lastSatellite._orbitRadius + lastSatellite.Size;
            }
            else
                _orbitRadius = initialRadiusMultiplier * parent.Size;


            MotionData = new PlanetMotionData(_orbitRadius);

            Depth = parent.Depth + 1;
            Parent = parent;
            View = PlanetViewBuilder.Create(Size);
            Init();
        }
    }
}