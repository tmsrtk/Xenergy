using API.Entities.DTO;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Xenergy.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/")]
    [ProducesResponseType(404)]
    public class AccessRuleController : Controller
    {
        private IUserGroupRepository _userGroupRepository;
        private IUserRepository _userRepository;
        private IAccessRuleRepository _accessRuleRepository;
        private IUserTypeRepository _userTypeRepository;
        private readonly IMapper _mapper;


        public AccessRuleController(IUserGroupRepository userGroupRepository, IUserRepository userRepository, IAccessRuleRepository accessRuleRepository, IUserTypeRepository userTypeRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userGroupRepository = userGroupRepository;
            _userRepository = userRepository;
            _accessRuleRepository = accessRuleRepository;
            _userTypeRepository = userTypeRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<AccessRuleDTO>))]
        [ProducesResponseType(400)]
        [Route("accessRule/")]
        public IActionResult GetAccessRules()
        {
            var accessRules = _accessRuleRepository.GetAccessRules();
            var accessRuleDTO = new List<AccessRuleDTO>();
            foreach (var accessRule in accessRules)
            {
                accessRuleDTO.Add(_mapper.Map<AccessRuleDTO>(accessRule));
            }
            return Ok(accessRuleDTO);
        }
        [HttpGet("accessRule/{id:int}", Name = "GetAccessRule")]
        [ProducesResponseType(200, Type = typeof(AccessRuleDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetAccessRule(int id)
        {
            var accessRule = _accessRuleRepository.GetAccessRule(id);
            var accessRuleDTO = new AccessRuleDTO();
            if (accessRule == null)
            {
                return NotFound();
            }
            else
            {
                accessRuleDTO = _mapper.Map<AccessRuleDTO>(accessRule);
                return Ok(accessRuleDTO);
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(AccessRuleDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("accessRule/")]
        public IActionResult CreateAccessRule([FromBody] AccessRuleDTO accessRuleDTO)
        {
            if (accessRuleDTO == null)
            {
                return BadRequest(ModelState);
            }
            if (_accessRuleRepository.AccessRuleExists(accessRuleDTO.AccessRuleName))
            {
                ModelState.AddModelError("", "Access Rule already exists");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accessRule = new AccessRule();
            accessRule = _mapper.Map<AccessRule>(accessRuleDTO);
            bool success = _accessRuleRepository.CreateAccessRule(accessRule);
            if (!success)
            {
                ModelState.AddModelError("", $"Something went wrong when adding Access Rule {accessRuleDTO.AccessRuleName}");
                return StatusCode(500, ModelState);

            }
            return CreatedAtRoute("GetAccessRule", new { id = accessRule.Id }, accessRule);
        }

        [HttpPatch("accessRule/{Id:int}", Name = "UpdateAccessRule")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateAccessRule(int accessRuleId, [FromBody] AccessRuleDTO accessRuleDTO)
        {
            if (accessRuleDTO == null || accessRuleDTO.Id != accessRuleId)
            {
                return BadRequest(ModelState);
            }
            var accessRule = new AccessRule();
            accessRule = _mapper.Map<AccessRule>(accessRuleDTO);
            if (!_accessRuleRepository.UpdateAccessRule(accessRule))
            {
                ModelState.AddModelError("", $"Something went wrong when upddating Access Rule {accessRuleDTO.AccessRuleName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("accessRule/{id:int}", Name = "DeleteAccessRule")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAccessRule(int id)
        {
            if (!_accessRuleRepository.AccessRuleExists(id))
            {
                return NotFound();
            }
            var accessRule = _accessRuleRepository.GetAccessRule(id);
            if (!_accessRuleRepository.DeleteAccessRule(accessRule))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting Access Rule {accessRule.AccessRuleName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}
