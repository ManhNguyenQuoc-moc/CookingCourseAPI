using CookingCourseAPI.Data;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Repositories;
using Microsoft.EntityFrameworkCore;

public class LearningPathRepository : ILearningPathRepository
{
    private readonly AppDbContext _context;

    public LearningPathRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LearningPath> CreateAsync(LearningPath path)
    {
        _context.LearningPaths.Add(path);
        await _context.SaveChangesAsync();
        return path;
    }

    public async Task<LearningPath> UpdateAsync(LearningPath path)
    {
        _context.LearningPaths.Update(path);
        await _context.SaveChangesAsync();
        return path;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var path = await _context.LearningPaths.FindAsync(id);
        if (path == null) return false;
        _context.LearningPaths.Remove(path);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<LearningPath> GetByIdAsync(int id)
    {
        return await _context.LearningPaths
            .Include(lp => lp.LearningPathCourses)
            .FirstOrDefaultAsync(lp => lp.Id == id);
    }

    public async Task<List<LearningPath>> GetAllAsync()
    {
        return await _context.LearningPaths
            .Include(lp => lp.LearningPathCourses)
            .ToListAsync();
    }
}
