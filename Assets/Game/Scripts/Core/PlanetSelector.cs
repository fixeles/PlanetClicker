using System;
using Game.Scripts.Space.Common;

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
            if (_iterator > SpaceBody.TotalBodiesCount - 1)
                _iterator = 0;

            SelectedBody = SpaceBody.GetBody(_iterator);
        }

        public static void SelectPrev()
        {
            _iterator--;
            if (_iterator < 0)
                _iterator = SpaceBody.TotalBodiesCount - 1;


            SelectedBody = SpaceBody.GetBody(_iterator);
        }
    }
}