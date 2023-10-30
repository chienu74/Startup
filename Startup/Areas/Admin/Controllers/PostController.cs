using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using startup.Models;

namespace startup.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly DataContext _context;
        public PostController(DataContext context) 
        {
            _context = context; 
        }

        public IActionResult Index()
        {
            var mnList = _context.Posts.OrderBy(m => m.PostID).ToList();
            return View(mnList);
        }

        public IActionResult Create()
        {
            var mnList = (from m in _context.Menus
                          select new SelectListItem()
                          {
                              Text = m.MenuName,
                              Value = m.MenuID.ToString()
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text= "----Select----",
                Value = string.Empty
            });
            ViewBag.mnList = mnList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                _context.SaveChanges();
            }
            return RedirectToAction("Create");
        }

        public IActionResult Edit(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var po = _context.Posts.Find(id);
            if (po == null)
            {
                return NotFound();
            }

            var polist = (from m in _context.Posts
                          select new SelectListItem()
                          {
                              Text = m.Title,
                              Value = m.PostID.ToString(),
                          }).ToList();
            polist.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.poList = polist;
            return View(po);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Post po)
        {
            if (ModelState.IsValid)
            {
                _context.Posts.Update(po);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(po);
        }

        public IActionResult Delete(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var bl = _context.Posts.Find(id);
            if (bl == null)
            {
                return NotFound();
            }
            return View(bl);
        }
        [HttpPost]
        public IActionResult Delete(long id)
        {
            var delePost = _context.Posts.Find(id);
            if (delePost == null)
            {
                return NotFound();
            }
            _context.Posts.Remove(delePost);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
