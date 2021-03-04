using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using LiteDB;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Identity.LiteDB
{
   [SuppressMessage("ReSharper", "UnusedMember.Global")]
   public class IdentityRole : IdentityRole<string>
   {
      public IdentityRole()
      {
         Id = ObjectId.NewObjectId().ToString();
         Claims = new List<IdentityUserClaim>();
      }

      public IdentityRole(string roleName) : this() => Name = roleName;

      [BsonId] public new string Id { get; set; }

      public new string Name { get; set; }
      public List<IdentityUserClaim> Claims { get; set; }

      public virtual void AddClaim(Claim claim)
      {
         Claims.Add(new IdentityUserClaim(claim));
      }

      public virtual void RemoveClaim(Claim claim)
      {
         var claimsToRemove = Claims
            .Where(c => c.Type == claim.Type)
            .Where(c => c.Value == claim.Value);

         Claims = Claims.Except(claimsToRemove).ToList();
      }
   }
}
