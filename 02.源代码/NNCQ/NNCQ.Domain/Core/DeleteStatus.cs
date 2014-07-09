using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Core
{
    public class DeleteStatus
    {
        public List<DeleteStatusModel> SDSM = new List<DeleteStatusModel>();

        public void Initialize(bool oStatus, string oMessage)
        {
            var item = new DeleteStatusModel();
            item.OperationStatus = oStatus;
            item.OperationMessage = oMessage;
            SDSM.Add(item);
        }


    }
    public class DeleteStatusModel
    {
        public bool OperationStatus { get; set; }
        public string OperationMessage { get; set; }
    }

    public class StatusMessageForDeleteOperation<T> where T : class
    {

        public string OperationMessage { get; set; }
        public bool CanDelete { get; set; }

        public StatusMessageForDeleteOperation(string message, List<T> collecton)
        {
            OperationMessage = message;
            if (collecton.Count > 0)
                CanDelete = false;
            else
                CanDelete = true;
        }


        public StatusMessageForDeleteOperation(string message, Expression<Func<T, bool>> expression)
        {
            OperationMessage = message;
            CanDelete = _SetCanDelete(expression);
        }

        private bool _SetCanDelete(Expression<Func<T, bool>> expression)
        {
            var dbContext = new EntityDbContext();
            var dbSet = dbContext.Set<T>();
            if (dbSet.Where(expression).ToList().Count() > 0)
                return false;
            else
                return true;
        }

        public StatusMessageForDeleteOperation(string message, int itemCount)
        {
            OperationMessage = message;
            if (itemCount > 0)
                CanDelete = false;
            else
                CanDelete = true;
        }


    }

}
