using System.Reflection;
using DeckIQ.Api.Models;
using DeckIQ.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeckIQ.Api.Data;

/* User, --> Tabela AspNetUser
 * IdentityRole<long>, --> AspNetRoles (UserRoles)
 * ... Uma para Claim, login, RolesClaim, Tokens
 */

public class AppDbContext : IdentityDbContext<User,
    IdentityRole<long>,
    long,
    IdentityUserClaim<long>,
    IdentityUserRole<long>,
    IdentityUserLogin<long>,
    IdentityRoleClaim<long>,
    IdentityUserToken<long>>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<FlashCard> FlashCards { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}