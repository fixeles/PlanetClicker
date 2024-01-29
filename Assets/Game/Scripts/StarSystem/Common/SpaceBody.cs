using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Money.Upgrades;
using Game.Scripts.StarSystem.Planets;

namespace Game.Scripts.StarSystem.Common
{
    public abstract class SpaceBody
    {
        public static event Action NewBodyCreatedEvent; 
        public static double TotalIncomePerSecond => AllBodies.Sum(spaceBody => spaceBody.IncomePerSecond);

        private static readonly List<SpaceBody> AllBodies = new();
        public abstract double IncomePerSecond { get; }
        public SpaceBodyUpgradeData UpgradeData { get; protected set; }
        public SpaceBodyView View { get; protected set; }

        protected IMotionData MotionData;
        protected SpaceBody Parent;

        internal protected readonly List<Planet> Satellites = new();
        internal protected int Depth;
        internal protected float Size;


        public static int TotalBodiesCount => AllBodies.Count;

        public static SpaceBody GetBody(int index) => AllBodies[index];

        public int SatellitesCount => Satellites.Count;

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
            NewBodyCreatedEvent?.Invoke();
        }

        private void Select()
        {
            PlanetSelector.SelectedBody = this;
        }
    }
}