using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using BusinessObjects.Entities.Enums;

namespace DAO
{
    public class OrderDAO : GenericDAO<Order>
    {

        public OrderDAO(ApplicationDbContext context) : base(context)
        {

        }
    }
}
