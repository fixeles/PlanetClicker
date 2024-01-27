using System.Collections.Generic;
using Game.Scripts.StarSystem.Planets;

namespace Game.Scripts.StarSystem.Common
{
    public abstract class SpaceBody
    {
        private static readonly List<SpaceBody> AllBodies = new();
        public SpaceBodyView View { get; protected set; }

        protected IMotionData MotionData;
        protected SpaceBody Parent;

        internal protected readonly List<Planet> Satellites = new();
        internal protected int Depth;
        internal protected float Size;


        public static int TotalBodiesCount => AllBodies.Count;

        public static SpaceBody GetBody(int index) => AllBodies[index];

        public float SatellitesCount => Satellites.Count;

        public bool CanCreateSatellite => Size > StaticData.SatelliteSizeStep * (SatellitesCount + 2);

        public void CreateSatellite()
        {
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
            AllBodies.Add(this);
        }

        private void Select()
        {
            PlanetSelector.SelectedBody = this;
        }
    }
}