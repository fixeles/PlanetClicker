using Game.Scripts.Serialization.DTO;

namespace Game.Scripts.Serialization
{
    public class SaveData
    {
        public readonly double Money;
        public readonly StarDTO StarDTO;

        public SaveData(double money, StarDTO starDTO)
        {
            Money = money;
            StarDTO = starDTO;
        }
    }
}