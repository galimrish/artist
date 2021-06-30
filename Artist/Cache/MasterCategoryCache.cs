using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Artist.Interfaces;
using System.Data.Entity;
using Artist.Helpers;

namespace Artist.Cache
{
    public class MasterCategoryCache : IMasterCategoryCache
    {
        private const string _cacheNameMasterCategory = "MasterCategory";        

        private readonly MemoryCache _cache = MemoryCache.Default;
        
        private readonly TimeSpan _cacheLifeTime;

        private readonly IDbContextFactory _dbContextFactory;

        public MasterCategoryCache(IDbContextFactory dbContextFactory,
                                IAppSettings appSettings)
        {
            _dbContextFactory = dbContextFactory;
            _cacheLifeTime = appSettings.DefaultCacheLifeTime;
        }

        public ICollection<IMasterCategory> GetMasterCategories()
        {
            var masterCategories = (ICollection<IMasterCategory>)_cache.Get(_cacheNameMasterCategory);
            if (masterCategories == null)
            {
                using (var context = _dbContextFactory.Create())
                {
                     var entities = context.MasterCategory
                                    .AsNoTracking()
                                    .ToArray();

                    masterCategories = AutoMapper.Mapper.Map<MasterCategory[]>(entities);

                    _cache.Add(_cacheNameMasterCategory, masterCategories, DateTimeOffset.Now.Add(_cacheLifeTime));
                }
            }

            return masterCategories;
        }
        
        public void ClearCache()
        {
            _cache.Remove(_cacheNameMasterCategory);
        }
    }
}
