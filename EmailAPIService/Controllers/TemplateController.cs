using EmailAPIService.Models;
using EmailAPIService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace EmailAPIService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemplateController : ControllerBase
    {
        private readonly EmailAPIDBContext _context;

        public TemplateController(EmailAPIDBContext context)
        {
            _context = context;
        }
        //Get all templates
        [HttpGet]
        public ActionResult<IEnumerable<Template>> GetAllTemplates()
        {
            var templateList = _context.Templates.ToList();
            return templateList;
        }
        //Get a specific template
        [HttpGet("{id}")]
        public ActionResult<Template> GetTemplate(int id)
        {
            var template = _context.Templates.FirstOrDefault(t => t.Id == id);
            if (template == null)
            {
                return NotFound();
            }
            return template;
        }
        //Create a new template
        [HttpPost]
        public async Task<ActionResult<Template>> CreateTemplate(Template template)
        {
            try
            {
                _context.Templates.Add(template);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetTemplate), new { id = template.Id }, template);
        }
        //Delete a template
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTemplate(int id)
        {
            var template = _context.Templates.FirstOrDefault(t => t.Id == id);
            if (template == null)
            {
                return NotFound($"Template with ID {id} was not found.");
            }

            try
            {
                _context.Templates.Remove(template);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
        //Update a template
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTemplate(int id, Template template)
        {
            if (id != template.Id)
            {
                return BadRequest();
            }
            var existingTemplate = _context.Templates.SingleOrDefault(t => t.Id == id);
            if (existingTemplate == null)
            {
                return NotFound($"Template with ID {id} was not found.");
            }
            existingTemplate.Name = template.Name;
            existingTemplate.Subject = template.Subject;
            existingTemplate.Body = template.Body;
            try
            {
                _context.Entry(existingTemplate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
    }
}
