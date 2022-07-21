using Domain.Common;

namespace Domain.Main.DTO
{
    public class ProjectBaseDTO : AuditableDTO
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
    }

    public class ProjectDTO : ProjectBaseDTO, IBaseEntity<int>
    {
        public int Id { get; set; }
    }

    public class ProjectForCreationDTO : ProjectBaseDTO
    {
    }

    public class ProjectForUpdateDTO : ProjectBaseDTO, IBaseEntity<int>
    {
        public int Id { get; set; }
    }
}