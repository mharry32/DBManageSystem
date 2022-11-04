using DBManageSystem.Core.Entities;
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

        public AddConnectionViewModel()
        {
            server = new DatabaseServer();
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
