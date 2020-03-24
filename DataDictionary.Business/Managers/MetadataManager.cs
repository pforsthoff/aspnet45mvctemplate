using DataDictionary.Data.Repositories;
using DataDictionary.ServiceModel.Entities;
using ServiceModel.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataDictionary.Business.Managers
{
    public class MetadataManager : ManagerBase, IMetadataManager
    {
        readonly IRepository _repository;
        //readonly ServiceModel.Business.IRoleProviderManager _roleProvider;
        //IAppSettingsManager AppSettings { get; set; }

        public MetadataManager(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Metadata> GetAllMetadataItems()
        {
            IQueryable<Metadata> metadata= _repository.All<Metadata>();
            return metadata;
        }
        //public IEnumerable<ModelDesc> GetWorkflowSharedModelDropdownValues(string siteCode, string userId)
        //{
        //    var sitemodels = _repository.Query<SiteModel>(sm => sm.SITE_CODE == siteCode);
        //    var models = _repository.All<Model>();
        //    var userroles = _repository.Query<UserRole>(u => u.USER_ID == userId && u.SITE_CODE == siteCode);

        //    var result = (from t00 in models
        //        from sec in userroles
        //        from site in sitemodels
        //        where t00.ACTIVE_FLAG == "A"
        //              && t00.MDL_NO == site.MDL_NO
        //              && site.SITE_CODE == siteCode
        //              && t00.MDL_NO == sec.MDL_NO
        //              && sec.USER_ID == userId
        //              && sec.SITE_CODE == siteCode
        //              && sec.ROLE == "WORKPLAN"
        //        select new
        //        {
        //            MDL_NO = t00.MDL_NO,
        //            MDL_NAME = t00.MDL_NO + " " + t00.MDL_DESC
        //        }).ToList().Select(x => new ModelDesc {MDL_NO = x.MDL_NO, MDL_NAME = x.MDL_NAME});

        //    return result.GroupBy(s => s.MDL_NO).Select(group => group.First()).OrderBy(m => m.MDL_NO).ToList();
        //}
    }
}
