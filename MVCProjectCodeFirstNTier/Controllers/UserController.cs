using System.Linq;
using System.Web.Mvc;
using MVCProjectCodeFirstNTier.Models;
using NTier.BLL.Services;
using NTier.BLL.Models;
using System.Collections.Generic;
using System.Net;
using System.Data.Entity;

namespace MVCProjectCodeFirstNTier.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService = new UserService();

        public ActionResult Index()
        {
            List<UIUsers> userList = new List<UIUsers>();
            UIUsers currUser = new UIUsers();
            var bllUsers = _userService.GetAllUsers().ToList();
            foreach(var user in bllUsers)
            {
                currUser.userID = user.userID;
                currUser.username = user.username;
                currUser.email = user.email;
                userList.Add(currUser);
            }
            return View(userList);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BLLUsers bllUser = _userService.FindUser(id);
            if (bllUser == null)
            {
                return HttpNotFound();
            }
            var user = new UIUsers
            {
                userID = bllUser.userID,
                username = bllUser.username,
                email = bllUser.email,
            };
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,username,email,password,confirmedPassword,picturePath")] UIUsers uiUser)
        {
            if (ModelState.IsValid)
            {
                var user = new BLLUsers
                {
                    username = uiUser.username,
                    email = uiUser.email,
                    password = uiUser.password,
                    confirmedPassword = uiUser.confirmedPassword
                };
                _userService.Create(user);
                return RedirectToAction("Index");
            }
            return View(uiUser);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BLLUsers bllUser = _userService.FindUser(id);
            if (bllUser == null)
            {
                return HttpNotFound();
            }
            var user = new UIUsers
            {
                userID = bllUser.userID,
                username = bllUser.username,
                email = bllUser.email,
            };
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,username,email,password,confirmedPassword,picturePath")] UIUsers uiUser)
        {
            if (ModelState.IsValid)
            {
                var user = new BLLUsers
                {
                    username = uiUser.username,
                    email = uiUser.email,
                };
                _userService.EditUser(user);
                return RedirectToAction("Index");
            }
            return View(uiUser);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BLLUsers bllUser = _userService.FindUser(id);
            if (bllUser == null)
            {
                return HttpNotFound();
            }
            var user = new UIUsers
            {
                userID = bllUser.userID,
                username = bllUser.username,
                email = bllUser.email
            };
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _userService.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}