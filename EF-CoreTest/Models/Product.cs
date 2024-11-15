using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CoreTest.Models;

public class Product
{
    public int ProductID { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    [Required]
    public virtual Category Category { get; set; }
}