using RamSoft.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace RamSoft.Domain.Jira
{
    public class States : BaseEntity
    {
        [MaxLength(100)]
        public required string Name { get; set; }
    }
}
