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
        public event Action OnIncomeGather;
        public Observable<string> Price => _price;
        public Observable<Sprite> Icon => _icon;
        public Observable<bool> IsUnlocked => _isUnlocked;
        public Observable<bool> IsIncomeReady => _isIncomeReady;
        
        private readonly ReactiveProperty<string> _price = new("");
        private readonly ReactiveProperty<Sprite> _icon = new(null);
        private readonly ReactiveProperty<bool> _isUnlocked = new(false);
        
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
            _planet.OnGathered += IncomeGathered;
            
            UpdateState();
        }

        private void IncomeGathered(int obj)
        {
            OnIncomeGather?.Invoke();
        }
        public void Dispose()
        {
            _planet.OnIncomeReady -= IncomeReady;
            _planet.OnUnlocked -= UnlockPlanet;
            _planet.OnGathered -= IncomeGathered;
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
                _planet.GatherIncome();
                return;
            }
        }

        public void OnLongClicked()
        {
            planetPopup.Show(new PlanetPopupPresentation.Args(this._planet));
        }
        
        public class Factory : PlaceholderFactory<IPlanet, PlanetPresentation>
        {
        }
    }
}