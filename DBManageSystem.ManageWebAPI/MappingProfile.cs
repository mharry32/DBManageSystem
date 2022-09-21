using AutoMapper;
using DBManageSystem.Core.Entities;
using DBManageSystem.ManageWebAPI.Endpoints.RoleManageEndpoints;
using DBManageSystem.ManageWebAPI.Endpoints.UserManageEndpoints;

namespace DBManageSystem.ManageWebAPI
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, UserDTO>();
            CreateMap<Role, RoleDTO>();
        }
    }
}
