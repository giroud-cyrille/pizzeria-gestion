﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzeriaAPI.Authorize;
using PizzeriaAPI.Models;
using ProjetcLibrary.Models;

namespace PizzeriaAPI.Controllers
{
    [KeyRequirement]
    [Route("api/options")]
    [ApiController]
    public class ProductOptionsController : ControllerBase
    {
        private readonly Context _context;

        public ProductOptionsController(Context context)
        {
            _context = context;
        }

        // GET: api/ProductOptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductOption>>> GetOptions()
        {
            return await _context.Options.ToListAsync();
        }

        // GET: api/ProductOptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductOption>> GetProductOption(int id)
        {
            var productOption = await _context.Options.FindAsync(id);

            if (productOption == null)
            {
                return NotFound();
            }

            return productOption;
        }

        // PUT: api/ProductOptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductOption(int id, ProductOption productOption)
        {
            if (id != productOption.Id)
            {
                return BadRequest();
            }

            _context.Entry(productOption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductOptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductOptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductOption>> PostProductOption(ProductOption productOption)
        {
            _context.Options.Add(productOption);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductOption", new { id = productOption.Id }, productOption);
        }

        // DELETE: api/ProductOptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductOption(int id)
        {
            var productOption = await _context.Options.FindAsync(id);
            if (productOption == null)
            {
                return NotFound();
            }

            _context.Options.Remove(productOption);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductOptionExists(int id)
        {
            return _context.Options.Any(e => e.Id == id);
        }
    }
}
