#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PostDocuments.Models;

namespace PostDocuments.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly DocumentsDbContext _context;

        public DocumentsController(DocumentsDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //var model = new FilesViewModel();
            //foreach (var item in Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "upload")))
            //{
            //    model.Files.Add(
            //        new FileDetails { Name = System.IO.Path.GetFileName(item), Path = item });
            //}
            return View(await _context.Documentos.ToListAsync());
        }



        public IActionResult AddOrEdit(int id = 0)
        {
            ViewBag.StatusId = new SelectList
                (
                    new Documents().ListStatus(),
                    "StatusId",
                    "Status"
                );

            if (id == 0)
                return View(new Documents());
            else
                return View(_context.Documentos.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Documents Documents, IFormFile[] files)
        {

            if (Documents.StatusId == 1) Documents.Status = "Aprovado";
            if (Documents.StatusId == 2) Documents.Status = "Pendente";
            if (Documents.StatusId == 3) Documents.Status = "Reprovado";

            ViewBag.StatusId = new SelectList
                (
                    new Documents().ListStatus(),
                    "StatusId",
                    "Status"
                );

            foreach (var file in files)
            {
                var fileName = System.IO.Path.GetFileName(file.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "upload", fileName);
                Documents.Path = filePath;
                Documents.Name = fileName;

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                using (var localFile = System.IO.File.OpenWrite(filePath))
                using (var uploadedFile = file.OpenReadStream())
                {
                    uploadedFile.CopyTo(localFile);
                }
            }
            ViewBag.Message = "Erro ao efetuar upload!";


            if (Documents.DocumentId == 0)
            {
                Documents.Date = DateTime.Now;
                _context.Add(Documents);
            }
            else
                _context.Update(Documents);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            return View(Documents);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Documents = await _context.Documentos.FindAsync(id);
            _context.Documentos.Remove(Documents);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult Upload(IFormFile[] files)
        {
            // Iterate each files
            foreach (var file in files)
            {
                // Get the file name from the browser
                var fileName = System.IO.Path.GetFileName(file.FileName);

                // Get file path to be uploaded
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "upload", fileName);

                // Check If file with same name exists and delete it
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                // Create a new local file and copy contents of uploaded file
                using (var localFile = System.IO.File.OpenWrite(filePath))
                using (var uploadedFile = file.OpenReadStream())
                {
                    uploadedFile.CopyTo(localFile);
                }
            }
            ViewBag.Message = "Files are successfully uploaded";

            // Get files from the server
            //var model = new FilesViewModel();
            //foreach (var item in Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "upload")))
            //{
            //    model.Files.Add(
            //        new FileDetails { Name = System.IO.Path.GetFileName(item), Path = item });
            //}
            return View();
        }


        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("Arquivo não disponivel");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "upload", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
    {
        {".txt", "text/plain"},
        {".pdf", "application/pdf"},
        {".doc", "application/vnd.ms-word"},
        {".docx", "application/vnd.ms-word"},
        {".xls", "application/vnd.ms-excel"},
        {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
        {".csv", "text/csv"}
    };
        }

    }
}
