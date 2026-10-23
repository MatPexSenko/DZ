using System;
using JetBrains.Annotations;
using Modules.Money;
using Modules.Planets;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public sealed class IncomePresentation :IDisposable
    {
        public ReadOnlyReactiveProperty<float> Persent => _currentPersent;
        public ReadOnlyReactiveProperty<string> CurrentTime => _currentTime;
        public ReadOnlyReactiveProperty<bool> isUnlock => _isUnlock;
        public ReadOnlyReactiveProperty<bool> IsIncomeReady => _isIncomeReady;
        
        private readonly ReactiveProperty<float> _currentPersent = new(0f);
        private readonly ReactiveProperty<string> _currentTime = new("");
        private readonly ReactiveProperty<bool> _isUnlock = new(false);
        private readonly ReactiveProperty<bool> _isIncomeReady = new(false);
        
        [CanBeNull] 
        private IPlanet _planet;

        public IncomePresentation(IPlanet planet)
        {
            this._planet = planet;
            
            _planet.OnUnlocked += Unlock;
            _planet.OnIncomeTimeChanged += ChangeCurrentPercent;
            _planet.OnIncomeReady += IncomeReady;
        }

        public void Unlock()
        {
            _isUnlock.Value = true;
        }
        
        private string FormatTime(float seconds)
        {
            float minutes = seconds / 60;
            float secs = seconds % 60;
            secs = (int)secs;
            
            string strSec = secs>10 ? secs.ToString() : $"0{secs}";

            return $"{(int)minutes}: " + $"{strSec}";
        }

        private void IncomeReady(bool obj)
        {
            _isIncomeReady.Value = obj;
        }
        
        public void Dispose()
        {
            _planet.OnUnlocked -= Unlock;
            _planet.OnIncomeTimeChanged += ChangeCurrentPercent;
            _planet.OnIncomeReady -= IncomeReady;
        }

        private void ChangeCurrentPercent(float time)
        {
            _currentPersent.Value = _planet.IncomeProgress;
            _currentTime.Value = FormatTime(time);
            
        }
        
        public class Factory : PlaceholderFactory<IPlanet, IncomePresentation> { }
    }
}