namespace DBManageSystem.ManageWebAPI.Endpoints.DBOperationsEndpoints
{
    public class DatabaseSchemaDTO
    {
        public string Name { get; set; }
        public bool IsLeaf
        { get { return false; } }
        public int ServerId { get; set; }
    }
}
