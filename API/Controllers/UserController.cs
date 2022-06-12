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

            if (accessRuleAssignDTO.AccessRuleIDs.Any())
            {
                List<UserGroupAccessRule> UserGroupAccessRules = new List<UserGroupAccessRule>();
                foreach (var accessRuleId in accessRuleAssignDTO.AccessRuleIDs)
                {
                    UserGroupAccessRule userGroupAccessRule = new UserGroupAccessRule();
                    AccessRule access = new AccessRule();
                    access = _accessRuleRepository.GetAccessRule(accessRuleId);
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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<UserDTO>))]
        [ProducesResponseType(400)]
        [Route("users/")]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();
            var userDTOs = new List<UserDTO>();
            foreach (var user in users)
            {
                userDTOs.Add(_mapper.Map<UserDTO>(user));
            }
            return Ok(userDTOs);
        }

        [HttpGet("user/{id:int}", Name = "GetUser")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int id)
        {
            var user = _userRepository.GetUser(id);
            var userDTO = new UserDTO();
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                userDTO = _mapper.Map<UserDTO>(user);
                return Ok(userDTO);
            }
        }


        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UserDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("user/")]
        public IActionResult CreateUser([FromBody] UserUpsertDTO userUpsertDTO)
        {
            if (userUpsertDTO == null)
            {
                return BadRequest(ModelState);
            }
            if (_userRepository.UserExists(userUpsertDTO.UserName))
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User();
            user = _mapper.Map<User>(userUpsertDTO);
            user.UserGroup = _userGroupRepository.GetUserGroup(userUpsertDTO.UserGroupId);
            user.UserType = _userTypeRepository.GetUserType(userUpsertDTO.UserTypeId);
            bool success = _userRepository.CreateUser(user);
            if (!success)
            {
                ModelState.AddModelError("", $"Something went wrong when adding User {userUpsertDTO.UserName}");
                return StatusCode(500, ModelState);

            }
            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        }

        [HttpPatch("user/{Id:int}", Name = "UpdateUser")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateUser(int userId, [FromBody] UserUpsertDTO userUpsertDTO)
        {
            if (userUpsertDTO == null || userUpsertDTO.Id != userId)
            {
                return BadRequest(ModelState);
            }
            var user = new User();
            user = _mapper.Map<User>(userUpsertDTO);
            user.UserGroup = _userGroupRepository.GetUserGroup(userUpsertDTO.UserGroupId);
            user.UserType = _userTypeRepository.GetUserType(userUpsertDTO.UserTypeId);
            if (!_userRepository.UpdateUser(user))
            {
                ModelState.AddModelError("", $"Something went wrong when upddating User {userUpsertDTO.UserName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("user/{id:int}", Name = "DeleteUser")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUser(int id)
        {
            if (!_userRepository.UserExists(id))
            {
                return NotFound();
            }
            var user = _userRepository.GetUser(id);
            if (!_userRepository.DeleteUser(user))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting User {user.UserName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
