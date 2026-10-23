using Game.Presenters;
using R3;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Views
{
    public sealed class IncomeView : MonoBehaviour
    {
        [ShowInInspector]
        private IncomePresentation _incomePresentation;

        [SerializeField] private Image ProgressBar;
        [SerializeField] private Image IncomeBar;
        [SerializeField] private TMP_Text _text;
        
        private readonly CompositeDisposable _disposables = new();
        
        public void Construct(IncomePresentation incomePresentation)
        {
            _incomePresentation = incomePresentation;
            _incomePresentation.Persent.Subscribe(x => ProgressBar.fillAmount = x).AddTo(_disposables);
            _incomePresentation.IsIncomeReady.Subscribe(x => ShowProggresBar(!x)).AddTo(_disposables);
            _incomePresentation.isUnlock.Subscribe(x => ShowProggresBar(x)).AddTo(_disposables);
            _incomePresentation.CurrentTime.Subscribe(x => _text.text = x).AddTo(_disposables);
            
        }
        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        private void ShowProggresBar(bool obj)
        {
            Debug.Log("Show: " +  obj);
            ProgressBar.enabled = obj;
            IncomeBar.enabled = obj;
            _text.enabled = obj;
        }
    }
}