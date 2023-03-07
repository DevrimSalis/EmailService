using EmailService.Domain.Entities;
using EmailService.Domain.Interfaces;
using EmailService.Infrastructure.Cache;
using EmailService.Infrastructure.MessageQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly ICache _cache;
        private readonly IMessageQueue _queue;
        private readonly IRepository<Email> _repository;

        public EmailService(ICache cache, IMessageQueue queue, IRepository<Email> repository)
        {
            _cache = cache;
            _queue = queue;
            _repository = repository;
        }

        public async Task SendAsync(Email email)
        {
            var cacheKey = $"{email.To}:{email.Subject}";
            var cachedResult = await _cache.GetAsync<bool>(cacheKey);

            if (!cachedResult)
            {
                _queue.Publish(email, "email-queue");
                await _cache.SetAsync(cacheKey, true, TimeSpan.FromMinutes(10));
                await _repository.AddAsync(email);
            }
        }
    }
}