using AuthServer.Entity;
using AuthServer.Repository;
using IdentityModel;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// https://stackoverflow.com/questions/35304038/identityserver4-register-userservice-and-get-users-from-database-in-asp-net-core/35306021#35306021
namespace AuthServer.Services
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserClientRepository _userClientRepository;
        public ResourceOwnerPasswordValidator(UserClientRepository userClientRepository)
        {
            _userClientRepository = userClientRepository;
        }

        // 验证 /connect/token 时的用户账号信息
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var clientName = context.UserName;
            var password = context.Password;
            try
            {
                var user = await _userClientRepository.FindAsync(p => p.ClientName == clientName);
                if (user == null) context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidGrant, "无效的客户端");
                if (user.Password == password)
                {
                    context.Result = new GrantValidationResult(
                        subject: user.ClientId,
                        authenticationMethod: "custom",
                        claims: GetUserClaims(user));
                }
                else
                {
                    context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidGrant, "密码错误");
                }
            }
            catch (Exception)
            {
                context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidGrant, "用户账号或密码不正确");
            }

        }

        public static Claim[] GetUserClaims(UserClient user)
        {
            return new Claim[] {
                new Claim("userid",user.ClientId),
                new Claim(JwtClaimTypes.Name,user.ClientName),
                new Claim(JwtClaimTypes.Email,""),
            };
        }
    }
}
