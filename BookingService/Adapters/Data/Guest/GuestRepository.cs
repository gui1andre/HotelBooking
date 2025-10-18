using Entities = Domain.Entities;
using Domain.Ports;
using System;
using Microsoft.EntityFrameworkCore;
namespace Data.Guest
{
    public class GuestRepository : IGuestRepository
    {
        private readonly HotelDbContext _context;
        public GuestRepository(HotelDbContext hotelDbContext)
        {
            _context = hotelDbContext;

        }
        public async Task<int> Create(Entities.Guest guest)
        {
            _context.Guests.Add(guest);
            _context.SaveChanges();
            return guest.Id;
        }

        public Task Update(Entities.Guest guest)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Guest> GetByEmail(string email) => _context.Guests.FirstOrDefaultAsync(g => g.Email == email);

        public Task<Domain.Entities.Guest?> GetById(int id) => _context.Guests.FirstOrDefaultAsync(g => g.Id == id);
    }
}
