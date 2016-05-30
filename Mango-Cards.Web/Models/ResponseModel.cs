using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango_Cards.Web.Models
{
    public class ResponseModel
    {
        public int Id { get; set; }
        public int ErrorCode { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
        public string DebugMessage { get; set; }
    }
    public class UploadResponseModel
    {
        public int Id { get; set; }
        public int ErrorCode { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
        public string DebugMessage { get; set; }
        public string OriginalFileName { get; set; }
        public Guid FileId { get; set; }
    }
}
