namespace Domain.Common
{
    public interface IAuditableEntity
    {
        bool IsDeleted { get; set; }
        DateTime CreatedOn { get; set; }
        int CreatedBy { get; set; }
        DateTime? LastModifiedOn { get; set; }
        int LastModifiedBy { get; set; }
    }
}