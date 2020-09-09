using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class RequisitionFile
    {
        public long Id { get; set; }
        public string FileLocation { get; set; }
        public bool IsDeleted { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadedOn { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public long AdvanceRequisitionHeaderId { get; set; }
        [NotMapped]
        public string FileName
        {
            get { return Path.GetFileName(FileLocation); }
        }

        public AdvanceRequisitionHeader AdvanceRequisitionHeader { get; set; }
    }
}
