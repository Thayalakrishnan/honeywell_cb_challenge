using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class MvcVideoContext : DbContext
    {
        public MvcVideoContext (DbContextOptions<MvcVideoContext> options)
            : base(options)
        {
        }

        public DbSet<MvcMovie.Models.Video> Video { get; set; } = default!;
    }
}
