using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities.MenuAggregate;
using DBManageSystem.Core.Entities.MenuAggregate.Specifications;
using Moq;
using Xunit;

namespace DBManageSystem.UnitTests.Core.Specifications;
public class SubMenuWithMainMenuByIdSpecTest
{

  private const string firstMainMenuName = "m1";
  private const string secondMainMenuName = "m2";

  private const string firstSubMenuName = "s1";
  private const string secondSubMenuName = "s2";

  [Fact]
  public void GetSubMenuWithCertainId()
  {
    var spec = new SubMenuWithMainMenuByIdSpec(2);
    var result = spec.Evaluate(GetTestCollection()).FirstOrDefault();
    Assert.NotNull(result);
    Assert.NotNull(result.MainMenu);
    Assert.Equal($"{secondMainMenuName + firstSubMenuName}", result.Name);
    Assert.Equal(secondMainMenuName, result.MainMenu.Name);
  }


  private List<SubMenu> GetTestCollection()
  {
      var subMenus = new List<SubMenu>();
      MenusBuilder builder = new MenusBuilder();
      var menu1 = builder.AddMainMenu(firstMainMenuName).AddSubMenu($"{firstMainMenuName+firstSubMenuName}").Build();
    subMenus.AddRange(menu1.Item2);

    var menu2 = builder
      .AddMainMenu(secondMainMenuName).AddSubMenu($"{secondMainMenuName + firstSubMenuName}")
      .AddSubMenu($"{secondMainMenuName + secondSubMenuName}").Build();
    subMenus.AddRange(menu2.Item2);
    return subMenus;
  }
}
