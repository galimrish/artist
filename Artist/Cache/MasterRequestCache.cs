using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Artist.Interfaces;
using System.Data.Entity;
using Artist.Helpers;
using AutoMapper;
using System.Linq.Expressions;
using LinqKit;
//using Artist.Mapper;

namespace Artist.Cache
{
    public class MasterRequestCache : IMasterRequestCache
    {
        private const string _cacheName = "MasterRequest";
        private readonly MemoryCache _cache = MemoryCache.Default;        
        private readonly TimeSpan _cacheLifeTime;
        private readonly IDbContextFactory _dbContextFactory;
        //private readonly IMapper _mapper;

        public MasterRequestCache(IDbContextFactory dbContextFactory,
                                IAppSettings appSettings/*,
                                IMapper mapper*/)
        {
            _dbContextFactory = dbContextFactory;
            _cacheLifeTime = appSettings.DefaultCacheLifeTime;
            //_mapper = mapper;
        }

        public ICollection<IMasterRequest> GetMasterRequests()
        {
            var masterRequests = (ICollection<IMasterRequest>)_cache.Get(_cacheName);
            if (masterRequests == null)
            {
                using (var context = _dbContextFactory.Create())
                {
                    var entities = context.MasterRequest
                                    .AsNoTracking()
                                    .ToArray();

                    masterRequests = AutoMapper.Mapper.Map<MasterRequest[]>(entities);

                    _cache.Add(_cacheName, masterRequests, DateTimeOffset.Now.Add(_cacheLifeTime));
                }
            }

            return masterRequests;
        }

        public void ClearCache()
        {
            _cache.Remove(_cacheName);
        }
    }
}
