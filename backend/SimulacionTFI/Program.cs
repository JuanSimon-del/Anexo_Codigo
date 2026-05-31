// Program.cs es el punto de entrada de la aplicación. Cuando ejecutas el proyecto, 
// este es el archivo que se ejecuta primero.

// importan código de otros lugares del proyecto.

using SimulacionTFI.Domain.Interfaces;
using SimulacionTFI.Infrastructure.Generations;

// namespace es como una carpeta lógica que agrupa clases relacionadas.
// Evita choques de nombres cuando hay varias clases con el mismo nombre.
// Imagina dos clases llamadas Program en dos proyectos distintos:
// SimulacionTFI.Program
// OtraApp.Program
// Si usas namespace, el compilador sabe cuál es cuál.

namespace SimulacionTFI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Crea un constructor para la aplicación web.
            // builder será usado para configurar qué hace la aplicación y qué servicios utiliza.

            var builder = WebApplication.CreateBuilder(args);

            // Activa los controladores de API. Permite responder a peticiones HTTP como POST o GET.
            builder.Services.AddControllers();
            // Ayuda a generar información automática sobre los endpoints (las rutas disponibles).
            builder.Services.AddEndpointsApiExplorer();
            // Añade Swagger, una herramienta que crea una página donde puedes probar la API.
            builder.Services.AddSwaggerGen();
            builder.Services.AddRazorPages();

            // Esto es inyección de dependencias.
            // cada vez que una parte del programa pida un IGenerator, el sistema le dará una 
            // nueva instancia de CongruencialMixto.
            // IGenerator es una interfaz (un contrato), y CongruencialMixto es la implementación 
            // concreta que genera números aleatorios.
            builder.Services.AddTransient<IGenerator, CongruencialMixto>();

            // Habilitar CORS para que el frontend pueda hacer requests desde localhost
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFront", policy =>
                    policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            // Aquí se arma la aplicación con todas las configuraciones anteriores.
            // Después de esto app ya sabe qué servicios tiene y cómo debe comportarse.
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("AllowFront");

            app.UseAuthorization();

            app.MapControllers();
            app.MapRazorPages();

            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}


// Resumen simple
// Program.cs hace dos cosas principales:

// Configura la aplicación web:

// qué servicios tiene
// cómo maneja las peticiones
// qué páginas y APIs expone
// Arranca el servidor:

// redirige la raíz a Swagger
// ejecuta todo con app.Run()