﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TripFlip.Services.Interfaces;
using TripFlip.Services.DTO;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;

namespace TripFlip.Services
{
    /// <summary>
    /// Class that performs CRUD operations related to <see cref="RouteEntity"/>.
    /// </summary>
    public class RouteService : IRouteService
    {
        IMapper _mapper;
        FlipTripDbContext _flipTripDbContext;

        public RouteService(FlipTripDbContext flipTripDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _flipTripDbContext = flipTripDbContext;
        }

        public async Task<RouteDto> CreateAsync(RouteDto routeDto)
        {
            bool tripExists = await ValidateTripExistsAsync(routeDto.TripId);
            if (!tripExists)
            {
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }

            routeDto.DateCreated = DateTimeOffset.Now;

            RouteEntity routeEntity = _mapper.Map<RouteEntity>(routeDto);

            var entityEntry = _flipTripDbContext.Routes.Add(routeEntity);
            await _flipTripDbContext.SaveChangesAsync();

            routeDto.Id = entityEntry.Entity.Id;

            return routeDto;
        }

        public async Task<RouteDto> UpdateAsync(RouteDto routeDto)
        {
            bool routeExists = await ValidateRouteExistsAsync(routeDto.Id);
            if (!routeExists)
            {
                throw new ArgumentException(ErrorConstants.RouteNotFound);
            }

            bool tripExists = await ValidateTripExistsAsync(routeDto.TripId);
            if (!tripExists)
            {
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }

            // set original creation date
            routeDto.DateCreated = (await _flipTripDbContext
                    .Routes
                    .AsNoTracking()
                    .FirstAsync(routeEntity => routeEntity.Id == routeDto.Id)
                )
                .DateCreated;

            var routeEntity = _mapper.Map<RouteEntity>(routeDto);

            var entityEntry = _flipTripDbContext.Routes.Update(routeEntity);
            await _flipTripDbContext.SaveChangesAsync();

            routeDto.DateCreated = entityEntry.Entity.DateCreated;

            return routeDto;
        }

        public async Task<RouteDto> GetByIdAsync(int routeId)
        {
            var routeEntity = await _flipTripDbContext
                .Routes
                .AsNoTracking()
                .SingleOrDefaultAsync(routeEntity => routeEntity.Id == routeId);

            if (routeEntity == null)
            {
                throw new ArgumentException(ErrorConstants.RouteNotFound);
            }

            var routeDto = _mapper.Map<RouteDto>(routeEntity);

            return routeDto;
        }

        public async Task<List<RouteDto>> GetAllByTripAsync(int tripId)
        {
            bool tripExists = await TripExistsAsync(tripId);
            if (!tripExists)
            {
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }

            var routeEntityList = await _flipTripDbContext
                .Routes
                .AsNoTracking()
                .Where(routeEntity => routeEntity.TripId == tripId)
                .ToListAsync();

            if (routeEntityList.Count == 0)
            {
                throw new ArgumentException(ErrorConstants.RouteNotFound);
            }

            var routeDtoList = _mapper.Map<List<RouteDto>>(routeEntityList);

            return routeDtoList;
        }

        /// <summary>
        /// Checks if Trip exists by making a database query.
        /// </summary>
        /// <returns>Returns true if Trip with the given Id exists. Otherwise returns false.</returns>
        async Task<bool> ValidateTripExistsAsync(int tripId)
        {
            var tripEntity = await _flipTripDbContext.Trips
                .AsNoTracking()
                .SingleOrDefaultAsync(tripEntity => tripId == tripEntity.Id);

            return tripEntity != null;
        }

        /// <summary>
        /// Checks if Route exists by making a database query.
        /// </summary>
        /// <returns>Returns true if Route with the given Id exists. Otherwise returns false.</returns>
        async Task<bool> ValidateRouteExistsAsync(int routeId)
        {
            var routeEntity = await _flipTripDbContext.Routes
                .AsNoTracking()
                .SingleOrDefaultAsync(routeEntity => routeId == routeEntity.Id);

            return routeEntity != null;
        }
    }
}
