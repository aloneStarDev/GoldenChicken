using GoldenChicken.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace GoldenChicken.Controllers
{
    [Authorize(Roles = "Master")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private int id;

        public AdminController()
        {
            _db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadMedia(HttpPostedFileBase file, string Caption, string Link, int Type)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    var media = new Media
                    {
                        Caption = Caption,
                        Link = Link,
                    };
                    string _FileName = Path.GetFileName(file.FileName);
                    if (Type == 1)
                        media.type = MediaType.Photo;
                    else if (Type == 2)
                        media.type = MediaType.Video;
                    else
                        throw new ValidationException("نوع فایل مشخص شده صحیح نیست");
                    string _path = Path.Combine(Server.MapPath($"~/Content/Media/" + media.type), _FileName);
                    file.SaveAs(_path);
                    media.Path = _FileName;
                    _db.Media.Add(media);
                    _db.SaveChangesAsync();
                    TempData["msg"] = "با موفقیت بارگزاری شد";
                    return RedirectToAction("Index");
                }
                TempData["msg"] = "فایل پیدا نشد";
            }
            catch (ValidationException vex)
            {
                TempData["msg"] = vex.Message;
            }
            catch
            {
                TempData["msg"] = "خطا در هنگام ذخیره ی فایل";
            }
            return RedirectToAction("Index");
        }
        public ActionResult UpdateMedia(Media media, HttpPostedFileBase file)
        {

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult MediaItems()
        {
            var media = _db.Media.ToList();
            return Json(new { ok = true, items = media });
        }
        public ActionResult DeleteMedia(int Id)
        {
            var media = _db.Media.Where(x => x.Id == Id).FirstOrDefault();
            System.IO.File.Delete(Path.Combine(Server.MapPath($"~/Content/Media/" + media.type), media.Path));
            TempData["msg"] = "با موفقیت حذف شد";
            return RedirectToAction("Index");
        }
    }
}