using POS.Application.Commons.Bases;
using POS.Application.DTOs.User.Request;

namespace POS.Application.Interfaces
{
    public interface IUserApplication
    {
        Task<BaseResponse<bool>> RegisterUser(UserRequestDTO requesDTO);
        Task<BaseResponse<string>> GenerateToken(TokenRequestDTO requestDTO);
    }
}
