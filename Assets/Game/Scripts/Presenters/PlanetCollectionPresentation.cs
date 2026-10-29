using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Presenters
{
    public class PlanetCollectionPresentation
    {
        public readonly List<PlanetPresentation> PlanetsPresentation;

        public PlanetCollectionPresentation(List<PlanetPresentation> planetsPresentation)
        {
            this.PlanetsPresentation = planetsPresentation;
        }
    }
}