using System;
namespace WebApp1.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Test
    {
        [Key]
        public string TestId {get; set;}
    }
}
