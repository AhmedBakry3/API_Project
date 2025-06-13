﻿using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service
{
    public class CacheService(ICacheRepository cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string CacheKey) => await cacheRepository.GetAsync(CacheKey);

        public async Task SetAsync(string CacheKey, object CacheValue, TimeSpan TimeToLive)
        {
            var Value = JsonSerializer.Serialize(CacheValue);
            await cacheRepository.SetAsync(CacheKey, Value, TimeToLive);
        }
    }
}
