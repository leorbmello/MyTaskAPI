using Microsoft.AspNetCore.Identity;

namespace LeoMello.DAL.Entities
{
    /// <summary>
    ///     Represents our application user role information.
    /// </summary>
    public class ApplicationRole : IdentityRole<Guid>
    {
        public override Guid Id 
        {
            get => base.Id;
            set => base.Id = value; 
        }

        public override string? Name
        { 
            get => base.Name;
            set => base.Name = value;
        }

        public override string? NormalizedName
        { 
            get => base.NormalizedName; 
            set => base.NormalizedName = value; 
        }

        public override string? ConcurrencyStamp 
        {
            get => base.ConcurrencyStamp; 
            set => base.ConcurrencyStamp = value;
        }

        public override string ToString()
        {
            return string.Format("[{0}] Role Name: {1}", Id, Name);
        }
    }
}