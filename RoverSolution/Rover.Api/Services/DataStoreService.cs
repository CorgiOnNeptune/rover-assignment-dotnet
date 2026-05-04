using JsonFlatFileDataStore;
using Rover.Core.Models;

namespace Rover.Api.Services
{
    public class DataStoreService(DataStore store)
    {
        private IDocumentCollection<Simulation> Simulations = store.GetCollection<Simulation>("simulations");

        public async Task InsertAsync(Simulation simulation)
        {

            await Simulations.InsertOneAsync(simulation);
        }

        public Simulation? GetById(int id)
        {
            Simulation? simulation = Simulations.Find(sim => sim.Id == id).SingleOrDefault();
            return simulation;
        }

        public IEnumerable<Simulation> GetAll()
        {
            return Simulations.Find(_ => true).OrderByDescending(s => s.RequestTime).ToList();
        }

        public Task<bool> UpdateAsync(int id, Simulation simulation)
        {
            return Simulations.UpdateOneAsync(id, simulation);
        }
    }
}
