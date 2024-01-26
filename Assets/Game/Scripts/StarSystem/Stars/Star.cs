using Game.Scripts.StarSystem.Common;

namespace Game.Scripts.StarSystem.Stars
{
    public class Star : SpaceBody
    {
        public Star()
        {
            Size = 5;
            MotionData = new StarMotionData();
            Parent = this;
            View = PlanetBuilder.Create(Size, PlanetBuilder.SpaceBodyType.Star);
            Init();
        }
    }
}