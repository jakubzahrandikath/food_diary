﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FoodDiary.Models;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using FoodDiary.Data;
using FoodDiary.Repositories.Entities;
using System.Net;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FoodDiary.Controllers
{
    public class PersonalDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        UserManager<AppUser> _userManager;

        private DbSet<UserDetailsEntity> UserDetailsEntities { get; set; }
        private readonly ILogger<HomeController> _logger;
        public PersonalDetailsController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            UserDetailsEntities = _context.UserDetailsEntities;

        }
        public async Task<IActionResult> IndexAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result_user = _context.UserDetailsEntities.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
            var result = await _userManager.FindByIdAsync(userId);
            var model = new UserDetailsEntities_view()
            {
                appUser = result,
                userDetailsEntity = result_user,
            };

            return View(model);
        }

        public class UserDetailsEntities_view
        {
            public AppUser appUser { get; set; }
            public UserDetailsEntity userDetailsEntity { get; set; }
        }
    }
}
