﻿namespace eSecimAPI.Data
{
	using eSecimAPI.Models;
	using Microsoft.EntityFrameworkCore;
	using System.Collections.Generic;

	public class ESecimDbContext : DbContext
	{
		public ESecimDbContext(DbContextOptions<ESecimDbContext> options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Election> Elections { get; set; }
		public DbSet<Citizen> Citizens { get; set; }
	}
}
