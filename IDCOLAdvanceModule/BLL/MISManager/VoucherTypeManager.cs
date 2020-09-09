using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Accounts.NerdCastle.Utility;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IMISRepository;
using IDCOLAdvanceModule.Repository.MISRepository;

namespace IDCOLAdvanceModule.BLL.MISManager
{
    public class VoucherTypeManager:IVoucherTypeManager
    {
        private IVoucherTypeRepository _repository;

        public VoucherTypeManager()
        {
            _repository = new VoucherTypeRepository();
        }
        public ICollection<Accounts_VoucherTypes> GetAll(params Expression<Func<Accounts_VoucherTypes, object>>[] includes)
        {
            return _repository.GetAll(includes);
        }

        public ICollection<Accounts_VoucherTypes> Get(Expression<Func<Accounts_VoucherTypes, bool>> predicate, params Expression<Func<Accounts_VoucherTypes, object>>[] includes)
        {
            return _repository.Get(predicate, includes);
        }

        public Accounts_VoucherTypes GetFirstOrDefaultBy(Expression<Func<Accounts_VoucherTypes, bool>> predicate, params Expression<Func<Accounts_VoucherTypes, object>>[] includes)
        {
            return _repository.GetFirstOrDefaultBy(predicate, includes);
        }

        public ICollection<Accounts_VoucherTypes> GetOperationalTypes()
        {
            var voucherTypeIdList = new List<int>()
            {
                (int) VoucherTypeEnum.CreditVoucher, (int) VoucherTypeEnum.DebitVoucher, (int) VoucherTypeEnum.JournalVoucher
            };
            return Get(c=>voucherTypeIdList.Contains(c.VouTypeId)).ToList();
        }
    }
}
