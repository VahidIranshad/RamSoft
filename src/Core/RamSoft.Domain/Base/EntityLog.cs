namespace RamSoft.Domain.Base
{
    public class EntityLog : BaseEntity
    {
        public required string ClassName { get; set; }
        public string ChangeType { get; set; }
        public required string Key { get; set; }
    }
}
