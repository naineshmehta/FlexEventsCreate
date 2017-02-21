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

using System.Collections.Generic;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using NM.Modules.FlexEventsCreate.Models;

namespace NM.Modules.FlexEventsCreate.Components
{
    interface IItemManager
    {
        void CreateItem(FlexEvent t);
        void DeleteItem(int itemId, int moduleId);
        void DeleteItem(FlexEvent t);
        IEnumerable<FlexEvent> GetItems(int moduleId);
        FlexEvent GetItem(int itemId, int moduleId);
        void UpdateItem(FlexEvent t);
    }

    class ItemManager : ServiceLocator<IItemManager, ItemManager>, IItemManager
    {
        public void CreateItem(FlexEvent t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<FlexEvent>();
                rep.Insert(t);
            }
        }

        public void DeleteItem(int itemId, int moduleId)
        {
            var t = GetItem(itemId, moduleId);
            DeleteItem(t);
        }

        public void DeleteItem(FlexEvent t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<FlexEvent>();
                rep.Delete(t);
            }
        }

        public IEnumerable<FlexEvent> GetItems(int moduleId)
        {
            IEnumerable<FlexEvent> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<FlexEvent>();
                t = rep.Get(moduleId);
            }
            return t;
        }

        public FlexEvent GetItem(int itemId, int moduleId)
        {
            FlexEvent t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<FlexEvent>();
                t = rep.GetById(itemId, moduleId);
            }
            return t;
        }

        public void UpdateItem(FlexEvent t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<FlexEvent>();
                rep.Update(t);
            }
        }

        protected override System.Func<IItemManager> GetFactory()
        {
            return () => new ItemManager();
        }
    }
}
