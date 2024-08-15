using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.FileResponse
{
    public class FileResponseModel
    {
        public byte[] ImageBytes { get; set; }
        public string Status { get; set; }
        public string SlideStatus { get; set; }
    }

}
