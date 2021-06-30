namespace Artist.Model
{
    using System;

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ActionHistory")]
    public class ActionHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RecordId { get; set; }

        public long MasterId { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime Created { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime? Deleted { get; set; }
        public string CustomerPan { get; set; }
    }
}
