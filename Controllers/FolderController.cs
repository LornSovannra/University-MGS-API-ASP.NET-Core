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

        [HttpGet("View")]
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

        [HttpPost("Create")]
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

        [HttpPut("Move")]
        public IActionResult MoveFolder(string folderName, string newFolderName)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;

            string folder = Path.Combine(webRootPath, "Uploads/" + folderName);
            string newFolder = Path.Combine(webRootPath, "Uploads/" + newFolderName);

            if (Directory.Exists(folder))
            {
                if(Directory.Exists(newFolderName))
                {
                    Directory.Move(folder, newFolder);
                }
            }
            else
            {
                return BadRequest("The " + newFolderName + " folder isn't exist!");
            }

            return Ok("The folder " + folderName + " moved to " + newFolderName + " Successfully!");
        }

        [HttpPut("Rename")]
        public IActionResult RenameFolder(string folderName, string newFolderName)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;

            string folder = Path.Combine(webRootPath, "Uploads/" + folderName);
            string newFolder = Path.Combine(webRootPath, "Uploads/" + newFolderName);


            if (Directory.Exists(folder))
            {
                if(!Directory.Exists(newFolder))
                    Directory.Move(folder, newFolder);
                else
                    return BadRequest("The " + newFolderName + " folder is already existed!");

                return Ok("The folder " + folderName + " changed to " + newFolderName + " Successfully!");
            }
            else
            {
                return BadRequest("The " + newFolderName + " folder is already existed!");
            }
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteFolder(string folderName)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;

            string folder = Path.Combine(webRootPath, "Uploads/" + folderName);

            if (Directory.Exists(folder))
            {
                Directory.Delete(folder);

                return Ok("The folder " + folderName + " is deleted succefully!");
            }
            else
            {
                return BadRequest("The folder " + folderName + " isn't exist!");
            }
        }
    }
}
