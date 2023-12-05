﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YW.HandoverMgmt.Api.DataLayer;
using YW.HandoverMgmt.Api.Model.DTO;
using YW.HandoverMgmt.Api.Model.Entity;

namespace YW.HandoverMgmt.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly YWDbContextcs _dbContext;
        public UsersController(YWDbContextcs dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet("users")]
        public async Task<List<UserReq>> GetAllUsers() 
        { 
            List<UserReq> users = new List<UserReq>();
            var allUsers = _dbContext.Contacts.Select(x => new { x.Id, x.Name, x.MobileNumber, x.Role, x.Company });
            foreach (var user in allUsers)
            {
                UserReq userReq = new UserReq();
                userReq.Id = user.Id;
                userReq.Name = user.Name;
                userReq.MobileNumber = user.MobileNumber;
                userReq.Role = user.Role;
                userReq.Company = user.Company;
                users.Add(userReq);
            }
            return users;
        }

        [HttpGet("users/{id}")]
        public async Task<UserRes> GetUserById(string id)
        {
            var result = _dbContext.Contacts.FirstOrDefault(x => x.Id==id);
            if(result != null)
            {
                return result;
            }
            return null;
        }
    }
}
