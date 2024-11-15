using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CoreWarenlager.Models;

public class Warehouse
{
    public int WarehouseID { get; set; }

    public virtual List<Product>? Products { get; set; }

    public Warehouse()
    {
        Products = new List<Product>();
    }
}