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
            View = PlanetViewBuilder.Create(Size, PlanetViewBuilder.SpaceBodyType.Star);

            PlanetSelector.SelectedBody = this;
            Init();
        }
    }
}