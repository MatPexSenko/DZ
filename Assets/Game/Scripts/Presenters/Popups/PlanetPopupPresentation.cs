using System;
using JetBrains.Annotations;
using Modules.Planets;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game
{
    [Serializable]
    public class PlanetPopupPresentation
    {
        public readonly struct Args
        {
            public readonly IPlanet planet;

            public Args(IPlanet item)
            {
                this.planet = item;
            }
        }
        public Observable<string> Title => _title;
        public Observable<string> Population => _population;
        public Observable<string> Level => _level;
        public Observable<string> Income => _income;
        public Observable<string> UpgradeCost => _upgradeCost;
        public Observable<Sprite> Icon => _icon;
        public Observable<bool> IsOpen => _isOpen;
        
        public Observable<bool> IsMaxLevel => _isMaxLevel;
        
        private readonly ReactiveProperty<string> _title = new("");
        private readonly ReactiveProperty<string> _population = new("");
        private readonly ReactiveProperty<string> _level = new("");
        private readonly ReactiveProperty<string> _income = new("");
        private readonly ReactiveProperty<string> _upgradeCost = new("");
        private readonly ReactiveProperty<Sprite> _icon = new(null);
        
        private readonly ReactiveProperty<bool> _isOpen = new(false);

        private readonly ReactiveProperty<bool> _isMaxLevel = new(false);
        
        private IPlanet _targetPlanet;
        
        public void Show(Args args)
        {
            _targetPlanet = args.planet;
            UnSubscribe();
            UpdateState(_targetPlanet);
            Subscribe(_targetPlanet);
            _isOpen.Value = true;
        }
        
        public void Hide()
        { 
            UnSubscribe();
            _isOpen.Value = false;
        }

        public void OnClose()
        {
            Hide();
        }
        
        public void OnUpgrade()
        {
            _targetPlanet.Upgrade();
        }
        
        private void UpdateState(IPlanet planet)
        {
            _title.Value = planet.Name;
            _population.Value =$"Population: {planet.Population.ToString()}" ;
            SetLevelPlanet();
            _income.Value = $"Income: {planet.MinuteIncome.ToString()}";
            _upgradeCost.Value = planet.Price.ToString();
            _icon.Value = planet.GetIcon(planet.IsUnlocked);
            ReachMaxLevel(_targetPlanet.Level);
        }

        private void Subscribe(IPlanet planet)
        {
            planet.OnPopulationChanged += SetPopulation;
            planet.OnUpgraded += SetLevelPlanet;
            planet.OnIncomeChanged += SetIncome;
            planet.OnUpgraded += SetUpgradeCost;
            planet.OnUpgraded += ReachMaxLevel;
        }
        
        private void UnSubscribe()
        {
            if (_targetPlanet != null)
            {
                _targetPlanet.OnPopulationChanged -= SetPopulation;
                _targetPlanet.OnUpgraded -= SetLevelPlanet;
                _targetPlanet.OnIncomeChanged -= SetIncome;
                _targetPlanet.OnUpgraded -= SetUpgradeCost;
                _targetPlanet.OnUpgraded -= ReachMaxLevel;
            }
        }

        private void ReachMaxLevel(int level)
        {
            if (_targetPlanet.IsMaxLevel)
            {
                _isMaxLevel.Value = true;
                return;
            }
            _isMaxLevel.Value = false;
        }

        private void SetUpgradeCost(int obj)
        {            
            _upgradeCost.Value = _targetPlanet.Price.ToString();
            if (_targetPlanet.IsMaxLevel)
                _upgradeCost.Value = "MAX";
        }

        private void SetIncome(int obj)
        {
            _income.Value = obj.ToString();
        }

        private void SetLevelPlanet(int obj = 1)
        {
            _level.Value = $"{obj}/" + $"{_targetPlanet.MaxLevel}";
        }

        private void SetPopulation(int obj)
        {
            _population.Value = $"Population: {obj.ToString()}";
        }
    }
}