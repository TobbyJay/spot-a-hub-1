
using Entities;
using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        private IHubbRepository _hubbRepository;
      

        public RepositoryManager(AppDbContext context)
        {
            _context = context;
        }

        public IHubbRepository Hubb 
        {
            get
            {

                if (_hubbRepository == null)
                    _hubbRepository = new HubbRepository(_context);


                return _hubbRepository;
            }
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
