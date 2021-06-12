namespace Controllers
{
    public class Planet: SpaceBodyControllerBase
    {        
        private PlanetType _planetType;

        public enum PlanetType
        {
            Ice,
            Magma,
            Rings,
            Water,
            Radioactive
        }

        public void SetPlanetType(PlanetType planetType)
        {
            _planetType = planetType;
        }
    }
}