using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TestingPlatform.Migrations;
using TestingPlatform.Models;
using TestingPlatform.Data;

namespace TestingPlatform.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Groups : ControllerBase
{
    private readonly AppDbContext _context;

    public Groups(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var groups = await _context.Groups
            .Include(g => g.Direction)
            .Include(g => g.Course)
            .Include(g => g.Project)
            .ToListAsync();

        return Ok(groups);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var group = await _context.Groups
            .Include(g => g.Direction)
            .Include(g => g.Course)
            .Include(g => g.Project)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (group == null)
            return NotFound("Группа не найдена");

        return Ok(group);
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name, int directionId, int courseId, int projectId)
    {
        var group = new Group
        {
            Name = name,
            DirectionId = directionId,
            CourseId = courseId,
            ProjectId = projectId
        };

        _context.Groups.Add(group);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Группа создана", id = group.Id });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var group = await _context.Groups.FindAsync(id);
        if (group == null)
            return NotFound("Группа не найдена");

        _context.Groups.Remove(group);
        await _context.SaveChangesAsync();

        return Ok("Группа удалена");
    }
}
