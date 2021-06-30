using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Artist.Exceptions;
using Serilog;

namespace Artist
{
    [ExceptionSubstitutor]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ArtistService : IArtistService
    {
        private readonly Artist _artist;
        public ArtistService(Artist artist)
        {
            _artist = artist;
        }

        public IMasterCategory[] GetMasterCategories()
        {
            Log.Debug("Begin: IMasterCategory[] GetMasterCategories()");

            var categories = _artist.GetMasterCategories();
            Log.Debug("End: IMasterCategory[] GetMasterCategories() returns {@categories}", categories);

            return categories;
        }

        public IMasterCategory GetMasterCategory(string category)
        {
            Log.Debug("Begin: IMasterCategory GetMasterCategory({category})", category);

            var masterCategory = _artist.GetMasterCategory(category);
            Log.Debug("End: IMasterCategory GetMasterCategory({category}) returns {@masterCategory}", category, masterCategory);

            return masterCategory;
        }

        public void CreateMasterRequest(IMasterRequest masterRequest)
        {
            Log.Debug("Begin: void CreateMasterRequest({@masterRequest})", masterRequest);

            _artist.CreateMasterRequest(masterRequest);
            Log.Debug("End:  void CreateMasterRequest({@masterRequest})", masterRequest);
        }

        public IMasterRequest[] GetMasterRequests(long? customerId = null)
        {
            Log.Debug($"Begin: IMasterRequest[] GetMasterRequestsByCustomerId({customerId})");

            var requests = _artist.GetMasterRequests(customerId);
            Log.Debug("End: IMasterRequest[] GetMasterRequestsByCustomerId({customerId}) returns {@requests}", customerId, requests);

            return requests;

        }

        public IMasterRequest[] GetMasterRequests(IMasterRequestFilter filter)
        {
            Log.Debug("Begin: IMasterRequest[] GetMasterRequests({@filter})", filter);

            var requests = _artist.GetMasterRequests(filter);
            Log.Debug("End: IMasterRequest[] GetMasterRequests({@filter}) returns {@requests}", filter, requests);

            return requests;
        }

        public IMasterRequest GetLastMasterRequest(long customerId)
        {
            Log.Debug("Begin: IMasterRequest GetLastMasterRequest({customerId})", customerId);

            var request = _artist.GetLastMasterRequest(customerId);
            Log.Debug("End: IMasterRequest GetLastMasterRequest({customerId}) returns {@request}", customerId, request);

            return request;
        }


        public IMasterRequest GetMasterRequestById(int masterRequestId)
        {
            Log.Debug("Begin: IMasterRequest GetMasterRequestById({masterRequestId})", masterRequestId);

            var request = _artist.GetMasterRequestById(masterRequestId);
            Log.Debug("End: IMasterRequest GetMasterRequestById({masterRequestId}) returns {@request}", masterRequestId, request);

            return request;
        }


        public void DeleteMaster(long masterId, bool ignoreStatus = false)
        {
            Log.Debug($"Begin: void DeleteMaster({masterId}, {ignoreStatus})");

            _artist.DeleteMaster(masterId, ignoreStatus);
            Log.Debug($"End: void DeleteMaster({masterId}, {ignoreStatus})");
        }

        public void DeleteAllMasters()
        {
            Log.Debug("Begin: void DeleteAllMasters()");

            _artist.DeleteAllMasters();
            Log.Debug("End: void DeleteAllMasters()");
        }
        
        public void UpdateMasterRequest(IMasterRequest masterRequest)
        {
            Log.Debug("Begin: void UpdateMasterRequestByCustomerId({@masterRequest})", masterRequest);

            _artist.UpdateMasterRequest(masterRequest);
            Log.Debug("End:  void UpdateMasterRequestByCustomerId({@masterRequest})", masterRequest);
        }

        public ICustomerInfo[] GetMasterCustomersInfo(long masterId)
        {
            Log.Debug("Begin: ICustomerInfo[] GetMasterCustomersInfo(masterId)", masterId);
            ICustomerInfo[] customersInfo = _artist.GetMasterCustomersInfo(masterId);
            Log.Debug("End: ICustomerInfo[] GetMasterCustomersInfo(masterId) returns ({@customersInfo})", masterId, customersInfo);

            return customersInfo;
        }

        public IMaster[] GetMasters()
        {
            Log.Debug("Begin: IMaster[] GetMasters()");
            IMaster[] masters = _artist.GetMasters();
            Log.Debug("End: IMaster[] GetMasters() returns ({@masters})", masters);

            return masters;
        }

        public IMaster[] GetMasters(IMasterFilter filter)
        {
            Log.Debug("Begin: IMaster[] GetMasters({@filter})", filter);
            IMaster[] masters = _artist.GetMasters(filter);
            Log.Debug("End: IMaster[] GetMasters({@filter}) returns ({@masters})", filter, masters);

            return masters;
        }

        public IMaster GetMasterById(long masterId)
        {
            Log.Debug("Begin: IMaster GetMasterById(masterId)", masterId);
            IMaster master = _artist.GetMasterById(masterId);
            Log.Debug("End: IMaster GetMasterClientIds(masterId) returns ({@master})", masterId, master);

            return master;
        }

        public IMasterRequestComment[] GetMasterRequestComments(int masterRequestId)
        {
            Log.Debug("Begin: IMasterRequestComment[] GetMasterRequestComments({masterRequestId})", masterRequestId);

            var comments = _artist.GetMasterRequestComments(masterRequestId);
            Log.Debug("End:  IMasterRequestComment[] GetMasterRequestComments({masterRequestId}) returns {@comments}", masterRequestId, comments);

            return comments;
        }

        public void CreateMasterRequestComment(IMasterRequestComment comment)
        {
            Log.Debug("Begin: void CreateMasterRequestComment({@comment})", comment);

            _artist.CreateMasterRequestComment(comment);
            Log.Debug("End:  void CreateMasterRequestComment({@comment})", comment);
        }

        public IMaster GetMasterByPan(string pan)
        {
            Log.Debug("Begin: IMaster GetMasterByPan({pan})", pan);

            IMaster master = _artist.GetMasterByPan(pan);
            Log.Debug("Begin: IMaster GetMasterByPan({pan}) returns ({@master})", pan, master);

            return master;
        }

        public void SaveAction(IAction action)
        {
            Log.Debug("Begin: void SaveAction({@action})", action);

            _artist.SaveAction(action);
            Log.Debug("End:  void SaveAction({@action})", action);
        }

        public void UpdateMasterBindProperty(IMaster master)
        {
            Log.Debug("Begin: void UpdateMasterBindProperty({@master})", master);

            _artist.UpdateMasterBindProperty(master);
            Log.Debug("End: void UpdateMasterBindProperty({@master})", master);
        }

        public void CreateMaster(IMaster master)
        {
            Log.Debug("Begin: void CreateMaster({@master})", master);

            _artist.CreateMaster(master);
            Log.Debug("End: void CreateMaster({@master})", master);
        }

        public bool IsAlive()
        {
            Log.Debug("Begin: IsAlive()");

            bool isAlive = _artist.IsAlive();
            Log.Debug("End: IsAlive() returns {isAlive}", isAlive);

            return isAlive;
        }
    }
}
