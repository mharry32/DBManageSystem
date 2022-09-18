namespace DBManageSystem.ManageWebAPI.Endpoints.AuthEndpoints
{
    public class LoginResponse
    {

        public LoginResponse(string token)
        {
            Token = token;
        }

       public string Token { get; set; }
    }
}
