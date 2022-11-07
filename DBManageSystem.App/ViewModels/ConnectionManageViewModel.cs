using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Interfaces;

namespace DBManageSystem.App.ViewModels
{
	public class ConnectionManageViewModel
	{
        public ObservableCollection<DatabaseServer> servers { get; set; }
        public ICommand DeleteDbCommand { get; set; }

        private readonly IDatabaseServerService _databaseServerService;
        public ConnectionManageViewModel(IDatabaseServerService databaseServerService)
		{
            servers = new ObservableCollection<DatabaseServer>();
            _databaseServerService = databaseServerService;
            DeleteDbCommand = new Command<DatabaseServer>(async (arg) => {

               await DeleteDb(arg);
            });
        }

        
        public async Task DeleteDb(DatabaseServer server)
        {

            var result = await _databaseServerService.DeleteDatabaseServer(server.Id);
            if (result.IsSuccess == true)
            {
                await Reload();
            }

        }

        private async Task Reload()
        {
            var result = await _databaseServerService.GetServerList();
            if (result.IsSuccess == true)
            {
                servers.Clear();
                result.Value.ForEach(t => { servers.Add(t); });
            }
           
        }

        public async Task Initial()
        {
            await Reload();
        }
	}
}

