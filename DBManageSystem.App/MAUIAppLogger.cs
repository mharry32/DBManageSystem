using System;
using DBManageSystem.Core.Interfaces;
using DBManageSystem.Infrastructure.Services;

namespace DBManageSystem.App
{
	public class MAUIAppLogger<T>: IAppLogger<T>
    {
		public MAUIAppLogger()
		{
		}

        public void LogInformation(string message, params object[] args)
        {
            Console.WriteLine(message);
        }

        public void LogWarning(string message, params object[] args)
        {
            Console.WriteLine(message);
        }
    }
}

