using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

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
            }

            var identidadeUsuario = new ClaimsIdentity(context.Options.AuthenticationType);
            context.Validated(identidadeUsuario);            
        }

        //public Task MatchEndpoint(OAuthMatchEndpointContext context)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public Task TokenEndpoint(OAuthTokenEndpointContext context)
        //{
        //    throw new System.NotImplementedException();
        //}

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