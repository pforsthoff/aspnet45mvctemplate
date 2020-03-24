using DataDictionary.ServiceModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceModel.Business
{
    public interface IMetadataManager
    {
        //IEnumerable<ModelDesc> GetWorkflowSharedModelDropdownValues(string siteCode, string userId);
        IEnumerable<Metadata> GetAllMetadataItems();
    }
}
