using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Data
{
    public class PageAction : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid ActionId { get; set; }
        public Guid PageId { get; set; }
        public Action Action { get; set; }
        public Page Page { get; set; }
        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }
        [ForeignKey("ModifiedBy")]
        public User ModifiedByUser { get; set; }
        [ForeignKey("DeletedBy")]
        public User DeletedByUser { get; set; }
    }
}
