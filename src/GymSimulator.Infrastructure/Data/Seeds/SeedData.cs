using GymSimulator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GymSimulator.Infrastructure.Data.Seeds
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();
            var logger = services.GetRequiredService<ILogger<SeedData>>();

            try
            {
                await context.Database.EnsureCreatedAsync();

                await SeedPlanTypesAsync(context);
                await SeedClassTypesAsync(context);
                await SeedStudentsAsync(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while seeding data.");
                throw;
            }
        }

        private static async Task SeedPlanTypesAsync(AppDbContext context)
        {
            if (await context.PlanTypes.AnyAsync()) return;

            var planTypes = new List<PlanType>
            {
                new() { Name = "Plano Mensal", Description = "Acesso à academia em horário comercial", MonthlyClassLimit = 12 },
                new() { Name = "Plano Trimestral", Description = "Acesso ilimitado + aulas em grupo", MonthlyClassLimit = 20 },
                new() { Name = "Plano Anual", Description = "Acesso total + personal trainer", MonthlyClassLimit = 30 }
            };

            await context.PlanTypes.AddRangeAsync(planTypes);
            await context.SaveChangesAsync();
        }

        private static async Task SeedClassTypesAsync(AppDbContext context)
        {
            if (await context.ClassTypes.AnyAsync()) return;

            var classTypes = new List<ClassType>
            {
                new() { Name = "Yoga", Description = "Aulas de yoga para todos os níveis" },
                new() { Name = "Pilates", Description = "Exercícios de pilates para fortalecimento" },
                new() { Name = "Spinning", Description = "Aulas de bike indoor" },
                new() { Name = "Musculação", Description = "Treino de força orientado" }
            };

            await context.ClassTypes.AddRangeAsync(classTypes);
            await context.SaveChangesAsync();
        }

        private static async Task SeedStudentsAsync(AppDbContext context)
        {
            if (await context.Students.AnyAsync()) return;

            var monthlyPlan = await context.PlanTypes.FirstAsync(p => p.Name == "Plano Mensal");

            var students = new List<Student>
            {
                new() {
                    Name = "João Silva",
                    Email = "joao.silva@email.com",
                    Phone = "11999999999",
                    PlanTypeId = monthlyPlan.Id
                },
                new() {
                    Name = "Maria Santos",
                    Email = "maria.santos@email.com",
                    Phone = "11888888888",
                    PlanTypeId = monthlyPlan.Id
                }
            };

            await context.Students.AddRangeAsync(students);
            await context.SaveChangesAsync();
        }
    }
}