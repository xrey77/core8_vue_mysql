using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core8_vue_mysql.Entities
{
    [Table("users")]
    public class User {

        [Key]
        public int Id {get; set;}

        [Column("lastname",TypeName="varchar(20)")]
        [Required]
        public string LastName {get; set;}

        [Column("firstname",TypeName="varchar(20)")]
        [Required]
        public string FirstName {get; set;}

        [Column("username",TypeName="varchar(20)")]
        [Required]
        public string UserName {get; set;}

        [Column("password",TypeName="text")]
        [Required]
        public string Password {get; set;}

        [Column("email",TypeName="varchar(100)")]
        [Required]
        public string Email { get; set; }

        [Column("mobile",TypeName="varchar(15)")]
        public string Mobile { get; set; }

        [Column("roles",TypeName="varchar(10)")]
        public string Roles { get; set; }

        [Column("isactivated")]
        public int Isactivated {get; set;}

        [Column("isblocked")]
        public int Isblocked {get; set;}

        [Column("mailtoken")]
        public int Mailtoken {get; set;}

        [Column("qrcodeurl",TypeName="text")]
        public string Qrcodeurl {get; set;}

        [Column("profilepic",TypeName="varchar(100)")]
        public string Profilepic {get; set;}

        [Column("secretkey",TypeName="text")]
        public string Secretkey  {get; set;}

        [Column("createdAt",TypeName="timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("getutcdate()")]        
        public DateTime CreatedAt  {get; set;}

        [Column("updatedAt",TypeName="timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("getutcdate()")]
        public DateTime UpdatedAt  {get; set;}

    }
}