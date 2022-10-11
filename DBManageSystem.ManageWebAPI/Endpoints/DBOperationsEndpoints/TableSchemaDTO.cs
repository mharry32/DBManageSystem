namespace DBManageSystem.ManageWebAPI.Endpoints.DBOperationsEndpoints
{
    public class TableSchemaDTO
    {
        public string Name { get; set; }

        public bool IsLeaf { get { return false; } }
    }
}
