
using Entities;
using Entities.DTO_s.Reponses;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    internal class HubbRepository : RepositoryBase<Hubb>, IHubbRepository
    {
        public HubbRepository(AppDbContext context) : base(context)
        {

        }
        public void CreateHubb(Hubb hubb)
        {
            Create(hubb);
        }

        public void UpdateHubb(Hubb hubb)
        {
            Update(hubb);
        } 
        
        public void DeleteSingleHubbId(Guid id)
        {
            DeleteSingleHubbId(id);
        }


        public IEnumerable<DisplayHubDTO> GetHubs(bool trackChanges)
        {
            var getHubs = FindAll(trackChanges).AsNoTracking().ToList();

            var result = getHubs ?? new List<Hubb>();

            List<DisplayHubDTO> hubs = new List<DisplayHubDTO>();

            foreach (var item in result)
            {
                var fetchHubs = new DisplayHubDTO
                {
                    Address = item.Address,
                    ImageUrl = item.Image,
                    Name = item.Name,
                    State = item.State,
                    Tags = item.Tags,
                    Website = item.Website,
                    Latitude = item.Latitude,
                    Longitude = item.Longitude
                };

                hubs.Add(fetchHubs);
            }

            return hubs;
        }

     
        public async Task<Hubb> GetHubbById(Guid id, bool trackchanges)
        {
            var getHub = await FindByCondition(h => h.HubbId == id, trackchanges)
                .AsNoTracking()              
                .SingleOrDefaultAsync();

            var result = getHub ?? null;

            return getHub;
        }

        public async Task<Hubb> GetHubbByName(string name, bool trackchanges)
        {
            var getHub = await FindByCondition(h => h.Name.ToLower() == name, trackchanges)
                 .AsNoTracking()
                 .SingleOrDefaultAsync();

            var result = getHub ?? null;

            return result;
        }

        public async Task<IEnumerable<Hubb>> GetHubbsByState(string state, bool trackchanges)
        {
            var getHub = await FindByCondition(h => h.State.ToLower() == state, trackchanges)
                .AsNoTracking()
                .ToListAsync();

          
            var result = getHub == null || getHub.Count == 0 ? null : getHub;

            return result;
        }

        public async Task<IEnumerable<Hubb>> GetHubbsByTag(string tag, bool trackchanges)
        {
            var getHub = await FindByCondition(h => h.Tags.ToLower() == tag || h.Tags.ToLower().Contains(tag), trackchanges)
               .AsNoTracking()
               .ToListAsync();

            var result = getHub == null || getHub.Count == 0 ? null : getHub;

            return result;
        }

        public async Task<Hubb> CheckIfHubExistsByName(string name, bool trackchanges)
        {
           
            var getHub = await FindByCondition(h => h.Name.ToLower().Equals(name) || h.Name.ToLower().Contains(name), trackchanges)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            var result = getHub ?? null;

            return result;

        }
    }
}
