using System;

namespace Game.Scripts.Money
{
    public static class Wallet
    {
        public static event Action MoneyChangedEvent;
        public static double CurrentMoney { get; private set; }

        public static bool HasMoney(double value) => CurrentMoney >= value;

        public static void AddMoney(double count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException();
            CurrentMoney += count;
            MoneyChangedEvent?.Invoke();
        }

        public static bool TrySubtractMoney(double count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException();
            if (CurrentMoney - count < 0)
                return false;

            CurrentMoney -= count;
            MoneyChangedEvent?.Invoke();
            return true;
        }

        public static bool TrySubtractMoneyWithCallback(double count, Action callback)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException();
            if (CurrentMoney - count < 0)
                return false;

            CurrentMoney -= count;
            callback.Invoke();
            MoneyChangedEvent?.Invoke();
            return true;
        }
    }
}