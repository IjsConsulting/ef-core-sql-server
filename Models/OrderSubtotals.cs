using System;
using System.Collections.Generic;

namespace ef_core_sql_server.Models
{
    public partial class OrderSubtotals
    {
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
