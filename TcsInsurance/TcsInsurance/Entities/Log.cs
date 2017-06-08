namespace TcsInsurance.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Log
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public DateTime DateTime { get; set; }

        public string Input { get; set; }

        public string Output { get; set; }

        public string Exception { get; set; }
    }
}
