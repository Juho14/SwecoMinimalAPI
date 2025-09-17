using Entities;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Domain
{
    public static class ProjectEndpoints
    {
        public static void MapProjectEndpoints(this WebApplication app)
        {
            // CREATE
            app.MapPost("/projects", async (AddProjectDTO dto, ProjectDbContext db) =>
            {
                var entity = new ProjectEntity
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    ProjectManager = dto.ProjectManager,
                    Client = dto.Client,
                    Options = dto.Options
                };

                db.Projects.Add(entity);
                await db.SaveChangesAsync();

                var result = new ProjectDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    ProjectManager = entity.ProjectManager,
                    Client = entity.Client,
                    Options = entity.Options
                };

                return Results.Created($"/projects/{result.Id}", result);
            });

            // GET ALL
            app.MapGet("/projects", async (ProjectDbContext db) =>
            {
                var projects = await db.Projects
                    .Select(p => new ProjectDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        ProjectManager = p.ProjectManager,
                        Client = p.Client,
                        Options = p.Options
                    })
                    .ToListAsync();

                return Results.Ok(projects);
            });

            // GET BY ID
            app.MapGet("/projects/{id:long}", async (long id, ProjectDbContext db) =>
            {
                var project = await db.Projects
                    .Where(p => p.Id == id)
                    .Select(p => new ProjectDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        ProjectManager = p.ProjectManager,
                        Client = p.Client,
                        Options = p.Options
                    })
                    .FirstOrDefaultAsync();

                return project is not null ? Results.Ok(project) : Results.NotFound();
            });

            // UPDATE BY ID
            app.MapPut("/projects/{id:long}", async (long id, ProjectDto dto, ProjectDbContext db) =>
            {
                var project = await db.Projects.FindAsync(id);
                if (project is null) return Results.NotFound();

                project.Name = dto.Name;
                project.Description = dto.Description;
                project.ProjectManager = dto.ProjectManager;
                project.Client = dto.Client;
                project.Options = dto.Options;

                await db.SaveChangesAsync();

                var result = new ProjectDto
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    ProjectManager = project.ProjectManager,
                    Client = project.Client,
                    Options = project.Options
                };

                return Results.Ok(result);
            });

            // DELETE BY ID
            app.MapDelete("/projects/{id:long}", async (long id, ProjectDbContext db) =>
            {
                var project = await db.Projects.FindAsync(id);
                if (project is null) return Results.NotFound();

                db.Projects.Remove(project);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
