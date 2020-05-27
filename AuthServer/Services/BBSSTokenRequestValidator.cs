using AuthServer.Repository;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Services
{
    public class BBSSTokenRequestValidator : ICustomTokenRequestValidator
    {
        private readonly UserClientRepository _userClientRepository;
        public BBSSTokenRequestValidator(UserClientRepository userClientRepository)
        {
            _userClientRepository = userClientRepository;
        }
        public async Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            var validatedRequest = context.Result.ValidatedRequest;
            var clientId = validatedRequest.ClientId;
            var password = validatedRequest.Secret.Id;
            try
            {
                var user = await _userClientRepository.FindAsync(p => p.ClientId == clientId);
                if (user == null)
                {
                    SetError(context, "客户端无效");
                    return;
                }
                if (user.Password == password)
                {
                    return;
                }
                else
                {
                    SetError(context, "密码错误");
                }
            }
            catch (Exception)
            {
                SetError(context, "用户账号或密码不正确");
            }
        }

        private void SetError(CustomTokenRequestValidationContext context, string error)
        {
            context.Result.Error = error;
            context.Result.IsError = true;
        }
    }
}
