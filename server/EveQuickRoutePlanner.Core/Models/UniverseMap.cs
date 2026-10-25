namespace EveQuickRoutePlanner.Core.Models
{
    public sealed class UniverseMap
    {
        #region Private Fields
        private readonly Dictionary<int, SolarSystem> _solarSystems;
        private readonly Dictionary<int, Station> _stations;
        private readonly Dictionary<int, int[]> _adjacentSystems;
        #endregion Private Fields

        #region Constructors
        
        public UniverseMap(IEnumerable<SolarSystem> solarSystems, IEnumerable<Station> stations, IEnumerable<(int From, int To)> starGates)
        {
            _solarSystems = solarSystems.ToDictionary(solarSystem => solarSystem.Id);
            _stations = stations.ToDictionary(station => station.Id);

            _adjacentSystems = starGates
                .GroupBy(starGate => starGate.From)
                .ToDictionary(group => group.Key, //From
                    group => group.Select(starGate => starGate.To).Distinct().ToArray());
        }
        
        #endregion Constructors

        #region Properties
        
        public int SolarSystemCount => _solarSystems.Count;

        public int StationCount => _stations.Count;

        public IReadOnlyCollection<SolarSystem> SolarSystems => _solarSystems.Values;

        public IReadOnlyCollection<Station> Stations => _stations.Values;
        
        #endregion Properties

        #region Methods
        
        public SolarSystem? GetSystem(int id) => _solarSystems.TryGetValue(id, out var system) ? system : null;

        public SolarSystem? GetSystem(string name) => _solarSystems.Values.FirstOrDefault(solarSystem => solarSystem.Name == name);
        
        public int? GetSystemId(string name) => GetSystem(name)?.Id;

        public Station? GetStation(int id) => _stations.TryGetValue(id, out var station) ? station : null;

        public Station? GetStation(string name) => _stations.Values.FirstOrDefault(station => station.Name == name);

        public int? GetStationId(string name) => GetStation(name)?.Id;

        public IReadOnlyList<int> GetNeighbours(int solarSystemId) => _adjacentSystems.TryGetValue(solarSystemId, out var neighbours) ? neighbours : [];

        #endregion Methods
    }
}
