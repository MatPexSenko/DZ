using System.Collections.Generic;
using Game.Presenters;
using Modules.Planets;
using UnityEngine;
using Zenject;

namespace Game.Views
{
    public sealed class PlanetViewsInitializer : IInitializable
    {
        private readonly List<PlanetView> _views;
        private readonly PlanetPresentation.Factory _factory;
        private readonly IPlanet[] _planets;

        public PlanetViewsInitializer(
            List<PlanetView> views,
            IPlanet[] planets,
            PlanetPresentation.Factory factory)
        {
            _views = views;
            _planets = planets;
            _factory = factory;
            
            Debug.Log("daafawfafw");
        }

        public void Initialize()
        {
            for (int i = 0; i < _views.Count; i++)
            {
                var presentation = _factory.Create(_planets[i]);
                _views[i].Construct(presentation);
                Debug.Log(_planets[i].Name);
            }
        }
    }
}