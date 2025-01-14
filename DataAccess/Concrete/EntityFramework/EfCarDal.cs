﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (var context = new RentACarContext())
            {
                var result = from p in context.Cars
                    join b in context.Brands
                        on p.BrandId equals b.Id
                    join c in context.Colors
                        on p.ColorId equals c.Id
                    select new CarDetailDto
                    {
                        Id = p.Id,
                        CarName = p.Name,
                        BrandName = b.Name,
                        BrandId = b.Id,
                        ColorName = c.Name,
                        ColorId = c.Id,
                        DailyPrice = p.DailyPrice,
                        ModelYear = p.ModelYear
                    };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}