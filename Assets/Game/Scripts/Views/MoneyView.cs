using System.Globalization;
using DG.Tweening;
using R3;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;
using Game.Presenters;

namespace Game.Views
{
    public sealed class MoneyView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinCount;
        
        
        [SerializeField] private float _duration = 0.6f;
        [ShowInInspector] private MoneyPresentation _moneyPresentation;
        private readonly CompositeDisposable _disposables = new();

        private Tween _tween;
        private int _displayed;

        [Inject]
        public void Construct(MoneyPresentation presentation)
        {
            _moneyPresentation = presentation;
        }

        private void OnEnable()
        {
            _moneyPresentation.Money
                .Subscribe(SetMoneyCount)
                .AddTo(_disposables);
        }

        private void OnDisable()
        {
            _disposables.Clear();
            _tween?.Kill();
        }

        public void SetMoneyCount(string coinCount)
        {
            if (!int.TryParse(coinCount, out var target))
            {
                _tween?.Kill();
                _coinCount.text = coinCount;
                return;
            }

            _tween?.Kill();
            DOTween.To(
                () => _displayed,
                value =>
                {
                    _displayed = value;
                    _coinCount.text = ((long)_displayed).ToString();
                },
                target,
                _duration
            );
        }
    }
}