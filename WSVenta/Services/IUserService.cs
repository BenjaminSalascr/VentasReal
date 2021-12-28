using WSVenta.Models.Request;
using WSVenta.Models.Response;

namespace WSVenta.Services
{
    public interface IUserService
    {
        /// <summary>
        /// método que recibe un modelo de AuthRequest y retorna un objeto tipo UserResponse
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        UserResponse Auth(AuthRequest model);
    }
}
