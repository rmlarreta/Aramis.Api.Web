﻿using Aramis.Api.Commons.ModelsDto.Customers;

namespace Aramis.Api.CustomersService.Interfaces
{
    public interface ICustomersService
    {
        List<OpClienteDto> GetAll();
        OpClienteDto GetById(string id);
        bool Delete(string id);
        OpClienteDto Update(OpClienteInsert entity);
        OpClienteDto Insert(OpClienteInsert entity);
    }
}