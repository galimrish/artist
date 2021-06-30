using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Shouldly;

namespace Artist.TestClient
{
    [TestClass]
    public class ArtistTest
    {
        private static Fixture _fixture = new Fixture();

        [TestInitialize]
        public void Init()
        {
            _fixture.Customizations.Add(new TypeRelay(typeof(IMasterRequest), typeof(MasterCategory)));
        }

        [TestMethod]
        public void GetMasterCategories()
        {
            using (var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                var categories = artistService.GetMasterCategories();
            }
        }

        [TestMethod]
        public void GetMasterCategory()
        {
            using (var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                var categories = artistService.GetMasterCategory("makeuper");
            }
        }

        [TestMethod]
        public void GetMasterRequestsByCustomerId()
        {
            using (var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                var requests = artistService.GetMasterRequests(123);
            }
        }

        [TestMethod]
        public void GetLastMasterRequest()
        {
            using (var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                var request = artistService.GetLastMasterRequest(123);
            }
        }

        [TestMethod]
        public void CreaterRequest()
        {
            using (var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                artistService.CreateMasterRequest(new MasterRequest
                {
                    CustomerId = 1423,
                    MasterCategoryId = 1,
                    QuestionaryAnswerId = 14,
                    RequestDate = DateTime.UtcNow,
                    StateChangeDate = DateTime.UtcNow,
                    State = 1
                });
            }
        }

        [TestMethod]
        public void UpdateMasterRequest()
        {
            using (var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                artistService.UpdateMasterRequest(new MasterRequest
                {
                    Id = 1,
                    CustomerId = 123,
                    MasterCategoryId = 1,
                    QuestionaryAnswerId = 1,
                    RequestDate = new DateTime(2018, 1, 26, 10, 22, 03),
                    StateChangeDate = DateTime.UtcNow,
                    State = 1
                });
            }
        }

        [TestMethod]
        public void CreateMasterRequestComment()
        {
            using (var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                artistService.CreateMasterRequestComment(new MasterRequestComment
                {
                    Author = null,
                    Date = DateTime.Now,
                    MasterRequestId = 1,
                    Message = "What is going on?"
                });
            }
        }

        [TestMethod]
        public void GetMasterRequests()
        {
            using (var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                artistService.GetMasterRequests();
            }
        }

        [TestMethod]
        public void GetMasterRequestsWithFilter()
        {
            using (var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                artistService.GetMasterRequests(new MasterRequestFilter
                {
                    CategoryIds = new int[] {1,2},
                    StateIds = new int[] {1,2}
                });
            }
        }

        [TestMethod]
        public void GetMasterRequestComments()
        {
            using (var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                artistService.GetMasterRequestComments(1);
            }
        }

        [TestMethod]
        public void GetMasterById()
        {
            using (var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                IMaster result  = artistService.GetMasterById(0);
                Assert.AreEqual(0, result.Id);
                Assert.AreEqual(1, result.MasterCategoryId);
                Assert.AreEqual("First Name", result.Name);
                Assert.AreEqual(true, result.CanBindCustomer);
                Assert.IsTrue(result.Created <= DateTime.UtcNow);
                Assert.AreEqual("None", result.Status);

                result = artistService.GetMasterById(31337);
                Assert.IsNull(result);
            }
        }

        [TestMethod]
        public void GetMasterByPan()
        {
            using (var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                // Check existing master
                IMaster result = artistService.GetMasterByPan("990000000001");
                Assert.AreEqual(0, result.Id);
                Assert.AreEqual(1, result.MasterCategoryId);
                Assert.AreEqual("First Name", result.Name);
                Assert.AreEqual(true, result.CanBindCustomer);
                Assert.IsTrue(result.Created <= DateTime.UtcNow);
                Assert.AreEqual("None", result.Status);

                // Check nonexistent number
                result = artistService.GetMasterByPan("12345678910");
                Assert.IsNull(result);
            }
        }

        [TestMethod]
        public void GetMasters()
        {
            using (var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                artistService.GetMasters();
            }
        }

        [TestMethod]
        public void GetMastersWithFilter()
        {
            using (var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                artistService.GetMasters(new MasterFilter
                {
                    CanBindCustomer = true,
                    CategoryIds = new int[] {1,2}
                });
            }
        }

        [TestMethod]
        public void SaveActionMasterDoesNotExist()
        {
            using (var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                try
                {
                    var action = new Artist.Action
                    {
                        MasterId = 31337,
                        CustomerId = 45678,
                        CustomerName = "Some Name",
                        CustomerPan = "990000000333",
                        Created = DateTime.UtcNow,
                        ExpirationDate = DateTime.UtcNow.AddMonths(3)
                    };

                    artistService.SaveAction(action);
                }
                catch(FaultException ex)
                {
                    Assert.AreEqual("31337 not found", ex.Message);
                    return;
                }

                Assert.Fail("Exception has been expected");
            }
        }

        [TestMethod]
        public void SaveAction()
        {
            using (var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                int masterId = 0;
                int customerId = 45678;
                string customerName = "Some Name";
                string customerPan = "990000000333";

                var action = new Artist.Action
                {
                    MasterId = masterId,
                    CustomerId = customerId,
                    CustomerName = customerName,
                    CustomerPan = customerPan,
                    Created = DateTime.UtcNow,
                    ExpirationDate = DateTime.UtcNow.AddMonths(3)
                };

                artistService.SaveAction(action);

                var customersInfo = artistService.GetMasterCustomersInfo(masterId);
                var actInfo = customersInfo.First(ci => ci.ClientId == customerId);
                Assert.AreEqual(customerName, actInfo.Name);
                Assert.AreEqual(customerPan, actInfo.Pan);
            }            
        }

        [TestMethod]
        public void CreateMaster()
        {
            using(var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                artistService.CreateMaster(new Master
                {
                    CanBindCustomer = true,
                    Created = DateTime.UtcNow,
                    Id = 999,
                    MasterCategoryId = 1,
                    MSISDN = "72345678901",
                    Name = "Last First Patronymic",
                    Pan = "990000009012"
                });
            }
        }

        [TestMethod]
        public void UpdateMasterBindProperty()
        {
            using (var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                var master = artistService.GetMasterById(2);
                master.CanBindCustomer = false;

                artistService.UpdateMasterBindProperty(master);
            }
        }

        [TestMethod]
        public void DeleteMaster()
        {
            using (var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                artistService.DeleteMaster(999);
            }
        }

        [TestMethod]
        public void DeleteAllMasters()
        {
            using(var channel = new ChannelFactory<IArtistService>("ArtistServiceEndpoint"))
            {
                var artistService = channel.CreateChannel();

                artistService.DeleteAllMasters();
            }
        }
    }
}
