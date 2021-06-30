namespace Artist.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MasterRequest")]
    public partial class MasterRequest
    {
        public int Id { get; set; }

        public long CustomerId { get; set; }

        public int MasterCategoryId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime RequestDate { get; set; }

        [Required]
        public int State { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StateChangeDate { get; set; }

        public long QuestionaryAnswerId { get; set; }
        
        public virtual MasterCategory MasterCategory { get; set; }
    }
}
