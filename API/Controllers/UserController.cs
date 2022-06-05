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
    public class UserController : Controller
    {
        private IUserGroupRepository _userGroupRepository;
        private IUserRepository _userRepository;
        private IAccessRuleRepository _accessRuleRepository;
        private IUserTypeRepository _userTypeRepository;
        private readonly IMapper _mapper;

        public UserController(IUserGroupRepository userGroupRepository, IUserRepository userRepository, IAccessRuleRepository accessRuleRepository, IUserTypeRepository userTypeRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userGroupRepository = userGroupRepository;
            _userRepository = userRepository;
            _accessRuleRepository = accessRuleRepository;
            _userTypeRepository = userTypeRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<UserGroupDTO>))]
        [ProducesResponseType(400)]
        [Route("userGroups/")]
        public IActionResult GetUserGroups()
        {
            var userGroups = _userGroupRepository.GetUserGroups();
            var userGroupDTO = new List<UserGroupDTO>();
            foreach (var userGroup in userGroups)
            {
                userGroupDTO.Add(_mapper.Map<UserGroupDTO>(userGroup));
            }
            return Ok(userGroupDTO);
        }
        [HttpGet("userGroup/{id:int}", Name = "GetUserGroup")]
        [ProducesResponseType(200, Type = typeof(UserGroupDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetUserGroup(int id)
        {
            var userGroup = _userGroupRepository.GetUserGroup(id);
            var userGroupDTO = new UserGroupDTO();
            if (userGroup == null)
            {
                return NotFound();
            }
            else
            {
                userGroupDTO = _mapper.Map<UserGroupDTO>(userGroup);
                return Ok(userGroupDTO);
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UserGroupDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("userGroup/")]
        public IActionResult CreateUserGroup([FromBody] UserGroupDTO userGroupDTO)
        {
            if (userGroupDTO == null)
            {
                return BadRequest(ModelState);
            }
            if (_userGroupRepository.UserGroupExists(userGroupDTO.UserGroupName))
            {
                ModelState.AddModelError("", "User Group already exists");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userGroup = new UserGroup();
            userGroup = _mapper.Map<UserGroup>(userGroupDTO);
            bool success = _userGroupRepository.CreateUserGroup(userGroup);
            if (!success)
            {
                ModelState.AddModelError("", $"Something went wrong when adding user group {userGroupDTO.UserGroupName}");
                return StatusCode(500, ModelState);

            }
            return CreatedAtRoute("GetUserGroup", new { id = userGroup.Id }, userGroup);
        }

        [HttpPatch("userGroup/{Id:int}", Name = "UpdateUserGroup")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateUserGroup(int userGroupId, [FromBody] UserGroupDTO userGroupDTO)
        {
            if (userGroupDTO == null || userGroupDTO.Id != userGroupId)
            {
                return BadRequest(ModelState);
            }
            var userGroup = new UserGroup();
            userGroup = _mapper.Map<UserGroup>(userGroupDTO);
            if (!_userGroupRepository.UpdateUserGroup(userGroup))
            {
                ModelState.AddModelError("", $"Something went wrong when upddating user group {userGroupDTO.UserGroupName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("userGroup/{id:int}", Name = "DeleteUserGroup")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUserGroup(int id)
        {
            if (!_userGroupRepository.UserGroupExists(id))
            {
                return NotFound();
            }
            var userGroup = _userGroupRepository.GetUserGroup(id);
            if (!_userGroupRepository.DeleteUserGroup(userGroup))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting user group {userGroup.UserGroupName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /**
         * User Type endpoints
         */
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<UserTypeDTO>))]
        [ProducesResponseType(400)]
        [Route("userTypes/")]
        public IActionResult GetUserTypes()
        {
            var userTypes = _userTypeRepository.GetUserTypes();
            var userTypeDTO = new List<UserTypeDTO>();
            foreach (var userType in userTypes)
            {
                userTypeDTO.Add(_mapper.Map<UserTypeDTO>(userType));
            }
            return Ok(userTypeDTO);
        }
        [HttpGet("userType/{id:int}", Name = "GetUserType")]
        [ProducesResponseType(200, Type = typeof(UserTypeDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetUserType(int id)
        {
            var userType = _userTypeRepository.GetUserType(id);
            var userTypeDTO = new UserTypeDTO();
            if (userType == null)
            {
                return NotFound();
            }
            else
            {
                userTypeDTO = _mapper.Map<UserTypeDTO>(userType);
                return Ok(userTypeDTO);
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UserTypeDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("userType/")]
        public IActionResult CreateUserType([FromBody] UserTypeDTO userTypeDTO)
        {
            if (userTypeDTO == null)
            {
                return BadRequest(ModelState);
            }
            if (_userTypeRepository.UserTypeExists(userTypeDTO.UserTypeName))
            {
                ModelState.AddModelError("", "User Type already exists");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userType = new UserType();
            userType = _mapper.Map<UserType>(userTypeDTO);
            bool success = _userTypeRepository.CreateUserType(userType);
            if (!success)
            {
                ModelState.AddModelError("", $"Something went wrong when adding user type {userTypeDTO.UserTypeName}");
                return StatusCode(500, ModelState);

            }
            return CreatedAtRoute("GetUserType", new { id = userType.Id }, userType);
        }

        [HttpPatch("userType/{Id:int}", Name = "UpdateUserType")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateUserType(int userTypeId, [FromBody] UserTypeDTO userTypeDTO)
        {
            if (userTypeDTO == null || userTypeDTO.Id != userTypeId)
            {
                return BadRequest(ModelState);
            }
            var userType = new UserType();
            userType = _mapper.Map<UserType>(userTypeDTO);
            if (!_userTypeRepository.UpdateUserType(userType))
            {
                ModelState.AddModelError("", $"Something went wrong when upddating user type {userTypeDTO.UserTypeName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("userType/{id:int}", Name = "DeleteUserType")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUserType(int id)
        {
            if (!_userTypeRepository.UserTypeExists(id))
            {
                return NotFound();
            }
            var userType = _userTypeRepository.GetUserType(id);
            if (!_userTypeRepository.DeleteUserType(userType))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting user type {userType.UserTypeName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("userGroup/{id:int}/accessRules")]
        public IActionResult AssignAccessRulesToUserGroup(int id, [FromBody] AccessRuleAssignDTO accessRuleAssignDTO)
        {
            if (!_userGroupRepository.UserGroupExists(id))
            {
                ModelState.AddModelError("", $"User Group is not available {id}");
                return StatusCode(404, ModelState);
            }
            if(_accessRuleRepository.UserGroupAccessRuleExists(id))
            {
                if(!_accessRuleRepository.DeleteUserGroupAccessRules(id))
                {
                    ModelState.AddModelError("", $"Something went wrong when deleting access rule permission {id}");
                    return StatusCode(500, ModelState);
                }
            }

            if (accessRuleAssignDTO.AccessRules.Any())
            {
                List<UserGroupAccessRule> UserGroupAccessRules = new List<UserGroupAccessRule>();
                foreach (var accessRule in accessRuleAssignDTO.AccessRules)
                {
                    UserGroupAccessRule userGroupAccessRule = new UserGroupAccessRule();
                    AccessRule access = new AccessRule();
                    access = _mapper.Map<AccessRule>(accessRule);
                    UserGroup userGroup = _userGroupRepository.GetUserGroup(id);
                    userGroupAccessRule.UserGroup = userGroup;
                    userGroupAccessRule.AccessRule = access;
                    UserGroupAccessRules.Add(userGroupAccessRule);
                }

                if(!_accessRuleRepository.AddUserGroupAccessRules(UserGroupAccessRules))
                {
                    ModelState.AddModelError("", $"Something went wrong when assigning access rule to user group {id}");
                    return StatusCode(500, ModelState);
                }
            }
            return NoContent();

        }
    }
}
