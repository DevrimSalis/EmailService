using EmailService.Domain.Entities;
using EmailService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Infrastructure.Persistence
{
    public class EmailRepository : IRepository<Email>
    {
        private readonly EmailContext _context;

        public EmailRepository(EmailContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Email email)
        {
            await _context.Emails.AddAsync(email);
            await _context.SaveChangesAsync();
        }
        public async Task<Email> GetByIdAsync(Guid id)
        {
            return await _context.Emails.FindAsync(id);
        }

        public async Task<List<Email>> GetAllAsync()
        {
            return await _context.Emails.ToListAsync();
        }

        public async Task UpdateAsync(Email email)
        {
            _context.Emails.Update(email);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Email email)
        {
            _context.Emails.Remove(email);
            await _context.SaveChangesAsync();
        }
    }
}