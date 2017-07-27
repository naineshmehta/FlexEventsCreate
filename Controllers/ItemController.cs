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
using System.Collections.Generic;
using System.Web.Mvc;
using NM.Modules.FlexEventsCreate.Components;
using NM.Modules.FlexEventsCreate.Models;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;

namespace NM.Modules.FlexEventsCreate.Controllers
{
    [DnnHandleError]
    public class ItemController : DnnController
    {
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ShowForm(FlexEventViewModel flexEvent)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files;

                if (file.Count != 0)
                {
                    //do something  
                }

                //return RedirectToDefaultRoute();
            }

            return View(flexEvent);
        }

        [ModuleAction(ControlKey = "Edit", TitleKey = "AddItem")]
        [HttpGet]
        public ActionResult ShowForm()
        {
            var flexEvent = new FlexEventViewModel();

            var locations = LocationManager.Instance.Get(722);
            locations.Insert(0, new Location {ItemId = 0, Name = "==Please select a location=="});

            //flexEvent.ModuleId = ModuleContext.ModuleId;
            //flexEvent.Locations = locations;

            var list = new List<SelectListItem>();

            foreach (var loc in locations)
            {
                var item = new SelectListItem();
                item.Text = loc.Name;
                item.Value = loc.ItemId.ToString();
                list.Add(item);
            }

            flexEvent.LocationList = list;
            flexEvent.StartDateTime = DateTime.Now;
            flexEvent.EndDateTime = DateTime.Now;

            return View(flexEvent);
        }
    }
}
