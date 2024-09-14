using System.ComponentModel.DataAnnotations;

namespace RamSoft.Domain.Base
{
    public abstract class BaseEntity<T>
    {

        public T Id { get; set; }
        public DateTime CreateDate { get; set; }
        [MaxLength(100)]
        public string? CreatorID { get; set; }
        public DateTime LastEditDate { get; set; }
        [MaxLength(100)]
        public string? LastEditorID { get; set; }
        public bool IsDeleted { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}
