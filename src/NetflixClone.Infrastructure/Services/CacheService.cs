using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace NetflixClone.Infrastructure.Services;

/// <summary>
/// Cache service using IMemoryCache
/// Provides simple in-memory caching with expiration
/// </summary>
public interface ICacheService
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);
    Task RemoveAsync(string key);
    Task RemoveByPrefixAsync(string prefix);
}

public class CacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    private readonly HashSet<string> _cacheKeys = new();
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public CacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        await Task.CompletedTask; // For async interface consistency
        
        if (_cache.TryGetValue(key, out T? value))
        {
            return value;
        }

        return default;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        await _semaphore.WaitAsync();
        try
        {
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(30),
                SlidingExpiration = TimeSpan.FromMinutes(10)
            };

            cacheOptions.RegisterPostEvictionCallback((evictedKey, evictedValue, reason, state) =>
            {
                _cacheKeys.Remove(evictedKey.ToString() ?? string.Empty);
            });

            _cache.Set(key, value, cacheOptions);
            _cacheKeys.Add(key);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task RemoveAsync(string key)
    {
        await _semaphore.WaitAsync();
        try
        {
            _cache.Remove(key);
            _cacheKeys.Remove(key);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task RemoveByPrefixAsync(string prefix)
    {
        await _semaphore.WaitAsync();
        try
        {
            var keysToRemove = _cacheKeys.Where(k => k.StartsWith(prefix)).ToList();
            foreach (var key in keysToRemove)
            {
                _cache.Remove(key);
                _cacheKeys.Remove(key);
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
