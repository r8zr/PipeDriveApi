using PipeDriveApi.Models;

namespace PipeDriveApi.EntityServices
{
    public class OrganizationEntityService<TOrganization> : PagingEntityService<TOrganization>
        where TOrganization : Organization
    {
        public OrganizationEntityService(IPipeDriveClient client) : base(client, "organizations")
        {
        }
    }
}
