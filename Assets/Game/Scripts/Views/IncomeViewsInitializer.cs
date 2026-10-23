using System.Collections.Generic;
using Game.Presenters;
using Modules.Planets;
using Zenject;

namespace Game.Views
{
    public sealed class IncomeViewsInitializer : IInitializable
    {
        private readonly List<IncomeView> _views;
        private readonly IPlanet[] _planets;
        private readonly PlanetPresentation.Factory _planetFactory;
        private readonly IncomePresentation.Factory _incomeFactory;

        public IncomeViewsInitializer(
            List<IncomeView> views,
            IPlanet[] planets,
            IncomePresentation.Factory incomeFactory)
        {
            _views = views;
            _planets = planets;
            _incomeFactory = incomeFactory;
        }

        public void Initialize()
        {
            for (int i = 0; i < _views.Count; i++)
            {
                var planet = _planets[i];
                
                var incomePresentation = _incomeFactory.Create(planet);

                _views[i].Construct(incomePresentation);
            }
        }
    }
}