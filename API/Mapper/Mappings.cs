using API.Entities.DTO;
using Xenergy.Entities;
using AutoMapper;

namespace API.Mapper
{
    public class Mappings: Profile
    {
        public Mappings()
        {
            CreateMap<UserGroup, UserGroupDTO>().ReverseMap();
        }
    }
}
