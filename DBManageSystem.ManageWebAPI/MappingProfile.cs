using AutoMapper;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Entities.MenuAggregate;
using DBManageSystem.Core.Entities.SchemaAggregate;
using DBManageSystem.Core.Enums;
using DBManageSystem.ManageWebAPI.Endpoints.DBManageEndpoints;
using DBManageSystem.ManageWebAPI.Endpoints.DBOperationsEndpoints;
using DBManageSystem.ManageWebAPI.Endpoints.MenuEndpoints;
using DBManageSystem.ManageWebAPI.Endpoints.RoleManageEndpoints;
using DBManageSystem.ManageWebAPI.Endpoints.UserManageEndpoints;

namespace DBManageSystem.ManageWebAPI
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<DatabaseServerTypeDTO, DatabaseTypeEnum>().ConvertUsing((s, d) =>
            {
                return DatabaseTypeEnum.FromValue(s.TypeId);
            });

            CreateMap<DatabaseTypeEnum, DatabaseServerTypeDTO>().ConvertUsing((t, s) =>
            {
                return new DatabaseServerTypeDTO()
                {
                    TypeId = t.Value,
                    TypeName = t.Name
                };
            });

            CreateMap<int, DatabaseStatusEnum?>().ConvertUsing((i,dse) =>
            {
                if (i == 0)
                {
                    return null;
                }
                return DatabaseStatusEnum.FromValue(i);
            });

            CreateMap<DateTime?, string>().ConvertUsing((d, s) =>
            {
                if(d==null)
                {
                    return "";
                }
                else
                {
                    return d.Value.ToString("yyyy-MM-dd HH:mm");
                }
            });

            CreateMap<string, DateTime?>().ConvertUsing((s, d) =>
            {
                if (string.IsNullOrEmpty(s))
                {
                    return null;
                }
                else
                {
                    return DateTime.Parse(s);
                }
            });
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, UserDTO>();
            CreateMap<Role, RoleDTO>();
            CreateMap<MainMenu, MainMenuDTO>();
            CreateMap<SubMenu, SubMenuDTO>();
            CreateMap<CreateDBRequest, DatabaseServer>();
            CreateMap<DatabaseServer, DatabaseServerDTO>();
            CreateMap<DatabaseServerDTO, DatabaseServer>();
            CreateMap<DatabaseSchema, DatabaseSchemaDTO>();
            CreateMap<TableSchema, TableSchemaDTO>();
            CreateMap<SqlLog, SqlLogDTO>();
        }
    }
}
