using BusinessObjects.Entities;
using DAO;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : GenericRepository<Order, OrderDAO>, IOrderRepository
    {

        public OrderRepository(OrderDAO orderDAO) : base(orderDAO) { 
        
        }
    }
}
