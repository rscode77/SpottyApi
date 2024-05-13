using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public enum RoleEnum
    {
        Admin,
        Moderator,
        User
    }
    public class Role
    {
        public int Id { get; set; } = 3;
        public string Name { get; set; } = Enum.GetName(typeof(RoleEnum), RoleEnum.User)!;
    }
}