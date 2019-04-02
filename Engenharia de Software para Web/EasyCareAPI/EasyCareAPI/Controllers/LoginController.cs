using EasyCareAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Web.Http.Cors;


namespace EasyCareAPI.Controllers
{
   // [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private const string communicationKey = "GQDstc21ewffff2345ffffFiwDffVvVBrk";
        SecurityKey signingKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(communicationKey));
      



        ContextDB _db;
        public LoginController()
        {
            _db = new ContextDB();

        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [AllowAnonymous]
        [AcceptVerbs("POST")]
        [Route("usuario")]
        public string getAuth(string username, string pass)
        {
            JwtManager Jwt = new JwtManager();
            UserModel u = _db.Users.Where(d => d.Name == username && d.Pass == pass).FirstOrDefault();            
            return Jwt.GenerateToken(u.Id, u.Name);
        }

        [AcceptVerbs("POST")]
        [Route("usuarionovo")]
        public string setUser([FromBody] UserModel usuario)
        {

            UserModel u = _db.Users.Add(usuario);
            _db.SaveChanges();
           
            return ("User Created With credentials:" + u.Id + u.Mail + u.Name + u.Pass);
            
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [AllowAnonymous]
        [AcceptVerbs("GET")]
        [Route("exercise")]
        public string getExe(string username, string pass)
        {
            JwtManager Jwt = new JwtManager();
            UserModel u = _db.Users.Where(d => d.Name == username && d.Pass == pass).FirstOrDefault();
            return Jwt.GenerateToken(u.Id, u.Name);
        }



        [AcceptVerbs("GET")]
        [Route("exercicio")]
        public string getExercicio(string username, string krpt)
        {

            return "ok";
        }

        
    }
 
}
