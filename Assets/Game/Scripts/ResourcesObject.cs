namespace Game.TitlesHonors
{
    public class ResourcesObject<T> where T : UnityEngine.Object
    {
        private string _path;
        private T _value;

        public ResourcesObject(string path)
        {
            _path = path;
        }

        public T Value
        {
            get
            {
                if (_value == null)
                {
                    _value = UnityEngine.Resources.Load<T>(_path);
                    _path = null;
                }

                return _value;
            }
        }
    }
}