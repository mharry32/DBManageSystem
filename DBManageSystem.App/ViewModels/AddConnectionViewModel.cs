using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManageSystem.App.ViewModels
{
    public class AddConnectionViewModel:BaseViewModel
    {
       public DatabaseServer server;

        public IList<DatabaseTypeEnum> serverTypes { get; set; }

        public DatabaseTypeEnum SelectedDBType { get; set; }
        public AddConnectionViewModel()
        {
            server = new DatabaseServer();
            serverTypes = DatabaseTypeEnum.List.ToList();
            
        }


        public string ConnectionUrl
        {
            get { return server.ConnectUrl; }
            set
            {
                if (server.ConnectUrl != value)
                {
                    server.ConnectUrl = value;
                    OnPropertyChanged(nameof(ConnectionUrl));
                }
            }
        }
    }

}
