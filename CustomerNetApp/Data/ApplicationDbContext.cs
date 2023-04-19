using System;
using CustomerNetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerNetApp.Data
{
	public class ApplicationDbContext: DbContext
	{
		public ApplicationDbContext(DbContextOptions options): base(options)
		{
		}

		public DbSet<Customer> Customers { get; set; }
	}
}

