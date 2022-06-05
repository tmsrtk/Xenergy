using API.Controllers;
using API.Entities.DTO;
using API.Repository.IRepository;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestAPI.MockData;
using Xenergy.Entities;

namespace TestAPI.Systems.Controllers
{
    public class TestUserController
    {
        [Fact]
        public void GetUserGroupsShouldReturn200Status()
        {
            var expected = new UserGroupDTO();
            /// Arrange
            var _userGroupRepository = new Mock<IUserGroupRepository>();
            var _userRepository = new Mock<IUserRepository>();
            var _userTypeRepository = new Mock<IUserTypeRepository>();
            var _accessRuleRepository = new Mock<IAccessRuleRepository>();

            var _mapper = new Mock<IMapper>();
            _userGroupRepository.Setup(_ => _.GetUserGroups()).Returns(UserGroupMockData.GetUserGroups());
            _userRepository.Setup(_ => _.GetUsers()).Returns(new List<User> ());
            _userTypeRepository.Setup(_ => _.GetUserTypes()).Returns(new List<UserType>());
            _accessRuleRepository.Setup(_ => _.GetAccessRules()).Returns(new List<AccessRule>());
            _mapper.Setup(x => x.Map<UserGroup, UserGroupDTO>(It.IsAny<UserGroup>())).Returns(expected);
            var sut = new UserController(_userGroupRepository.Object, _userRepository.Object, _accessRuleRepository.Object, _userTypeRepository.Object, _mapper.Object);

            /// Act
            var result = sut.GetUserGroups();

            /// Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }
    }
}
