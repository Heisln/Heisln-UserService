﻿using Heisln.Car.Contract;
using Heisln.Car.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heisln.Car.Infrastructure
{
    public class BookingRepository : EntityFrameWorkRepository, IBookingRepository
    {
        public BookingRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public void Add(Booking entity)
        {
            dbContext.Bookings.Add(entity);
        }

        public Task<IEnumerable<Booking>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Booking> GetAsync(Guid id)
        {
            return await dbContext.Bookings
                .Include(booking => booking.User)
                .Include(booking => booking.Car)
                .SingleAsync(booking => booking.Id == id);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUser(Guid userId)
        {
            return await dbContext.Bookings.Where(booking => booking.User.Id == userId)
                .Include(booking => booking.User)
                .Include(booking => booking.Car)
                .ToListAsync();
        }

        public void Remove(Booking entity)
        {
            dbContext.Bookings.Remove(entity);
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Booking> entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
