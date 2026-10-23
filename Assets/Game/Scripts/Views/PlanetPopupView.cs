using R3;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Views
{
    public class PlanetPopupView : MonoBehaviour
    {
        [SerializeField]
        private Button _closeButton;
        
        [SerializeField]
        private Button _upgradeButton;
        
        [SerializeField]
        private TMP_Text _planetName;
        
        [SerializeField]
        private TMP_Text _population;
        
        [SerializeField]
        private TMP_Text _level;
        
        [SerializeField]
        private TMP_Text _income;
        
        [SerializeField]
        private TMP_Text _upgradeCost;
        
        [SerializeField]
        private Image _planetImage;
        
        [SerializeField]
        private PlanetPopupPresentation _presentation;
        
        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(PlanetPopupPresentation presentation)
        {
            _presentation = presentation;
            
            _presentation.IsOpen.Subscribe(x => Show(x));
        }

        [Sirenix.OdinInspector.Button]
        public void Show(bool isOpen)
        {
            if (isOpen)
            {
                _presentation.Title.Subscribe(x => _planetName.text = x).AddTo(_disposables);
                _presentation.Level.Subscribe(x => _level.text = $"Level: {x}").AddTo(_disposables);
                _presentation.Population.Subscribe(x => _population.text = $"Population: {x}").AddTo(_disposables);
                _presentation.Income.Subscribe(x => _income.text = $"Income: {x}").AddTo(_disposables);
                _presentation.UpgradeCost.Subscribe(x => _upgradeCost.text = x).AddTo(_disposables);
                _presentation.Icon.Subscribe(x => _planetImage.sprite = x).AddTo(_disposables);

                _closeButton.onClick.AddListener(_presentation.OnClose);
                _upgradeButton.onClick.AddListener(_presentation.OnUpgrade);
            
                this.gameObject.SetActive(true);
                return;
            }
            Hide();
        }
        
        private void Hide()
        { 
            _disposables.Clear();
            _closeButton.onClick.RemoveListener(_presentation.OnClose);
            _upgradeButton.onClick.RemoveListener(_presentation.OnUpgrade);
            
            this.gameObject.SetActive(false);
        }
    }
}