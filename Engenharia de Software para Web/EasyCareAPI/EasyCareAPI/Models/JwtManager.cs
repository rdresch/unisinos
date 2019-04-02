﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
namespace EasyCareAPI.Models
{
    public class JwtManager
    {
        public string GenerateToken(int idUser, string username, int expireMinutes = 480)
        {

            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            //DateTime expires = DateTime.UtcNow.AddDays(7);
            DateTime expires = DateTime.UtcNow.AddMinutes(expireMinutes);

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.NameIdentifier, idUser.ToString())
        });

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(
                        issuer: "EASY",
                        audience: "EASY",
                        subject: claimsIdentity,
                        notBefore: issuedAt,
                        expires: expires,
                        signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;

        }

        public static int getUserIdFromClaim()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var sid = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                               .Select(c => c.Value).SingleOrDefault();
            var idUsuario = 0;
            Int32.TryParse(sid, out idUsuario);

            return idUsuario;
        }

    }
}