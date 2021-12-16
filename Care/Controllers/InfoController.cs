using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Care.Models;
using Care.Helpers;
using System.Threading;

namespace Care.Controllers
{
    [MethodLogger]
    public class InfoController : Controller
    {
        private readonly ServiceDbContext _context;
        public InfoController(ServiceDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string id)
        {
            try {
                UInt32 idInt = UInt32.Parse(id);
                PostModel org = _context.Posts.FirstOrDefault(m => m.OrgId == idInt);
                if (org == null) {
                    SetError("The requested organisation doesn't exist.");
                    
                    return View();
                }
                else {
                    ResetError();

                    return View(org);
                }
            }
            catch {
                SetError("The requested organisation ID is invalid.");
                return View();
            }
        }

        public event EventHandler<EventArgsWithErrorMessage> SetErrorEvent;
        public event EventHandler<EventArgs> ResetErrorEvent;

        public virtual void SetError(string errorMessage) {
            new PostModelError(this);
            EventHandler<EventArgsWithErrorMessage> raiseEvent = SetErrorEvent;

            if (raiseEvent != null) {
                raiseEvent (this, new EventArgsWithErrorMessage(errorMessage));
            }
        }

        public virtual void ResetError() {
            new PostModelError(this);
            EventHandler<EventArgs> raiseEvent = ResetErrorEvent;

            if (raiseEvent != null) {
                raiseEvent (this, new EventArgs());
            }
        }
    }
}
