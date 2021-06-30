namespace Artist.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MasterRequestComment")]
    public partial class MasterRequestComment
    {
        public int Id { get; set; }

        public int MasterRequestId { get; set; }

        [StringLength(100)]
        public string Author { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
