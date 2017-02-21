/*
' Copyright (c) 2017 nainesh.mehta
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Linq;
using System.Web.Mvc;
using NM.Modules.FlexEventsCreate.Components;
using NM.Modules.FlexEventsCreate.Models;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework.JavaScriptLibraries;

namespace NM.Modules.FlexEventsCreate.Controllers
{
    [DnnHandleError]
    public class ItemController : DnnController
    {

        public ActionResult Delete(int itemId)
        {
            ItemManager.Instance.DeleteItem(itemId, ModuleContext.ModuleId);
            return RedirectToDefaultRoute();
        }

        public ActionResult Edit(int itemId = -1)
        {
            DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(CommonJs.DnnPlugins);

            var userlist = UserController.GetUsers(PortalSettings.PortalId);
            var users = from user in userlist.Cast<UserInfo>().ToList()
                        select new SelectListItem { Text = user.DisplayName, Value = user.UserID.ToString() };

            ViewBag.Users = users;

            var item = (itemId == -1)
                 ? new FlexEvent { ModuleId = ModuleContext.ModuleId }
                 : ItemManager.Instance.GetItem(itemId, ModuleContext.ModuleId);

            return View(item);
        }

        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Index(FlexEvent flexEvent)
        {
            if (flexEvent.ItemId == -1)
            {
                flexEvent.CreatedByUserId = User.UserID;
                flexEvent.CreatedOnDate = DateTime.UtcNow;
                flexEvent.LastModifiedOnDate = DateTime.UtcNow;

                ItemManager.Instance.CreateItem(flexEvent);
            }
            else
            {
                var existingItem = ItemManager.Instance.GetItem(flexEvent.ItemId, flexEvent.ModuleId);
                existingItem.LastModifiedOnDate = DateTime.UtcNow;
                existingItem.ItemName = flexEvent.ItemName;
                existingItem.Summary = flexEvent.Summary;
                
                ItemManager.Instance.UpdateItem(existingItem);
            }

            return RedirectToDefaultRoute();
        }

        [ModuleAction(ControlKey = "Edit", TitleKey = "AddItem")]
        public ActionResult ShowForm()
        {
            //var items = ItemManager.Instance.GetItem(ModuleContext.ModuleId, ModuleContext.ModuleId);
            var flexEvent = new FlexEvent();
            flexEvent.ModuleId = ModuleContext.ModuleId;

            return View(flexEvent);
        }
    }
}
