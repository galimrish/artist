using AutoMapper;
using Artist.Exceptions;
using Artist.Helpers;
using Artist.Interfaces;
using Common.Helpers.Extensions;
using LinqKit;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Artist
{
    public class Artist : IArtistService
    {
        private readonly IAppSettings _appSettings;
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IMasterCategoryCache _masterCategoryCache;
        private readonly IMasterRequestCache _masterRequestCache;

        public Artist(IAppSettings appSettings,
                        IDbContextFactory dbContextFactory,
                        IMasterCategoryCache masterCategoryCache,
                        IMasterRequestCache masterRequestCache)
        {
            _appSettings = appSettings;
            _dbContextFactory = dbContextFactory;
            _masterCategoryCache = masterCategoryCache;
            _masterRequestCache = masterRequestCache;
        }

        public bool IsAlive()
        {
            return true;
        }

        public IMasterCategory[] GetMasterCategories()
        {
            return _masterCategoryCache.GetMasterCategories().ToArray();
        }

        public IMasterCategory GetMasterCategory(string category)
        {
            return _masterCategoryCache.GetMasterCategories().FirstOrDefault(mc => mc.Name.ToLower() == category.ToLower());
        }

        public IMasterRequest[] GetMasterRequests(long? customerId = null)
        {
            var masterRequests = _masterRequestCache.GetMasterRequests()?.OrderBy(r => r.RequestDate)?.ToArray();
            if (customerId == null)
            {
                return masterRequests;
            }

            return masterRequests.Where(r => r.CustomerId == customerId)?.ToArray();
        }

        public IMasterRequest[] GetMasterRequests(IMasterRequestFilter filter)
        {
            var predicate = PredicateBuilder.New<IMasterRequest>(true);

            if (filter != null)
            {
                if (filter.CategoryIds?.Length > 0)
                    predicate = predicate.And(p => filter.CategoryIds.Contains(p.MasterCategoryId));

                if (filter.MasterRequestId.HasValue)
                    predicate = predicate.And(p => filter.MasterRequestId == p.Id);

                if (filter.StateIds?.Length > 0)
                    predicate = predicate.And(p => filter.StateIds.Contains(p.State));

                if (filter.RequestDateFrom.HasValue)
                    predicate = predicate.And(p => p.RequestDate >= filter.RequestDateFrom);

                if (filter.RequestDateTo.HasValue)
                    predicate = predicate.And(p => p.RequestDate <= filter.RequestDateTo);

                if (filter.StateChangeDateFrom.HasValue)
                    predicate = predicate.And(p => p.StateChangeDate >= filter.StateChangeDateFrom);

                if (filter.StateChangeDateTo.HasValue)
                    predicate = predicate.And(p => p.StateChangeDate <= filter.StateChangeDateTo);
            }

            var masterRequests = _masterRequestCache.GetMasterRequests()?.Where(predicate)?.OrderBy(r => r.RequestDate)?.ToArray();

            return masterRequests?.ToArray();
        }

        public IMasterRequest GetLastMasterRequest(long customerId)
        {
            var requests = GetMasterRequests(customerId);
            if (requests.IsNullOrEmpty())
            {
                return null;
            }

            var lastRequest = requests.Last();
            if(lastRequest?.State == (int)MasterRequestState.Withdrawn)
            {
                return null;
            }

            return lastRequest;
        }

        public IMasterRequest GetMasterRequestById(int masterRequestId)
        {
            return _masterRequestCache.GetMasterRequests()?.FirstOrDefault(r => r.Id == masterRequestId);
        }

        public void CreateMasterRequest(IMasterRequest masterRequest)
        {
            using (var context = _dbContextFactory.Create())
            {
                var dbEntity = Mapper.Map<Model.MasterRequest>(masterRequest);
                context.MasterRequest.Add(dbEntity);
                context.SaveChanges();
            }
            _masterRequestCache.ClearCache();
        }

        public void DeleteMaster(long masterId, bool ignoreStatus = false)
        {
            using (var context = _dbContextFactory.Create())
            {
                var dbEntity = context.Master.Find(masterId);
                if (dbEntity == null)
                {
                    throw new DeleteNonExistMasterException($"{masterId} not found");
                }

                if(ignoreStatus == false && dbEntity.Status == "Deleted")
                {
                    throw new DeleteNonExistMasterException($"{masterId} alredy deleted");
                }
                dbEntity.Status = "Deleted";
                context.SaveChanges();
            }
        }

        public void DeleteAllMasters()
        {
            var masters = GetMasters();
            foreach(var master in masters)
            {
                DeleteMaster(master.Id);
            }
        }

        public IMasterRequestComment[] GetMasterRequestComments(int masterRequestId)
        {
            using (var context = _dbContextFactory.Create())
            {
                var comments = context.MasterRequestComment
                                    ?.AsNoTracking()
                                    ?.Where(mrc => mrc.MasterRequestId == masterRequestId)
                                    ?.ToArray();

                return Mapper.Map<MasterRequestComment[]>(comments);
            }
        }

        public void CreateMasterRequestComment(IMasterRequestComment comment)
        {
            using (var context = _dbContextFactory.Create())
            {
                var dbEntity = Mapper.Map<Model.MasterRequestComment>(comment);
                context.MasterRequestComment.Add(dbEntity);
                context.SaveChanges();
            }
        }

        public void UpdateMasterRequest(IMasterRequest masterRequest)
        {
            using (var context = _dbContextFactory.Create())
            {
                var dbEntity = context.MasterRequest?.Find(masterRequest.Id);
                if (dbEntity == null)
                {
                    throw new MasterRequestNotFoundException($"{masterRequest.Id} not found");
                }
                
                dbEntity.CopyPropertiesFrom(masterRequest, new string[] { "Id" });
                context.SaveChanges();
            }

            _masterRequestCache.ClearCache();
        }

        public ICustomerInfo[] GetMasterCustomersInfo(long masterId)
        {
            using (var context = _dbContextFactory.Create())
            {
                var customersInfo = context.Action
                                           .Where(dd => (dd.MasterId == masterId) && (dd.ExpirationDate >= DateTime.UtcNow))
                                           .Select(a => new CustomerInfo
                                           {
                                               ClientId = a.CustomerId,
                                               Name = a.CustomerName,
                                               Pan = a.CustomerPan
                                           })
                                           .ToArray();

                return customersInfo;
            }
        }

        public IMaster[] GetMasters()
        {
            using (var context = _dbContextFactory.Create())
            {
                var masters = context.Master
                                    ?.AsNoTracking()
                                    ?.Where(m => m.Status != "Deleted")
                                    ?.ToArray();

                return Mapper.Map<Master[]>(masters);
            }
        }

        public IMaster[] GetMasters(IMasterFilter filter)
        {
            using (var context = _dbContextFactory.Create())
            {
                var predicate = PredicateBuilder.New<Model.Master>(true);
                if (filter != null)
                {
                    if (!string.IsNullOrWhiteSpace(filter.MasterName))
                    {
                        predicate = predicate.And(p => p.Name.ToLower().Contains(filter.MasterName.ToLower()));
                    }
                    if (filter.CategoryIds?.Length > 0)
                    {
                        predicate = predicate.And(p => filter.CategoryIds.Contains(p.MasterCategoryId));
                    }
                    if (!string.IsNullOrWhiteSpace(filter.MasterPhoneNumber))
                    {
                        predicate = predicate.And(p => p.MSISDN.Contains(filter.MasterPhoneNumber));
                    }
                    if (filter.CanBindCustomer != null)
                    {
                        predicate = predicate.And(p => filter.CanBindCustomer == p.CanBindCustomer);
                    }
                }
                predicate = predicate.And(p => p.Status != "Deleted");

                var masters = context.Master
                                    ?.AsNoTracking()
                                    ?.Where(predicate)
                                    ?.ToArray();

                
                return Mapper.Map<Master[]>(masters);
            }
        }

        public IMaster GetMasterById(long masterId)
        {
            using (var context = _dbContextFactory.Create())
            {
                var master = context.Master.FirstOrDefault(m => m.Id == masterId);
                if (master == null || master.Status == "Deleted") { return null; }
                else
                {
                    return Mapper.Map<Master>(master);
                }
            }
        }

        public IMaster GetMasterByPan(string pan)
        {
            using (var context = _dbContextFactory.Create())
            {
                var master = context.Master.FirstOrDefault(m => m.Pan == pan);
                if (master == null || master.Status == "Deleted") { return null; }
                else
                {
                    return Mapper.Map<Master>(master);
                }
            }
        }

        public void SaveAction(IAction action)
        {
            using (var context = _dbContextFactory.Create())
            {
                var masterDb = context.Master.FirstOrDefault(m => m.Id == action.MasterId);
                if (masterDb == null || masterDb.Status == "Deleted")
                {
                    throw new MasterNotFoundException($"{action.MasterId} not found");
                }

                var existingActionDb = context.Action.SingleOrDefault(a => (a.CustomerId == action.CustomerId && a.MasterId == a.MasterId));
                if (existingActionDb != null)
                {
                    DeleteInactiveAction(context, existingActionDb);
                }

                var actionDb = Mapper.Map<Model.Action>(action);
                var actionHistRecordDb = new Model.ActionHistory
                {
                    MasterId = action.MasterId,
                    CustomerId = action.CustomerId,
                    CustomerName = action.CustomerName,
                    CustomerPan = action.CustomerPan,
                    Created = action.Created,
                    ExpirationDate = action.ExpirationDate,
                    Deleted = null
                };

                using (var scope = new TransactionScope())
                {
                    context.Action.Add(actionDb);
                    context.ActionHistory.Add(actionHistRecordDb);
                    context.SaveChanges();

                    scope.Complete();
                }
            }
        }

        private void DeleteInactiveAction(Model.ArtistDbContext context, Model.Action actionDb)
        {
            using (var scope = new TransactionScope())
            {
                var histRecordDb = context.ActionHistory.First(ah => (ah.MasterId == actionDb.MasterId
                                                                   && ah.CustomerId == actionDb.CustomerId
                                                                   && ah.ExpirationDate == actionDb.ExpirationDate));

                histRecordDb.Deleted = DateTime.UtcNow;                
                context.Action.Remove(actionDb);
                context.SaveChanges();

                scope.Complete();
            }
        }

        public void UpdateMasterBindProperty(IMaster master)
        {
            using (var context = _dbContextFactory.Create())
            {
                var masterDb = context.Master.FirstOrDefault(m => m.Id == master.Id);
                if (masterDb == null)
                {
                    throw new MasterNotFoundException($"{master.Id} not found");
                }

                masterDb.CanBindCustomer = master.CanBindCustomer;
                context.SaveChanges();
            }
        }

        public void CreateMaster(IMaster master)
        {
            using (var context = _dbContextFactory.Create())
            {
                var masterDb = context.Master.Find(master.Id);
                if (masterDb != null)
                {
                    // Если мастер помечен как удаленный обновляем запись в бд, иначе ошибка
                    if (masterDb.Status == "Deleted")
                    {
                        Mapper.Map(master, masterDb);
                    }
                    else
                    {
                        throw new MasterExistException($"{master.Id} already exists");
                    }
                }
                else
                {
                    context.Master.Add(Mapper.Map<Model.Master>(master));
                }

                var category = GetCategoryName(master.MasterCategoryId);
                if (string.IsNullOrWhiteSpace(category))
                {
                    throw new MasterCategoryNotFoundException($"{master.MasterCategoryId} not found");
                }

                context.SaveChanges();
            }
        }

        private string GetCategoryName(int masterCategoryId)
        {
            return _masterCategoryCache.GetMasterCategories()
                ?.FirstOrDefault(c => c.Id == masterCategoryId)
                ?.Name
                ?.ToUpper();
        }
    }
}
