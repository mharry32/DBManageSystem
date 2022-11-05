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
       public DatabaseServer server;

        private readonly IDatabaseServerService _databaseServerService;
        public ICommand AddDatabaseServer { get; set; }

        public IList<DatabaseTypeEnum> serverTypes { get; set; }

        public DatabaseTypeEnum SelectedDBType { get; set; }
        public AddConnectionViewModel(IDatabaseServerService databaseServerService)
        {
            _databaseServerService = databaseServerService;
            server = new DatabaseServer();
            serverTypes = DatabaseTypeEnum.List.ToList();
            AddDatabaseServer = new Command(() => AddDatabaseCommand());

        }

        private void AddDatabaseCommand()
        {

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
