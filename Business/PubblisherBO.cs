using library_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace library_system.Business
{
    public class PubblisherBO
    {
        private readonly AppDbContext _context;
        public PubblisherBO(AppDbContext context)
        {
            _context = context;
        }


        public IQueryable<Pubblisher> getAllPubblishers()
        {
            return getAllPubblishers(false);
        }

        public IQueryable<Pubblisher> getAllPubblishers(bool withHidden)
        {
            var pubblishersQuery = _context.Pubblishers.AsQueryable();

            if (!withHidden)
            {
                //pubblishersQuery = pubblishersQuery.Where(pubblisher => pubblisher.isHidden != true);
            }

            return pubblishersQuery.OrderBy(pubblisher => pubblisher.Company);
        }

        public IQueryable<Pubblisher> getPubblisherByID(int Id)
        {
            return _context.Pubblishers.Where(pubblisher => pubblisher.Id == Id);
        }

        public bool CreatePubblisher(Pubblisher pubblisher)
        {
            try
            {
                if (pubblisher != null)
                {
                    _context.Pubblishers.Add(pubblisher);
                    _context.SaveChanges();
                    return true;
                }
                return false;

            }
            catch
            {
                return false;
            }
        }

        public bool DeletePubblisher(int id)
        {

            var pubblisher = _context.Pubblishers.Find(id);
            if (pubblisher == null)
            {
                return false;
            }
            else
            {
                _context.Remove(pubblisher);
                _context.SaveChanges();
                return true;
            }
        }
        public bool updatePubblisher(Pubblisher updated)
        {
            var Pubblishers = _context.Pubblishers.Find(updated.Id);

            if (Pubblishers == null)
            {
                return false;
            }
            else
            {
                Pubblishers.Company = updated.Company;
                _context.SaveChanges();
                return true;
            }
        }

        public IQueryable<Pubblisher> SearchAsync(string? term)
        {
            var query = _context.Pubblishers
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                term = term.Trim();
                query = query.Where(p => EF.Functions.Like(p.Company!, $"%{term}%"));
            }
            query =  query
                .OrderBy(p => p.Company);
            return  query;
        }
    }
}
