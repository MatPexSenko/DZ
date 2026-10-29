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
        public ReadOnlyReactiveProperty<string> Money => _money;
        
        private readonly ReactiveProperty<string> _money;
        
        private readonly IMoneyStorage _moneyStorage;

        public MoneyPresentation(IMoneyStorage moneyStorage)
        {
            _money = new ReactiveProperty<string>(this.Formate(moneyStorage.Money));
            _moneyStorage = moneyStorage;
        }
        private void MoneyChanged(int value, int _)
        {
            _money.Value = this.Formate(value);
        }
        
        private string Formate(int value) => $"{value}";
        public void Initialize()
        {
            _moneyStorage.OnMoneyChanged += MoneyChanged;
        }
        public void Dispose()
        { 
            _moneyStorage.OnMoneyChanged -= MoneyChanged;
        }
    }
}