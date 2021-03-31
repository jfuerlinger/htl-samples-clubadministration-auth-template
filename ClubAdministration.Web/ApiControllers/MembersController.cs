using ClubAdministration.Core.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using ClubAdministration.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace ClubAdministration.Web.ApiControllers
{
    /// <summary>
    /// API-Controller für die Abfrage von Mitgliedern
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Member> _userManager;

        /// <summary>
        /// Constructor mit DI
        /// </summary>
        public MembersController(
            IUnitOfWork unitOfWork,
            UserManager<Member> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        /// <summary>
        /// Liefert alle Namen der Mitglieder
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<string[]>> GetAll()
          => await _unitOfWork.MemberRepository.GetMemberNamesAsync();


        /// <summary>
        /// Spezialroute zum Abfragen von Sektionen eines Mitglieds
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        [HttpGet]
        [Route("{lastName}/{firstName}/sections")]
        public async Task<IActionResult> Get(string lastName, string firstName)
        {
            var member = await _unitOfWork.MemberRepository.GetMemberByNameAsync(lastName, firstName);
            if (member == null)
            {
                return NotFound();
            }

            var sectionNames = await _unitOfWork
                .SectionRepository
                .GetSectionNamesForMemberAsync(member.Id);

            return Ok(sectionNames);
        }

        /// <summary>
        /// Spezialroute zum Abfragen der Sektionen des aktuellen Benutzers
        /// </summary>
        [HttpGet]
        [Route("mine/sections")]
        public async Task<IActionResult> GetMineSections()
        {
            //TODO
            return Ok();
        }

    }
}