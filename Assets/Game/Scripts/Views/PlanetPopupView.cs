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
            _presentation.Title.Subscribe(x => _planetName.text = x).AddTo(_disposables);
            _presentation.Level.Subscribe(x => _level.text = x).AddTo(_disposables);
            _presentation.Population.Subscribe(x => _population.text = x).AddTo(_disposables);
            _presentation.Income.Subscribe(x => _income.text = x).AddTo(_disposables);
            _presentation.UpgradeCost.Subscribe(x => _upgradeCost.text = x).AddTo(_disposables);
            _presentation.Icon.Subscribe(x => _planetImage.sprite = x).AddTo(_disposables);
            _presentation.IsMaxLevel.Subscribe(x => SetUpgradeButtonColor(x)).AddTo(_disposables);
            
            _closeButton.onClick.AddListener(_presentation.OnClose);
            _upgradeButton.onClick.AddListener(_presentation.OnUpgrade);
        }

        [Sirenix.OdinInspector.Button]
        public void Show(bool isOpen)
        {
            this.gameObject.SetActive(isOpen);
        }

        private void SetUpgradeButtonColor(bool isMax)
        {
            if (isMax)
            {
                _upgradeButton.GetComponent<Image>().color = Color.darkGray;
                return;
            }
            _upgradeButton.GetComponent<Image>().color = new Color32(75,154,176, 255);
            
        }
        private void OnDestroy()
        {
            _disposables.Dispose();
            _closeButton.onClick.RemoveListener(_presentation.OnClose);
            _upgradeButton.onClick.RemoveListener(_presentation.OnUpgrade);
        }
    }
}