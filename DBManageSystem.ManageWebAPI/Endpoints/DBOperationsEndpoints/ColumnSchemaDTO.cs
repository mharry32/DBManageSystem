namespace DBManageSystem.ManageWebAPI.Endpoints.DBOperationsEndpoints
{
    public class ColumnSchemaDTO
    {
        public string Name { get; set; }

        public bool IsLeaf { get { return true; } }
    }
}
