using MonitorApplication_BL.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_BL.Commands.RegisterCommand
{
    public class SaveFileCommand : ICommand
    {
        public string UploaderName {get; set;}
        public ICollection<byte[]> formFiles {get; set;} 
    }
}
