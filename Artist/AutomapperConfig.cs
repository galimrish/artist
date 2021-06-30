using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artist
{
    public class AutomapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<IMasterRequest, Model.MasterRequest>()
                    .ForMember(s => s.Id, opt => opt.Ignore())
                    .ForMember(s => s.MasterCategory, opt => opt.Ignore());
                cfg.CreateMap<Model.MasterRequest, MasterRequest>();

                cfg.CreateMap<IMasterRequestComment, Model.MasterRequestComment>();
                cfg.CreateMap<Model.MasterRequestComment, MasterRequestComment>();

                cfg.CreateMap<IAction, Model.Action>();
                cfg.CreateMap<Model.Action, Action>();

                cfg.CreateMap<IMaster, Model.Master>()
                    .ForMember(s => s.MasterCategory, opt => opt.Ignore());
                cfg.CreateMap<Model.Master, Master>();

                cfg.CreateMap<Model.MasterCategory, MasterCategory>();
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
