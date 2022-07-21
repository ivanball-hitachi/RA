using Domain.Common;

namespace Domain.Main.DTO
{
    public class LegalEntityBaseDTO : AuditableDTO
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
    }

    public class LegalEntityDTO : LegalEntityBaseDTO, IBaseEntity<int>
    {
        public int Id { get; set; }
    }

    public class LegalEntityForCreationDTO : LegalEntityBaseDTO
    {
    }

    public class LegalEntityForUpdateDTO : LegalEntityBaseDTO, IBaseEntity<int>
    {
        public int Id { get; set; }
    }
}