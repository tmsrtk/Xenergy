using API.Entities.DTO;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Xenergy.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/")]
    [ProducesResponseType(404)]
    public class UserController : Controller
    {
        private IUserGroupRepository _userGroupRepository;
        private readonly IMapper _mapper;

        public UserController(IUserGroupRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _userGroupRepository = repo;
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
    }
}
