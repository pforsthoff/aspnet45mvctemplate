using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataDictionaryOverlay.App_Start
{
    public interface IStartupTask
    {
        void Configure();
    }
}