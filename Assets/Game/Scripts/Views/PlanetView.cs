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
        public string Id => _id;

        [SerializeField]
        private string _id;
        
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
        
        [Header("Income")]
        [SerializeField] private Image ProgressBar;
        [SerializeField] private Image IncomeBar;
        [SerializeField] private TMP_Text _text;
        
        [Header("CoinAnimation")]
        [SerializeField]private RectTransform _coinRectTransform;
        [SerializeField]private RectTransform _moneyRectTransform;
        [SerializeField] private ParticleAnimator _particleAnimator;
        
        private readonly CompositeDisposable _disposables = new();

        public void Construct(PlanetPresentation presentation)
        {
            this._planetPresentation = presentation;
            
            _planetPresentation.Price.Subscribe(x => _price.text = x).AddTo(_disposables);
            _planetPresentation.IsUnlocked.Subscribe(x => SetEnablePriceCoinImage(!x) ).AddTo(_disposables);
            _planetPresentation.IsUnlocked.Subscribe(x => SetEnableLockImage(!x) ).AddTo(_disposables);
            _planetPresentation.IsIncomeReady.Subscribe(x => SetEnableCoinImage(x)).AddTo(_disposables);
            _planetPresentation.Icon.Subscribe(x => _planetImage.sprite = x).AddTo(_disposables);
            
            _planetPresentation.Persent.Subscribe(x => ProgressBar.fillAmount = x).AddTo(_disposables);
            _planetPresentation.IsIncomeReady.Subscribe(x => ShowProggresBar(!x)).AddTo(_disposables);
            _planetPresentation.isUnlock.Subscribe(x => ShowProggresBar(x)).AddTo(_disposables);
            _planetPresentation.CurrentTime.Subscribe(x => _text.text = x).AddTo(_disposables);
            
            
            _planetPresentation.OnStartIncomeGather.Subscribe(unit => SetEnableCoinImage(false)).AddTo(_disposables);
            _planetPresentation.OnStartIncomeGather.Skip(1).Subscribe(unit => StartCoinAnimation()).AddTo(_disposables);
            button.OnClick += _planetPresentation.OnClicked;
            button.OnHold += _planetPresentation.OnLongClicked;
        }
        
        private void ShowProggresBar(bool obj)
        {
            ProgressBar.enabled = obj;
            IncomeBar.enabled = obj;
            _text.enabled = obj;
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
            _particleAnimator.Emit(_coinRectTransform.position, _moneyRectTransform.position,
                1,
                _planetPresentation.GatherIncome );
        }
    }
}