﻿namespace Domain.Common
{
    public abstract class AuditableWithBaseEntity<T> : BaseEntity<T>, IAuditableEntity
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int LastModifiedBy { get; set; }
    }
}