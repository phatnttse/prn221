﻿using BusinessObjects.Entities;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order, OrderDAO>
    {
    }
}
