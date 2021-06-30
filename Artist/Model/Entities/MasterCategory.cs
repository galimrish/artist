namespace Artist.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MasterCategory")]
    public partial class MasterCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MasterCategory()
        {
            Master = new HashSet<Master>();
            MasterRequest = new HashSet<MasterRequest>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string Description { get; set; }

        [Required]
        [StringLength(300)]
        public string MasterOfferUri { get; set; }

        public string MasterOfferText { get; set; }

        [Required]
        [StringLength(300)]
        public string CustomerOfferUri { get; set; }

        public string CustomerOfferText { get; set; }

        public int? QuestionaryId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Master> Master { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MasterRequest> MasterRequest { get; set; }
    }
}
