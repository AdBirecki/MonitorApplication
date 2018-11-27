using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
    { /* https://docs.microsoft.com/pl-pl/aspnet/core/mvc/models/file-uploads?view=aspnetcore-2.1 TODO !!! */
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
        public async Task<IActionResult> UploadImages() {

            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType)) {
                return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
            }

            string targetFilePath = null;
            var boundary = MultipartRequestHelper.GetBoundry(MediaTypeHeaderValue.Parse(Request.ContentType), _multipartBoundaryLengthLimit);

            var reader = new MultipartReader(boundary, HttpContext.Request.Body);
            var section = await reader.ReadNextSectionAsync();

            while (section != null) {
                var hasContentDispositionHeader = ContentDispositionHeaderValue
                    .TryParse(section.ContentDisposition, out ContentDispositionHeaderValue contentDispostion);

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

            return Ok();
        }
    }
}