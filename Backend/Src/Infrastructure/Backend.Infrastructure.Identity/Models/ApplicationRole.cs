using System;
using Microsoft.AspNetCore.Identity;

namespace Backend.Infrastructure.Identity.Models;

public class ApplicationRole(string name) : IdentityRole<Guid>(name)
{
}