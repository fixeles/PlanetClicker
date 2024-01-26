using System.Collections.Generic;
using Game.Scripts.StarSystem.Planets;
using Game.TitlesHonors;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.StarSystem.Common
{
    public abstract class SpaceBody
    {
        private static readonly ResourcesObject<SpaceBodyView> Prefab = new("PlanetPrefab");

        protected readonly HashSet<SpaceBody> Satellites = new();
        internal protected SpaceBody Parent;
        internal protected int Depth;
        internal protected float Size;
        protected IMotionData MotionData;

        private SpaceBodyView _view;
        
        public float SatellitesCount => Satellites.Count;


        public void Update()
        {
            MotionData.Update(Parent.MotionData.Position);
            foreach (var satellite in Satellites)
            {
                satellite.Update();
            }
            _view.UpdateTransform(MotionData);
        }
        
        
        protected void Init()
        {
            _view = Object.Instantiate(Prefab.Value);
            _view.transform.localScale = new Vector3(Size, Size, Size);
            _view.Init(MotionData);
            _view.SpawnSatelliteRequest += CreateSatellite;
//size, eg...
        }

        private void CreateSatellite()
        {
            Satellites.Add(new Planet(this));
        }
    }
}