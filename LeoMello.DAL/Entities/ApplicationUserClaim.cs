using Microsoft.AspNetCore.Identity;

namespace LeoMello.DAL.Entities
{
    /// <summary>
    ///     Represents our application user claim references.
    /// </summary>
    public class ApplicationUserClaim: IdentityUserClaim<Guid>
    {
        public override int Id 
        {
            get => base.Id; 
            set => base.Id = value; 
        }

        public override Guid UserId 
        { 
            get => base.UserId;
            set => base.UserId = value;
        }

        public override string? ClaimType 
        { 
            get => base.ClaimType;
            set => base.ClaimType = value; 
        }

        public override string? ClaimValue
        { 
            get => base.ClaimValue; 
            set => base.ClaimValue = value; 
        }
    }
}
