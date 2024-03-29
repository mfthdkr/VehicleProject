﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VehicleProject.CoreLayer.DTOs.Response;
using VehicleProject.CoreLayer.Repositories;
using VehicleProject.CoreLayer.Services.Abstract;
using VehicleProject.CoreLayer.UnitOfWork;
using VehicleProject.ServiceLayer.Mapping;

namespace VehicleProject.ServiceLayer.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto>
        where TEntity : class 
        where TDto : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IGenericRepository<TEntity> _genericRepository;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
        }

        public async Task<Response<TDto>> AddAsync(TDto dto)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(dto);

            await _genericRepository.AddAsync(newEntity);

            await _unitOfWork.CommmitAsync();

            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);

            return Response<TDto>.Success(newDto, StatusCodes.Status200OK);
        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var products = ObjectMapper.Mapper.Map<List<TDto>>(await _genericRepository.GetAllAsync());

            return Response<IEnumerable<TDto>>.Success(products, StatusCodes.Status200OK);
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var product = await _genericRepository.GetByIdAsync(id);

            if (product == null)
            {
                return Response<TDto>.Fail("Id not found", StatusCodes.Status404NotFound, true);
            }

            return Response<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(product), StatusCodes.Status200OK);
        }

        public async Task<Response<NoDataDto>> Remove(int id)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(id);

            if (isExistEntity == null)
            {
                return Response<NoDataDto>.Fail("Id not found", StatusCodes.Status404NotFound, true);
            }

            _genericRepository.Remove(isExistEntity);

            await _unitOfWork.CommmitAsync();

            return Response<NoDataDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<Response<NoDataDto>> Update(TDto dto, int id)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(id);

            if (isExistEntity == null)
            {
                return Response<NoDataDto>.Fail("Id not found", StatusCodes.Status404NotFound, true);
            }

            isExistEntity = ObjectMapper.Mapper.Map(dto,isExistEntity);

            _genericRepository.Update(isExistEntity);

            await _unitOfWork.CommmitAsync();

            return Response<NoDataDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _genericRepository.Where(predicate);

            return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()), StatusCodes.Status200OK);
        }
    }
}
