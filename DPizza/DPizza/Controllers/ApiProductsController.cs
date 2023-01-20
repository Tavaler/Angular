using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DPizza.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using DPizza.Models.Data;

namespace DPizza.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiProductsController : ControllerBase
    {
        private readonly DpizzaContext _context;
        private readonly IWebHostEnvironment _environment;

        public ApiProductsController(DpizzaContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: /ApiProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            return await _context.Products.OrderByDescending(p => p.ProductId).ToListAsync();
        }

        // GET: /ApiProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
            var products = await _context.Products.FindAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        [HttpPost]
        public async Task<ActionResult> PostProducts([FromForm] Products data, [FromForm] IFormFile UpFile)
        {
            var result = await _context.Products.FindAsync(data.ProductId);
            if (result != null)
            {
                //return Conflict();
                return CreatedAtAction(nameof(PostProducts), new { msg = "รหัสสินค้าซ้ำ" });
            }

            #region ImageManageMent

            var path = _environment.WebRootPath + Constants.Directory;
            if (UpFile?.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    //ตัดเอาเฉพาะชื่อไฟล์
                    var fileName = data.ProductId + Constants.ProductImage;
                    if (UpFile.FileName != null)
                    {
                        fileName += UpFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
                    }

                    using (FileStream filestream =
                        System.IO.File.Create(path + fileName))
                    {
                        UpFile.CopyTo(filestream);
                        filestream.Flush();

                        data.ProductImage = Constants.Directory + fileName;
                    }
                }
                catch (Exception ex)
                {
                    return CreatedAtAction(nameof(PostProducts), ex.ToString());
                }
            }

            #endregion

            await _context.Products.AddAsync(data);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostProducts), new { msg = "OK", data });
        }

        [HttpPut]
        public async Task<IActionResult> PutProducts([FromForm] Products data, [FromForm] IFormFile UpFile)
        {
            var result = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId.Equals(data.ProductId));
            if (result == null)
            {
                return NotFound();
            }

            #region ImageManageMent

            var path = _environment.WebRootPath + Constants.Directory;
            if (UpFile?.Length > 0)
            {
                try
                {
                    //ลบรูปภาพเดิม
                    var oldpath = _environment.WebRootPath + result.ProductImage;
                    if (System.IO.File.Exists(oldpath)) System.IO.File.Delete(oldpath);
                    //

                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    //ตัดเอาเฉพาะชื่อไฟล์
                    var fileName = data.ProductId + Constants.ProductImage;
                    if (UpFile.FileName != null)
                    {
                        fileName += UpFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
                    }

                    using (FileStream filestream =
                        System.IO.File.Create(path + fileName))
                    {
                        UpFile.CopyTo(filestream);
                        filestream.Flush();

                        data.ProductImage = Constants.Directory + fileName;
                    }
                }
                catch (Exception ex)
                {
                    return CreatedAtAction(nameof(PutProducts), new { msg = ex.ToString() });
                }
            }

            #endregion

            _context.Products.Update(data);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PutProducts), new { msg = "OK", data });
        }

        // DELETE: ApiProducts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> DeleteProducts(int id)
        {
            var result = await _context.Products.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            try
            {
                //ลบรูปภาพ
                var path = _environment.WebRootPath + result.ProductImage;

                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

                _context.Products.Remove(result);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return CreatedAtAction(nameof(DeleteProducts), e.ToString());
            }

            return result;
        }

        //Get /ApiProducts/SearchProducts/aa
        [Route("SearchProducts/{keyword}")]
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Products>>> SearchProducts(string keyword)
        {
            var result = await _context.Products.Where(p => p.ProductName.Contains(keyword)).ToListAsync();
            return result;
        }

    }
}

