
using Entities.DTO_s.Reponses;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IHubbRepository
    {
        void CreateHubb(Hubb hubb);
        void UpdateHubb(Hubb hubb);
        IEnumerable<DisplayHubDTO> GetHubs(bool trackChanges);
        Task<Hubb> GetHubbByName(string name, bool trackchanges);
        Task<Hubb> CheckIfHubExistsByName(string name, bool trackchanges);
        Task<IEnumerable<Hubb>> GetHubbsByState(string state, bool trackchanges);
        Task<IEnumerable<Hubb>> GetHubbsByTag(string tag, bool trackchanges);
        void DeleteSingleHubbId(Guid id);

    }
}
