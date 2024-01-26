using Game.Scripts.StarSystem.Common;
using Game.Scripts.StarSystem.Stars;

namespace Game.Scripts.StarSystem.Planets
{
    public class Planet : SpaceBody
    {
        public Planet(SpaceBody parent)
        {
            Depth = parent.Depth + 1;

            if (parent is Star)
            {
                Size = parent.Size / 2 - 0.2f * (Satellites.Count + 1);
            }
            else
                Size = parent.Size - 0.2f * (Satellites.Count + 1);

            MotionData = new PlanetMotionData(Size, parent.SatellitesCount);
            Parent = parent;

            View = PlanetViewBuilder.Create(Size);
            Init();
        }
    }
}