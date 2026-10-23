using System;
using System.Collections;
using Game.Presenters;
using Modules.UI;
using R3;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Views
{
    public class PlanetView : MonoBehaviour
    {
        [ShowInInspector]
        private PlanetPresentation _planetPresentation;
        
        [SerializeField]
        private MoneyView _moneyView;
        
        [SerializeField] private SmartButton button;

        [SerializeField] private TMP_Text _price;

        [SerializeField] private Image _lock;

        [SerializeField] private Image _coin;
        [SerializeField] private Image _priceCoin;

        [SerializeField] private Image _planetImage;

        [SerializeField] private UIFlyToTarget _flyToTarget;
        
        private readonly CompositeDisposable _disposables = new();
        
        public void Construct(PlanetPresentation presentation)
        {
            this._planetPresentation = presentation;
            
            _planetPresentation.Price.Subscribe(x => _price.text = x).AddTo(_disposables);
            _planetPresentation.IsUnlocked.Subscribe(x => SetEnablePriceCoinImage(!x) ).AddTo(_disposables);
            _planetPresentation.IsUnlocked.Subscribe(x => SetEnableLockImage(!x) ).AddTo(_disposables);
            _planetPresentation.IsIncomeReady.Where(x => x).Subscribe(x => SetEnableCoinImage(x)).AddTo(_disposables);
            _planetPresentation.Icon.Subscribe(x => _planetImage.sprite = x).AddTo(_disposables);
            
            
            _planetPresentation.OnIncomeGather += StartCoinAnimation;
            button.OnClick += _planetPresentation.OnClicked;
            button.OnHold += _planetPresentation.OnLongClicked;
        }

        private void OnDestroy()
        {
            _disposables.Clear();
            button.OnClick -= _planetPresentation.OnClicked;
            button.OnHold -= _planetPresentation.OnLongClicked;
        }
        public void SetEnableCoinImage(bool x) => _coin.enabled = x;
        
        public void SetEnablePriceCoinImage(bool x) => _priceCoin.enabled = x;
        
        public void SetEnableLockImage(bool x) => _lock.enabled = x;

        public void StartCoinAnimation()
        {
            var anim = _flyToTarget.Fly(_coin.gameObject.GetComponent<RectTransform>());
            anim.onComplete += () => SetEnableCoinImage(false);
        }
    }
}