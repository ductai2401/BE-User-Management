using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Data
{
    public abstract class BaseEntity
    {
        private DateTime _createdDate;
        public DateTime CreatedDate
        {
            get => _createdDate.ToLocalTime();
            set => _createdDate = value.ToLocalTime();
        }
        public Guid CreatedBy { get; set; }

        private DateTime _modifiedDate;
        public DateTime ModifiedDate
        {
            get => _modifiedDate.ToLocalTime();
            set => _modifiedDate = value.ToLocalTime();
        }
        public Guid ModifiedBy { get; set; }
        private DateTime? _deletedDate;
        public DateTime? DeletedDate
        {
            get => _deletedDate?.ToLocalTime();
            set => _deletedDate = value?.ToLocalTime();
        }
        public Guid? DeletedBy { get; set; }
        [NotMapped]
        public ObjectState ObjectState { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
