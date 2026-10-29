using System.Collections.Generic;
using Game.Presenters;
using UnityEngine;
using Zenject;

namespace Game.Views
{
    public sealed class PlanetCollectionView : IInitializable
    {
        private readonly Dictionary<string ,PlanetView> _views = new Dictionary<string ,PlanetView>();
        private readonly PlanetCollectionPresentation _planetCollection;

        public PlanetCollectionView(
            List<PlanetView> views,
            PlanetCollectionPresentation planetCollection)
        {
            foreach (PlanetView view in views)
            {
                _views.Add(view.Id, view);
            }
            _planetCollection = planetCollection;
        }

        public void Initialize()
        {
            foreach (var presentation in _planetCollection.PlanetsPresentation)
            {
                _views.TryGetValue(presentation.planetId, out var view);
                if(view)
                    view.Construct(presentation);
            }
        }
    }
}