using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyCareAPI.Models
{
    [Table("users", Schema = "public")]
    public class UserModel
    {

        [Column("name")]
        public string Name { get; set; }

       
        [Column("mail")]
        public string Mail { get; set; }

        [Column("pass")]
        public string Pass { get; set; }

        [Column("age")]
        public int Age { get; set; }

        [Column("heartTroubles")]
        public bool Heart { get; set;}

        [Column("articulationTroubles")]
        public bool Art { get; set; }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
    }
}