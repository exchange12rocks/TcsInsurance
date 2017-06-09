namespace TinkoffService.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Setting
    {
        [Key]
        public int Id { get; set; }

        [StringLength(400)]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
