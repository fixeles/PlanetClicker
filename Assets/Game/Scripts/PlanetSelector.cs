using System;
using Game.Scripts.StarSystem.Common;

namespace Game.Scripts
{
    public class PlanetSelector
    {
        public static event Action SelectedObjectChangedEvent;
        private static SpaceBody _selectedBody;
        
        public static SpaceBody SelectedBody
        {
            get => _selectedBody;
            set
            {
                _selectedBody = value;
                SelectedObjectChangedEvent?.Invoke();
            }
        }
    }
}