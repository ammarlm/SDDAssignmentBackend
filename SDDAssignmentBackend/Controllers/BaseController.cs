using System.Net;
using Microsoft.AspNetCore.Mvc;
using SDDAssignmentBackend.DTO;

namespace SDDAssignmentBackend.Controllers
{
    public class BaseController : ControllerBase
    {
        [NonAction]
        public GenResponse<T> Result<T>(T? data, string msg, bool success) => new()
        {
            Data = data,
            Msg = msg,
            Success = success
        };

        [NonAction] 
        public GenResponse<T> Sucess<T>(T data) => Result(data, "", true);

        [NonAction] 
        public GenResponse<object> Sucess(string msg) => Result<object>(null, msg, true);
    }
}
