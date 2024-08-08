using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Data
{
    public class Page : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }
        [ForeignKey("ModifiedBy")]
        public User ModifiedByUser { get; set; }
        [ForeignKey("DeletedBy")]
        public User DeletedByUser { get; set; }
    }
}
