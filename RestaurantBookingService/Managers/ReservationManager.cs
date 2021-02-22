using Microsoft.EntityFrameworkCore;
using RestaurantBookingService.DataAccess.Models;
using RestaurantBookingService.Exceptions;
using RestaurantBookingService.Managers;
using RestaurantBookingService.Managers.Interfaces;
using RestaurantBookingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantBookingService.Managers
{
    public class ReservationManager : BaseManager, IReservationManager
    {
        public async Task<string> BookReservationAsync(BookReservationCriteria criteria, CancellationToken cancellationToken)
        {
            await CheckOverBookedUser(criteria.UserIds, cancellationToken);

            // if users are not overbooked, then we create the reservation
            // first we need to find a table in the selected restaurant
            var tables = _dbContext.GetAvailableTablesByRestaurantIds(new List<long> { criteria.RestaurantId }, criteria.UserIds.Count);

            if (tables == null || !tables.Any())
            {
                throw new TableNotFoundException("There are not available tables at the moment. Please try again later.");
            }

            var reservation = new Reservation
            {
                DateTime = DateTime.Now,
                MarkForDelete = false,
                RestaurantId = criteria.RestaurantId,
                TableId = tables.FirstOrDefault().Id
            };

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Reservations.Add(reservation);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    var affectedUsers = await _dbContext.Users
                        .Where(user => criteria.UserIds.Contains(user.Id))
                        .ToListAsync(cancellationToken);

                    foreach(var user in affectedUsers)
                    {
                        user.ReservationId = reservation.Id;
                    }

                    _dbContext.UpdateRange(affectedUsers);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

            return string.Format("Reservation at restaurant {0} at {1} was successfully booked. Your reservation id is {3}, if you want to cancel, you'll need it :)", reservation.RestaurantId, reservation.DateTime, reservation.Id);
        }

        public async Task<string> DeleteReservationAsync(long reservationId, CancellationToken cancellationToken)
        {
            var reservation = await CheckExistingReservation(reservationId, cancellationToken);
            var users = await _dbContext.Users.Where(user => user.ReservationId == reservation.Id).ToListAsync(cancellationToken);
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var user in users)
                    {
                        user.ReservationId = null;
                    }

                    _dbContext.UpdateRange(users);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    reservation.MarkForDelete = true;
                    _dbContext.Update(reservation);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

            return string.Format("Reservation {0} was correctly deleted.", reservation.Id);
        }

        private async Task CheckOverBookedUser(List<long> userIds, CancellationToken cancellationToken)
        {
            var usersWithReservation = await _dbContext.Users
                .Where(user => userIds.Contains(user.Id) && user.ReservationId != null).ToListAsync(cancellationToken);
            if (usersWithReservation != null && usersWithReservation.Any())
            {
                // there are existing reservations, but they are probably finished, since they just last 2 hours
                var reservations = _dbContext.Reservations.Where(r => usersWithReservation.Select(ur => ur.ReservationId).Contains(r.Id));
                foreach (var reservation in reservations)
                {
                    if (reservation.DateTime.HasValue && (DateTime.Now - reservation.DateTime.Value).TotalHours < 2)
                    {
                        var message = string.Format("The user {0} already has a reservation in restaurant {1}.",
                            usersWithReservation.First().Name, reservation.RestaurantId);
                        throw new UserOverBookedException(message);
                    }
                }
            }
        }

        private async Task<Reservation> CheckExistingReservation(long reservationId, CancellationToken cancellationToken)
        {
            var reservation = await _dbContext.Reservations
                .Where(res => res.MarkForDelete == false && res.Id == reservationId)
                .FirstOrDefaultAsync(cancellationToken);

            if (reservation == null)
            {
                var message = string.Format("Reservation {0} does not exist.", reservationId);
                throw new ReservationNotFoundException(message);
            }

            return reservation;
        }
    }
}