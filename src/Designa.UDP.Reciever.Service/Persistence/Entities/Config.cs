using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Designa.UDP.Reciever.Service.Persistence.Entities
{
    [Table("configs")]
    public class Config
    {
        [Key]
        public int Id { get; set; }
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
    }
}
