using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRental.Business.Models;
using CarRental.Business.Models.User;
using CarRental.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Business.Services
{
    public interface IUserService
    {
       public Task<IdentityResult> CreateAsync(RegisterModel model);

    }
}