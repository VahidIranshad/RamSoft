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
        public byte[]? RowVersion { get; set; }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;

            if (obj is null) return false;

            foreach (var item in obj.GetType().GetProperties().Where(p => p.Name != "RowVersion"))
            {
                var sValue = item.GetValue(this);
                var dValue = item.GetValue(obj);
                if (sValue == null)
                {
                    if (dValue != null)
                    {
                        return false;
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (!sValue.Equals(dValue))
                {
                    return false;
                }
            }
            return true;
        }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}
