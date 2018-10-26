using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using MonitorApplication.Controllers.BaseControllers;
using MonitorApplication_BL.Commands.Interfaces;
using MonitorApplication_BL.Commands.RegisterCommand;
using MonitorApplication_BL.Queries.Interfaces;
using MonitorApplication_Utilities.Helpers;

namespace MonitorApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ApiBaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ILoggerFactory _loggerFactory;
        private const int _multipartBoundaryLengthLimit = int.MaxValue;
        public FileController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDisaptcher, ILoggerFactory loggerFactory)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDisaptcher;
            _loggerFactory = loggerFactory;
        }

        [HttpPost]
        public IActionResult PostFile([FromBody] SaveFileCommand command)
        {
            _commandDispatcher.Execute(command);
            return Ok("Ok");
        }

        [HttpPost]
        public async Task<IActionResult> UploadImages() {

            if(!MultipartRequestHelper.IsMultipartContentType(Request.ContentType)) {
                return BadRequest($"Expected a multipart request, bu got {Request.ContentType}");
            }
            var formAccumulator = new KeyValueAccumulator();
            string targetFilePath = null;

            var boundary = MultipartRequestHelper.GetBoundry(MediaTypeHeaderValue.Parse(Request.ContentType), _multipartBoundaryLengthLimit);

            var reader = new MultipartReader(boundary, HttpContext.Request.Body);
            var section = await reader.ReadNextSectionAsync();

            while (section != null) {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out ContentDispositionHeaderValue contentDispostion);
                if(hasContentDispositionHeader)
                {
                    if(MultipartRequestHelper.HasFileContentDisposition(contentDispostion))
                    {
                        targetFilePath = Path.GetTempFileName();
                        using (var targetStream = System.IO.File.Create(targetFilePath))
                        {
                            await section.Body.CopyToAsync(targetStream);
                        }
                    }
                }
            }

            return Ok("");
        }
        
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> PostFormFileAsync(List<IFormFile> files) {
            long size = files.Sum(f => f.Length);
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            return Ok(new { count = files.Count, size, filePath });
        }
    }
}