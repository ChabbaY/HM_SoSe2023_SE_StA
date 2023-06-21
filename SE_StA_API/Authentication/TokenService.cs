using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace SE_StA_API.Authentication {
    public class TokenService {
        private const int ExpirationMinutes = 30;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public TokenService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> CreateToken(IdentityUser user) {
            var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
            var claims = await CreateClaims(user);
            var token = CreateJwtToken(
                claims,
                CreateSigningCredentials(),
                expiration
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials, DateTime expiration)
            => new(
                "apiWithAuthBackend",
                "apiWithAuthBackend",
                claims,
                expires: expiration,
                signingCredentials: credentials
            );
            
        
        private async Task<List<Claim>> CreateClaims(IdentityUser user) {
            try {
                var claims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.Sub, "TokenForTheApiWithAuth"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                var userClaims = await _userManager.GetClaimsAsync(user);
                var userRoles = await _userManager.GetRolesAsync(user);
                claims.AddRange(userClaims);
                foreach (var userRole in userRoles) {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                    var role = await _roleManager.FindByNameAsync(userRole);
                    if (role != null) {
                        var roleClaims = await _roleManager.GetClaimsAsync(role);
                        foreach(Claim roleClaim in roleClaims) {
                            claims.Add(roleClaim);
                        }
                    }
                }

                return claims;
            } catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }
        private SigningCredentials CreateSigningCredentials() {
            return new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("9Yacv&DbDaK9bh&en$bcojwycAHxFncqs2tLG$PHgz6AumSzCnW&89Ss8ebBhqq!")
                ),
                SecurityAlgorithms.HmacSha512
            );
        }
    }
}