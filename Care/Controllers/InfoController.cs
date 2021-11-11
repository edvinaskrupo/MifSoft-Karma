using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Care.Models;
using Care.Helpers;
using System.Threading;

namespace Care.Controllers
{
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
                    Thread errorReportThread = new Thread(SetError);
                    errorReportThread.Start("The requested organisation doesn't exist.");
                    
                    return View();
                }
                else {
                    Thread errorReportThread = new Thread(ResetError);
                    errorReportThread.Start();

                    return View(org);
                }
            }
            catch {
                Thread errorReportThread = new Thread(SetError);
                errorReportThread.Start("The requested organisation ID is invalid.");
                return View();
            }
        }

        public event EventHandler<EventArgsWithErrorMessage> SetErrorEvent;
        public event EventHandler<EventArgs> ResetErrorEvent;

        public virtual void SetError(object errorMessage) {
            new PostModelError(this);
            EventHandler<EventArgsWithErrorMessage> raiseEvent = SetErrorEvent;

            if (raiseEvent != null) {
                if (errorMessage is string) {
                    raiseEvent (this, new EventArgsWithErrorMessage((string)errorMessage));
                }
                else {
                    raiseEvent (this, new EventArgsWithErrorMessage(null));
                }
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
