namespace Artist.Model
{
    using System;

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Action")]
    public class Action
    {
        [Key]
        [Column(Order = 0)]
        public long MasterId { get; set; }
        [Key]
        [Column(Order = 1)]
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime Created { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CustomerPan { get; set; }

    }
}
