using Modules.Planets;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    [CreateAssetMenu(
        fileName = "PresentersInstallers",
        menuName = "Zenject/New PresentersInstallers"
    )]
    public sealed class PresentersInstallers : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            foreach (var Planet in Container.ResolveAll<IPlanet>())
            {
                this.Container.BindInterfacesAndSelfTo<PlanetPresentation>().AsCached().WithArguments(Planet);
            }
            
            this.Container.BindInterfacesAndSelfTo<PlanetPopupPresentation>().AsSingle();
            this.Container.BindInterfacesAndSelfTo<MoneyPresentation>().AsSingle().NonLazy();
            
            this.Container.BindInterfacesAndSelfTo<PlanetCollectionPresentation>().AsSingle().NonLazy();
        }
    }
}