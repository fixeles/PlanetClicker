using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Core;
using Game.Scripts.Money.Upgrades;
using Game.Scripts.Serialization.DTO;
using Game.Scripts.Space.Planets;

namespace Game.Scripts.Space.Common
{
    public abstract class SpaceBody
    {
        public static double TotalIncomePerSecond => AllBodies.Sum(spaceBody => spaceBody.IncomePerSecond);

        private static readonly List<SpaceBody> AllBodies = new();
        public abstract double IncomePerSecond { get; }
        public abstract double NextUpgradeIncomePerSecond { get; }
        public SpaceBodyUpgradeData UpgradeData { get; protected set; }
        public SpaceBodyView View { get; protected set; }

        public SpaceBodyMotionData MotionData { get; protected set; }
        protected SpaceBody Parent;

        internal protected readonly List<Planet> Satellites = new();
        internal protected int Depth;
        internal protected float Size;


        public static int TotalBodiesCount => AllBodies.Count;

        public static SpaceBody GetBody(int index) => AllBodies[index];

        public int SatellitesCount => Satellites.Count;

        public bool CanCreateSatellite => Size > StaticData.SatelliteSizeStep * (SatellitesCount + 2);

        public void CreateSatellite(PlanetDTO dto = null)
        {
            var planet = new Planet(this, dto);
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
            View.SelectEvent += Select;
            AllBodies.Add(this);
            PlanetSelector.SelectedBody = this;
        }

        private void Select()
        {
            PlanetSelector.SelectedBody = this;
        }
    }

}