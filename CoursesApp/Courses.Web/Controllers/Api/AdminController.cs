using Courses.Domain.DomainModels;
using Courses.Domain.DTO;
using Courses.Domain.IdentityModels;
using Courses.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Courses.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ITransferRequestService _transferRequestService;
        private readonly UserManager<CoursesApplicationUser> _userManager;

        public AdminController(ITransferRequestService transferRequestService, UserManager<CoursesApplicationUser> userManager)
        {
            _transferRequestService = transferRequestService;
            _userManager = userManager;
        }

        [HttpGet("[action]")]
        public List<TransferRequest> GetAllTransfers()
        {
            return _transferRequestService.GetAllTransfers();
        }
        [HttpPost("[action]")]
        public TransferRequest GetDetailsForTransfers(BaseEntity model)
        {
            return _transferRequestService.GetTransfer(model.Id);
        }

        [HttpPost("[action]")]
        public bool ImportAllUsers(List<UserRegistrationDto> model)
        {
            bool status = true;
            foreach (var item in model)
            {
                var userCheck = _userManager.FindByEmailAsync(item.Email).Result;
                if (userCheck == null)
                {
                    var user = new CoursesApplicationUser
                    {
                        Name = "Test Name",
                        Surname = "Test LastName",
                        UserName = item.Email,
                        NormalizedUserName = item.Email.ToUpper(),
                        Email = item.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        PhoneNumber = "",
                  
                    };
                    var result = _userManager.CreateAsync(user, item.Password).Result;

                    status = status & result.Succeeded;
                }
                else
                {
                    continue;
                }
            }

            return status;
        }
    }
}
