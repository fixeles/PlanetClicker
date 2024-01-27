using System;
using Game.Scripts.StarSystem.Common;

namespace Game.Scripts
{
    public class PlanetSelector
    {
        public static event Action SelectedObjectChangedEvent;
        private static SpaceBody _selectedBody;
        private static int _iterator;

        public static SpaceBody SelectedBody
        {
            get => _selectedBody;
            set
            {
                _selectedBody = value;
                SelectedObjectChangedEvent?.Invoke();
            }
        }

        public static void SelectNext()
        {
            _iterator++;
            if (_iterator > SpaceBody.AllBodies.Count - 1)
                _iterator = 0;

            SelectedBody = SpaceBody.AllBodies[_iterator];
        }

        public static void SelectPrev()
        {
            _iterator--;
            if (_iterator < 0)
                _iterator = SpaceBody.AllBodies.Count - 1;

            SelectedBody = SpaceBody.AllBodies[_iterator];
        }
    }
}