﻿namespace LearningSystem.Web.Infrastructure.Extensions
{
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using LearningSystem.Web.Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                                            .GetRequiredService<IServiceScopeFactory>()
                                            .CreateScope())
            {
                serviceScope
                    .ServiceProvider
                    .GetService<LearningSystemDbContext>()
                    .Database
                    .Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task
                    .Run(async () =>
                    {
                        var adminName = GlobalConstants.AdminRole;

                        var hasRoles = await roleManager.Roles.CountAsync();

                        if (hasRoles == 0)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = adminName
                            });

                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = GlobalConstants.BlogAuthor
                            });

                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = GlobalConstants.Student
                            });

                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = GlobalConstants.Trainer
                            });
                        }

                        var adminUser = await userManager.FindByNameAsync(adminName);

                        if (adminUser == null)
                        {
                            adminUser = new User
                            {
                                UserName = adminName,
                                Email = "admin@admin.admin",
                                Name = "admin",
                                BirthDate = DateTime.UtcNow
                            };

                            await userManager.CreateAsync(adminUser, "admin123");

                            await userManager.AddToRoleAsync(adminUser, adminName);
                        }
                    })
                .Wait();
            }

            return app;
        }
    }
}
