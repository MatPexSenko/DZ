using System.Collections;
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
        [SerializeField] 
        private TMP_Text _coinCount;
        
        [ShowInInspector]private MoneyPresentation _presentation;
        private readonly CompositeDisposable _disposables = new();
        
        private WaitForSeconds _waitForSeconds;
        
        [Inject]
        public void Construct(MoneyPresentation presentation)
        {
            _presentation = presentation;
            Debug.Log($"Money Presentation {_presentation}");
        }
        private void OnEnable()
        {
            _presentation.Money
                .Subscribe(x => SetCoinCount(x))
                .AddTo(_disposables);

            _presentation.OnMoneyEarned += SetCoinCountWithDelay;
        }

        private void OnDisable()
        {
            _disposables.Dispose();
        }
        public void SetCoinCount(string coinCount)
        {
            Debug.Log($"Money Presentation {_presentation}");
            _coinCount.text = coinCount;
        }

        public void SetCoinCountWithDelay(string coinCount)
        {
            StartCoroutine(SetCoinCountRoutine(coinCount));
        }

        private IEnumerator SetCoinCountRoutine(string coinCount)
        {
            yield return new WaitForSeconds(0.7f);

            _coinCount.text = coinCount;
        }
    }
}