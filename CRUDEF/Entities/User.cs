namespace CRUDEF.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class User
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public int Age { get; set; }

        public bool IsMarried { get; set; }

        public string PassportNumber { get; set; }

        public string PassportSeria { get; set; }

        public int? CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
