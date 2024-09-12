namespace RamSoft.Domain.Base
{
    public abstract class BaseEntity<T>
    {

        public required T Id { get; set; }
        public DateTime CreateDate { get; set; }
        public required string CreatorID { get; set; }
        public DateTime LastEditDate { get; set; }
        public string? LastEditorID { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}
