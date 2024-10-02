using AutoMapper;
using Cabinet_Prototype.Data;
using Cabinet_Prototype.DTOs;
using Cabinet_Prototype.Models;
using Microsoft.AspNetCore.Identity;
using mimistore.DAL.Configuration;
using System.Net.Mail;

namespace Cabinet_Prototype.Services
{
    public interface IUserService
    {
    }

    public class UserService : IUserService
    {
        private readonly JwtBearerTokenSettings _bearerTokenSettings;

        private readonly ApplicationDbContext _context;

        private readonly SmtpClient _smtpClient;

        private readonly UserManager<User> _userManager;
    }
}
