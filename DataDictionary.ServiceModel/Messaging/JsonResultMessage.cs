using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.ServiceModel.Messaging
{
    public class JsonResultMessage
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public static class ResultMessages
    {
        public static JsonResultMessage JsonResultMessage()
        {
            return new JsonResultMessage
            {
                Success = true,
                Message = ""
            };
        }
    }
}
