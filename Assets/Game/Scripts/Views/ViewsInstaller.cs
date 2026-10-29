using System.Collections.Generic;
using Game.Presenters;
using UnityEngine;
using Zenject;

namespace Game.Views
{
    public sealed class ViewsInstaller : MonoInstaller
    {
        [SerializeField]
        private List<PlanetView> _planetViews;
        
        [SerializeField]
        private RectTransform _moneyRectTransform;
        public override void InstallBindings()
        {
            List<PlanetPresentation> presentations = Container.ResolveAll<PlanetPresentation>();

            foreach (var VARIABLE in _planetViews)
            {
                this.Container.BindInterfacesAndSelfTo<PlanetView>().FromInstance(VARIABLE).AsCached();
            }
            Container.BindInterfacesAndSelfTo<PlanetCollectionView>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<RectTransform>().FromInstance(_moneyRectTransform).AsSingle().NonLazy();
        }
    }
}