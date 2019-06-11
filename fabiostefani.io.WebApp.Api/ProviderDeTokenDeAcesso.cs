using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;

namespace fabiostefani.io.WebApp.Api
{
    public class ProviderDeTokenDeAcesso : OAuthAuthorizationServerProvider
    {
        //public Task AuthorizationEndpointResponse(OAuthAuthorizationEndpointResponseContext context)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public Task GrantAuthorizationCode(OAuthGrantAuthorizationCodeContext context)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public Task GrantCustomExtension(OAuthGrantCustomExtensionContext context)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        //{
        //    throw new System.NotImplementedException();
        //}

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var usuario = BaseUsuarios.Usuarios().FirstOrDefault(x => x.Nome == context.UserName && x.Senha == context.Password);

            if (usuario == null)
            {
                context.SetError("invalid_grant", "Usuário não encontrado ou senha incorreta.");
                return;
            }

            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                {
                    "Username",context.UserName
                }
            });

            var identy = new ClaimsIdentity(context.Options.AuthenticationType);
            var identidadeUsuario = new AuthenticationTicket(identy, props );

            foreach (var funcao in usuario.Funcoes)
            {
                identidadeUsuario.Identity.AddClaim(new Claim(ClaimTypes.Role, funcao));
            }

            context.Validated(identidadeUsuario);            
        }

        //public Task MatchEndpoint(OAuthMatchEndpointContext context)
        //{
        //    throw new System.NotImplementedException();
        //}

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var item in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(item.Key, item.Value);
            }
            var claims = context.Identity.Claims
                                         .GroupBy(x => x.Type)
                                         .Select(x => new { Claim = x.Key, Value = x.Select(z => z.Value).ToArray() });

            foreach (var item in claims)
            {
                context.AdditionalResponseParameters.Add(item.Claim, JsonConvert.SerializeObject(item.Value));
            }

            return base.TokenEndpoint(context);

        }

        //public Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        //{
        //    throw new System.NotImplementedException();
        //}

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();            
        }

        //public Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public override async Task ValidationClientAuthentication(OAuthValidateClientAuthenticationContext context)
        //{
        //    context.Validated();
        //}
        
        //public override async Task GrantResourcesOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        //{
        //    
        //}
    }
}
