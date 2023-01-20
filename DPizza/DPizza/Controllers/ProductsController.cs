using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DPizza.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using DPizza.Models.Data;

namespace DPizza.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DpizzaContext _context;
        private readonly IWebHostEnvironment _environment;
        public ProductsController(DpizzaContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Products data, IFormFile UpFile)
        {
            var result = await _context.Products.FindAsync(data.ProductId);
            if (result != null)
            {
                TempData["ChkError"] = "Error";
                return View();
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
                    return View();
                }
            }

            #endregion

            await _context.Products.AddAsync(data);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.Products.FindAsync(id);

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
                //error
            }

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Edit(int id)
        {
            var result = await _context.Products.FindAsync(id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Products data, IFormFile UpFile)
        {
            #region ImageManageMent

            var path = _environment.WebRootPath + Constants.Directory;
            if (UpFile?.Length > 0)
            {
                try
                {
                    //ลบรูปภาพเดิม
                    var oldpath = _environment.WebRootPath + data.ProductImage;
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
                    return View();
                }
            }

            #endregion

            _context.Products.Update(data);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
