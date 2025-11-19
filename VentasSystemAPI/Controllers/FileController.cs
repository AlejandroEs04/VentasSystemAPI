using Microsoft.AspNetCore.Mvc;

namespace VentasSystemAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class FileController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, string folder = "uploaded")
        {
            if(file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folder);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string extension = Path.GetExtension(file.FileName);
            string fileName = $"{Guid.NewGuid()}{extension}";
            string fullPath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string baseUrl = $"{Request.Scheme}://{Request.Host}";
            string urlImage = $"{baseUrl}/images/{folder}/{fileName}";

            return Ok(new { url = urlImage, fileName });
        }
    }
}
