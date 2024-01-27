using System.Collections.Generic;
using Game.Scripts.StarSystem.Planets;

namespace Game.Scripts.StarSystem.Common
{
    public abstract class SpaceBody
    {
        public SpaceBodyView View { get; protected set; }
        protected const float SatelliteSizeStep = 0.4f;

        protected IMotionData MotionData;
        protected SpaceBody Parent;

        internal protected readonly List<Planet> Satellites = new();
        internal protected int Depth;
        internal protected float Size;


        public float SatellitesCount => Satellites.Count;

        public void CreateSatellite()
        {
            if (Size < SatelliteSizeStep * (SatellitesCount + 1))
                return;

            var planet = new Planet(this);
            Satellites.Add(planet);
        }

        public void Update()
        {
            MotionData.Update(Parent.MotionData.Position);
            foreach (var satellite in Satellites)
            {
                satellite.Update();
            }
            View.UpdateTransform(MotionData);
        }

        protected void Init()
        {
            View.Init(MotionData);
            View.SelectEvent += Select;
//size, eg...
        }

        private void Select()
        {
            PlanetSelector.SelectedBody = this;
        }
    }
}