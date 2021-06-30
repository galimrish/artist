namespace Artist.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Master")]
    public partial class Master
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        public int MasterCategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Pan { get; set; }

        [Required]
        [StringLength(20)]
        public string MSISDN { get; set; }

        public bool CanBindCustomer { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public virtual MasterCategory MasterCategory { get; set; }
    }
}
