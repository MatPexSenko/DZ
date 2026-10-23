using System;
using System.Collections;
using Modules.Money;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public sealed class MoneyPresentation : IInitializable, IDisposable
    {
        public event Action<string> OnMoneyEarned;
        
        public ReadOnlyReactiveProperty<string> Money => _money;
        
        private readonly ReactiveProperty<string> _money;
        
        private readonly IMoneyStorage _moneyStorage;

        public MoneyPresentation(IMoneyStorage moneyStorage)
        {
            _money = new ReactiveProperty<string>(this.Formate(moneyStorage.Money));
            _moneyStorage = moneyStorage;
        }
        private void MoneysChanged(int value, int _)
        {
            _money.Value = this.Formate(value);
            Debug.Log($"Money Changed {value}");
        }
        
        private void MoneysErned(int value, int _)
        {
            OnMoneyEarned.Invoke(value.ToString());
        }
        private string Formate(int value) => $"{value}";
        public void Initialize()
        {
            _moneyStorage.OnMoneySpent += MoneysChanged;
            _moneyStorage.OnMoneyEarned += MoneysErned;
        }
        public void Dispose()
        { 
            _moneyStorage.OnMoneySpent -= MoneysChanged;
            _moneyStorage.OnMoneyEarned -= MoneysErned;
        }
    }
}