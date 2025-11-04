using System;
using BCrypt.Net;

namespace WebApplication1.Utils;

public static class PasswordHasher
{
        public static string Generate(string password)
                => BCrypt.Net.BCrypt.HashPassword(password);
    
}