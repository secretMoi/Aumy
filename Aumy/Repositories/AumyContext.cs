using Aumy.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace Aumy.Repositories;

public class AumyContext : DbContext
{

	public AumyContext(DbContextOptions options) : base(options)
	{
	}
	
	public DbSet<Device> Devices { get; set; }
	public DbSet<TuyaDevice> TuyaDevices { get; set; }
	public DbSet<Switch> Switchs { get; set; }
	public DbSet<Socket> Sockets { get; set; }
}