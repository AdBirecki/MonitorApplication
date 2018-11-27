using MonitorApplication_Models.PicturesModels;
using MonitorApplication_Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_Models.FileModels
{
    // Linking table
    public class UserAppFile
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int FileId { get; set; }
        public AppFile UploadedFile { get; set; }
    }
}
