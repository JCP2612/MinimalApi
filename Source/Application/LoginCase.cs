using ProductsNet.Source.Application.DTO;
using ProductsNet.Source.Infraestructure;

namespace ProductsNet.Source.Application
{
    public class LoginUseCase(UserRepository userRepository)
    {
        private readonly UserRepository _userRepository = userRepository;

        public async Task<UserResponseDTO?> ExecuteAsync(LoginDTO loginDTO)
        {
            if (string.IsNullOrWhiteSpace(loginDTO.Username))
            {
                throw new ArgumentException("No se puede iniciar sesion sin un username");
            }
            // TODO: Estandarizar errores
            if (string.IsNullOrWhiteSpace(loginDTO.Password))
            {
                throw new ArgumentException("No se puede iniciar sesion sin una contrasena");
            }

            var user = await _userRepository.GetUserByUsernameAsync(loginDTO.Username);
            if (user != null)
            {
                System.Console.WriteLine("hola");
                if (user.Password != loginDTO.Password)
                {
                    return null;
                }
                user.LastLogin = DateTime.UtcNow;
                await _userRepository.UpdateUserAsync(user);
                return new UserResponseDTO
                {
                    Fullname = user.Fullname,
                    Username = user.Username,
                    Id = user.Id
                };
            }
            return null;
        }
    }
}