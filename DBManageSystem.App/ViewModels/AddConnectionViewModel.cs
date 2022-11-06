using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Enums;
using DBManageSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DBManageSystem.App.ViewModels
{
    public class AddConnectionViewModel:BaseViewModel
    {
       public DatabaseServer server { get; set; }

        private readonly IDatabaseServerService _databaseServerService;
        public ICommand AddDatabaseServer { get; set; }

        public IList<DatabaseTypeEnum> serverTypes { get; set; }

        
        public AddConnectionViewModel(IDatabaseServerService databaseServerService)
        {
            _databaseServerService = databaseServerService;
            server = new DatabaseServer();
            serverTypes = DatabaseTypeEnum.List.ToList();
            AddDatabaseServer = new Command(() => AddDatabaseCommand());

        }

        private async void AddDatabaseCommand()
        {
            var dblist = await _databaseServerService.GetServerList();
            if (dblist.Value.Count == 0)
            {
                server.Id = 1;
            }
            else
            {
               var maxid = dblist.Value.MaxBy(t => t.Id);
                server.Id = maxid.Id + 1;
            }
            
            server.Name = DateTime.Now.Ticks.ToString();
            var result = await _databaseServerService.CreateDatabaseServer(server);
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

        public string UserName
        {
            get { return server.UserName; }
            set
            {
                if (server.UserName != value)
                {
                    server.UserName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        public string Password
        {
            get { return server.Password; }
            set
            {
                if (server.Password != value)
                {
                    server.Password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
    }

}
