using System;

namespace Game.Scripts.Money
{
    public static class Wallet
    {
        public static event Action MoneyChangedEvent;
        public static float CurrentMoney { get; private set; }

        public static void AddMoney(float count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException();
            CurrentMoney += count;
            MoneyChangedEvent?.Invoke();
        }

        public static bool TrySubtractMoney(float count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException();
            if (CurrentMoney - count < 0)
                return false;

            CurrentMoney -= count;
            MoneyChangedEvent?.Invoke();
            return true;
        }
    }
}