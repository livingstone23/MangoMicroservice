﻿using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Mango.Services.CouponAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize] // Uncomment this line to enable authorization
public class CouponController : ControllerBase
{


    private readonly AppDbContext _db;
    private ResponseDto _response;
    private IMapper _mapper;

    public CouponController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _response = new ResponseDto();
        _mapper = mapper;
    }


    [HttpGet]
    public ResponseDto Get()
    {
        try
        {
            IEnumerable<Coupon> objList = _db.Coupons.ToList();
            _response.Result = _mapper.Map<List<CouponDto>>(objList);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }


    [HttpGet]
    [Route("{id:int}")]
    public ResponseDto Get(int id)
    {
        try
        {

            Coupon obj = _db.Coupons.First(cu => cu.CouponId == id);
            _response.Result = _mapper.Map<CouponDto>(obj);
 
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }


    [HttpGet]
    [Route("GetByCode/{code}")]
    public ResponseDto GetByCode(string code)
    {
        try
        {

            Coupon obj = _db.Coupons.FirstOrDefault(cu => cu.CouponCode.ToLower() == code.ToLower());
            if (obj == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Invalid Coupon Code";
                return _response;
            }
            
            _response.Result = _mapper.Map<CouponDto>(obj);
 
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }


    [HttpPost]
    public ResponseDto Post([FromBody]CouponDto couponDto)
    {
        try
        {

            Coupon obj = _mapper.Map<Coupon>(couponDto);
            _db.Coupons.Add(obj);
            _db.SaveChanges();
            _response.Result = _mapper.Map<CouponDto>(obj);
            if (obj == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Invalid Coupon Code";
                return _response;
            }
            
            _response.Result = _mapper.Map<CouponDto>(obj);
 
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }

    [HttpPut]
    public ResponseDto Put([FromBody]CouponDto couponDto)
    {
        try
        {

            Coupon obj = _mapper.Map<Coupon>(couponDto);
            _db.Coupons.Update(obj);
            _db.SaveChanges();
            _response.Result = _mapper.Map<CouponDto>(obj);
            if (obj == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Invalid Coupon Code";
                return _response;
            }
            
            _response.Result = _mapper.Map<CouponDto>(obj);
 
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }

    [HttpDelete]
    [Route("{id:int}")]
    public ResponseDto Delete(int id)
    {
        try
        {
            Coupon obj = _db.Coupons.First(cu => cu.CouponId == id);
            _db.Coupons.Remove(obj);
            _db.SaveChanges();
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }




}

