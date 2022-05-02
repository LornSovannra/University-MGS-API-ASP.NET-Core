using Microsoft.AspNetCore.Mvc;
using University_MGS_API.Models;

namespace University_MGS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolderController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FolderController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index(string folderName)
        {
            string[] filePaths = Directory.GetFiles(Path.Combine(_webHostEnvironment.WebRootPath, "Uploads/" + folderName));

            List<Folder> folders = new List<Folder>();

            foreach (string file in filePaths)
            {
                folders.Add(new Folder { FolderName = Path.GetFileName(file) });
            }

            return Ok(folders);
        }

        [HttpPost]
        public IActionResult CreateFolder(string folderName)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;

            string folder = Path.Combine(webRootPath, "Uploads/" + folderName);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);

                return Ok("The folder " + folderName + " is created succefully!");
            }
            else
            {
                return BadRequest("The folder " + folderName + " is already existed!");
            }
        }
    }
}
