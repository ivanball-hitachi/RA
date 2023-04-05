//using Domain.Common;

//namespace Domain.Main.DTO
//{
//    public class CustomerBaseDTO : AuditableDTO
//    {
//        public string Name { get; set; } = default!;
//        public string CustomerAccount { get; set; } = default!;
//        public string? AccountNumber { get; set; } = default!;
//    }

//    public class CustomerDTO : CustomerBaseDTO, IBaseEntity<int>
//    {
//        public int Id { get; set; }
//    }

//    public class CustomerForCreationDTO : CustomerBaseDTO
//    {
//    }

//    public class CustomerForUpdateDTO : CustomerBaseDTO, IBaseEntity<int>
//    {
//        public int Id { get; set; }
//    }
//}