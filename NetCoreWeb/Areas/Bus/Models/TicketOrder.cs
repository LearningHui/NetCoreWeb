using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Areas.Bus.Models
{
    public class TicketOrder
    {
        [BindNever]
        public int OrderID { get; set; }
        [BindNever]
        public ICollection<TicketCartLine> Lines { get; set; }
        [BindNever]
        public bool Paid { get; set; }//是否已经付款
        [Required(ErrorMessage = "请输入联系电话")]
        public string Phone { get; set; }
        public string Remarks { get; set; }//备注信息（选填）
    }
}
