using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UserController:BaseApiController
    {
        private readonly StoreContext _context;
        public UserController(StoreContext context){
            _context=context;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                return BadRequest("User already exists.");
            }
            CreatePasswordHash(request.Password,out byte[] passwordHash, out byte[] passwordSalt);
            var user=new User{
                Email=request.Email,
                PasswordHash=passwordHash,
                PasswordSalt=passwordSalt,
                VerificationToken=CreateRandomToken()
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();

        }
        [HttpPost("login")]
        

        public async Task<IActionResult> Login(UserLoginRequest request)
        {
           var user = await _context.Users.FirstOrDefaultAsync(u => u.Email==request.Email);
           if(user==null){
            return BadRequest("kullanıcı bulunamadı");
           }
        //    if(user.VerifiedAt==null){
        //     return BadRequest("Not verified!");
        //    }
           if(!VerifyPasswordHash(request.Password,user.PasswordHash,user.PasswordSalt)){
            return BadRequest("password is incorrect");
           }
           return Ok();

        }
        private void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt){
            using(var hmac=new HMACSHA512()){
                passwordSalt=hmac.Key;
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
        private  bool VerifyPasswordHash(string password, byte[] passwordHash,byte[] passwordSalt){
            using(var hmac=new HMACSHA512(passwordSalt)){
                var computedHash=hmac
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }

        }
        private string CreateRandomToken(){
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        
    }
}