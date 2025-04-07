﻿using Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Hotel.API.Extensiones
{
    public static class MigracionDeExtensiones
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
              
            var dbContext = scope.ServiceProvider.GetRequiredService<AplicacionContextoDb>();

            dbContext.Database.Migrate();
        }
    }
}
