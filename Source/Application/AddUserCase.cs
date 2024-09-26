using ProductsNet.Source.Application.DTO;
using ProductsNet.Source.Domain;
using ProductsNet.Source.Infraestructure;

namespace ProductsNet.Source.Application
{
    public class AddUserUseCase(UserRepository userRepository)
    {
        private readonly UserRepository _userRepository = userRepository;

        public async Task<UserResponseDTO> ExecuteAsync(CreateUserDTO createUserDTO)
        {
            if (string.IsNullOrWhiteSpace(createUserDTO.Username)) throw new ArgumentException("El nombre de usuario es requerido");
            if (string.IsNullOrWhiteSpace(createUserDTO.Password)) throw new ArgumentException("La contrasena es requerida");

            var user = new User
            {
                Username = createUserDTO.Username,
                Password = createUserDTO.Password,
                Fullname = createUserDTO.Fullname
            };

            await _userRepository.AddUserAsync(user);

            return new UserResponseDTO
            {
                Username = user.Username,
                Fullname = user.Fullname,
                Id = user.Id
            };
        }
    }
}