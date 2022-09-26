using DBManageSystem.Core.Entities.MenuAggregate;

namespace DBManageSystem.ManageWebAPI.Endpoints.MenuEndpoints
{
    public class MainMenuDTO
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public int Order { get; set; }
        public List<SubMenuDTO> SubMenus { get; set; }
    }
}
