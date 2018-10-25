using MonitorApplication_Models.FileModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_Models.PicturesModels
{
    public class AppFile
    {
        public int AppFileId { get; set; }
        public string OriginalUploader { get; set; }
        public byte[] Content { get; set; }
        public ICollection<UserAppFile> UserAppFiles { get; set; }
    }
}
