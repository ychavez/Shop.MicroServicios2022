using Account.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Account.Api.Context
{
    public class AccountDbcontext: IdentityDbContext<ShopUser>
    {
        public AccountDbcontext(DbContextOptions options) : base(options)
        {
        }
    }
}
