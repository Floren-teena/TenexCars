using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Interfaces;

namespace TenexCars.DataAccess
{
    public class SeedData
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IOperatorRepository _operatorRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public SeedData(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IOperatorRepository operatorRepository,
                        IVehicleRepository vehicleRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _operatorRepository = operatorRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task SeedAsync()
        {
            await SeedRolesAsync();
            await SeedUsersOperatorVehicleAsync();

        }

        private async Task SeedRolesAsync()
        {

            string[] roles = { "Main_Subscriber", "Co_Subscriber", "Main_Operator", "Operator_Team_Member", "Tenex_Admin", "Tenex_Member" };
            foreach (var roleName in roles)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private async Task SeedUsersOperatorVehicleAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            if (!users.Any(u => _userManager.IsInRoleAsync(u, "Main_Operator").Result))
            {
                var operatorUser = new AppUser
                {
                    UserName = "alice.smith@example.com",
                    Email = "alice.smith@example.com",
                    FirstName = "Alice",
                    LastName = "Smith",
                    Type = "Main_Operator",

                };

                await _userManager.CreateAsync(operatorUser, "199026_Ll");
                await _userManager.AddToRoleAsync(operatorUser, "Main_Operator");

                var operatorUser2 = new AppUser
                {
                    UserName = "j.berg@mailinator.com",
                    Email = "j.berg@mailinator.com",
                    FirstName = "John",
                    LastName = "Bergkamp",
                    Type = "Main_Operator",

                };

                await _userManager.CreateAsync(operatorUser2, "199026_Ll");
                await _userManager.AddToRoleAsync(operatorUser2, "Main_Operator");


                var operatorUser3 = new AppUser
                {
                    UserName = "manor@mailinator.com",
                    FirstName = "Manuel",
                    LastName = "Ortega",
                    Email = "manor@mailinator.com",
                    Type = "Main_Operator",

                };

                await _userManager.CreateAsync(operatorUser3, "199026_Ll");
                await _userManager.AddToRoleAsync(operatorUser3, "Main_Operator");
            }
        }
    }
}
