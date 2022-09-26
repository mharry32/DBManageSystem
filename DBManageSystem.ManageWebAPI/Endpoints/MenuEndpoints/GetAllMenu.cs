using Ardalis.ApiEndpoints;
using AutoMapper;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.MenuEndpoints
{
    public class GetAllMenu : EndpointBaseAsync.WithoutRequest.WithActionResult<List<MainMenuDTO>>
    {
        private IMenuService _menuService;
        private IMapper _mapper;
        public GetAllMenu(IMenuService menuService,IMapper mapper)
        {
            _menuService = menuService;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("/users/allmenus")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<ActionResult<List<MainMenuDTO>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var subMenus = (await _menuService.GetAllMenus()).Value;
            
           var menuGroups = subMenus.GroupBy(t => t.MainMenu.Id).OrderBy(t=>t.Key);

            List<MainMenuDTO> result = new List<MainMenuDTO>();
            foreach(var group in menuGroups)
            {
                var mainMenu = subMenus.FirstOrDefault(s => s.MainMenu.Id == group.Key);
                MainMenuDTO mainMenuDTO = new MainMenuDTO()
                {
                    Id = mainMenu.Id,
                    Name = mainMenu.Name,
                    SubMenus = new List<SubMenuDTO>(),
                    Order = mainMenu.Order
                };

                var subMenuDTOs = group.OrderBy(t => t.Order).ToList();
                _mapper.Map(subMenuDTOs, mainMenuDTO.SubMenus);
                result.Add(mainMenuDTO);
            }

            return result.OrderBy(t => t.Order).ToList();

        }
    }
}
