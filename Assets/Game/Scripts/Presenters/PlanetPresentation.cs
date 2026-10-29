using System;
using Modules.Planets;
using Modules.Utils;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPresentation : IDisposable
    {
        public string planetId => _planet.Name;
        public Observable<Unit> OnStartIncomeGather => _onStartIncomeGather;
        public Observable<string> Price => _price;
        public Observable<Sprite> Icon => _icon;
        public Observable<bool> IsUnlocked => _isUnlocked;
        
        public ReadOnlyReactiveProperty<float> Persent => _currentPersent;
        public ReadOnlyReactiveProperty<string> CurrentTime => _currentTime;
        public ReadOnlyReactiveProperty<bool> isUnlock => _isUnlock;
        public ReadOnlyReactiveProperty<bool> IsIncomeReady => _isIncomeReady;
        
        private readonly ReactiveProperty<Unit> _onStartIncomeGather = new ReactiveProperty<Unit>();
        private readonly ReactiveProperty<string> _price = new("");
        private readonly ReactiveProperty<Sprite> _icon = new(null);
        private readonly ReactiveProperty<bool> _isUnlocked = new(false);
        
        private readonly ReactiveProperty<float> _currentPersent = new(0f);
        private readonly ReactiveProperty<string> _currentTime = new("");
        private readonly ReactiveProperty<bool> _isUnlock = new(false);
        private readonly ReactiveProperty<bool> _isIncomeReady = new(false);
        
        [ShowInInspector]
        private IPlanet _planet;
        
        private PlanetPopupPresentation planetPopup;

        public PlanetPresentation(IPlanet planet, PlanetPopupPresentation planetPopup, MoneyPresentation moneyPresentation)
        {
            this._planet = planet;
            this.planetPopup = planetPopup;
            
            _planet.OnUnlocked += UnlockPlanet;
            _planet.OnIncomeReady += IncomeReady;
            
            _planet.OnUnlocked += Unlock;
            _planet.OnIncomeTimeChanged += ChangeCurrentPercent;
            _planet.OnIncomeReady += IncomeReady;
            
            UpdateState();
        }

        private void StartIncomeGather()
        {
            _onStartIncomeGather.OnNext(Unit.Default);
        }
        public void Dispose()
        {
            _planet.OnIncomeReady -= IncomeReady;
            _planet.OnUnlocked -= UnlockPlanet;
            
            _planet.OnUnlocked -= Unlock;
            _planet.OnIncomeTimeChanged -= ChangeCurrentPercent;
            _planet.OnIncomeReady -= IncomeReady;
        }

        void UpdateState()
        {
            _isUnlocked.Value = _planet.IsUnlocked;
            _price.Value = _planet.Price.ToString();
            _icon.Value = _planet.GetIcon(_planet.IsUnlocked);
        }

        private void UnlockPlanet()
        {
            _isUnlocked.Value = _planet.IsUnlocked;
            _price.Value = "";
            _icon.Value = _planet.GetIcon(_planet.IsUnlocked);
        }

        private void IncomeReady(bool obj)
        {
            _isIncomeReady.Value = obj;
        }
        public void OnClicked()
        {
            if (!_planet.IsUnlocked)
            {
                _planet.Unlock();
                return;
            }

            if (_planet.IsIncomeReady)
            {
                this.StartIncomeGather();
                return;
            }
        }

        public void GatherIncome()
        {
            _planet.GatherIncome();
        }

        public void OnLongClicked()
        {
            planetPopup.Show(new PlanetPopupPresentation.Args(this._planet));
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
            
            string strSec = secs>=10 ? secs.ToString() : $"0{secs}";

            return $"{(int)minutes}: " + $"{strSec}";
        }
        private void ChangeCurrentPercent(float time)
        {
            _currentPersent.Value = _planet.IncomeProgress;
            _currentTime.Value = FormatTime(time);
            
        }
    }
}