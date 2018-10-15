using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using PipeDriveApi.Models;
using PipeDriveApi.Response;

namespace PipeDriveApi.EntityServices
{
    public abstract class PagingEntityService<TEntity> : EntityServiceBase
        where TEntity : BaseEntity
    {
        protected string _Resource;

        #region constructor

        public PagingEntityService(IPipeDriveClient client, string resource) : base(client)
        {
            _Resource = resource;
        }

        #endregion

        #region interface

        public virtual async Task<ListResult<TEntity>> GetAsync(int start = 0, int limit = 100, Sort sort = null)
        {
            var request = new RestRequest(_Resource, Method.GET);
            return await GetAsync(request, start, limit, sort);
        }

        public virtual async Task<ListResult<TEntity>> GetAsync(IRestRequest request, int start = 0, int limit = 100, Sort sort = null)
        {
            request.SetQueryParameter("start", start.ToString());
            request.SetQueryParameter("limit", limit.ToString());
            if (sort != null)
                request.SetQueryParameter("sort", sort.ToString());

            var response = await _client.ExecuteRequestWithCustomResponseAsync<PaginatedPipeDriveResponse<TEntity>, List<TEntity>>(request);
            return new ListResult<TEntity>(response.Data, response.AdditionalData.Pagination);
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(Sort sort = null)
        {
            var request = new RestRequest(_Resource, Method.GET);
            return await GetAllAsync(request, sort);
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(IRestRequest request, Sort sort = null)
        {
            var combinedList = new List<TEntity>();
            int start = 0, limit = 1000;
            while (true)
            {
                var response = await GetAsync(request, start, limit, sort);
                combinedList.AddRange(response);
                start = response.Pagination.NextStart;
                if(!response.Pagination.MoreItemsInCollection) break;
            }
            return combinedList;
        }

        public virtual async Task<TEntity> PostAsync(IDictionary<string, object> parameters)
        {
            return await PostAsync(null, parameters);
        }

        public virtual async Task<TEntity> PostAsync(string resource, IDictionary<string, object> parameters)
        {
            var request = new RestRequest(BuildResourceString(resource), Method.POST);
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    request.AddParameter(parameter.Key, parameter.Value);
                }
            }
            var response = await _client.ExecuteRequestWithCustomResponseAsync<PipeDriveResponse<TEntity>, TEntity>(request);
            return response.Data;
        }

        public virtual async Task<TEntity> PutAsync(IDictionary<string, object> parameters)
        {
            return await PutAsync(null, parameters);
        }

        public virtual async Task<TEntity> PutAsync(string resource, IDictionary<string, object> parameters)
        {
            var request = new RestRequest(BuildResourceString(resource), Method.PUT);
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    request.AddParameter(parameter.Key, parameter.Value);
                }
            }
            var response = await _client.ExecuteRequestWithCustomResponseAsync<PipeDriveResponse<TEntity>, TEntity>(request);
            return response.Data;
        }

        public virtual async Task<DeleteResponse> DeleteAsync(string resource = null)
        {
            var request = new RestRequest(BuildResourceString(resource), Method.DELETE);
            return await _client.ExecuteRequestAsync<DeleteResponse>(request);
        }

        #endregion

        #region helpers

        string BuildResourceString(string resource)
        {
            return !string.IsNullOrEmpty(resource)
                ? $"{_Resource}/{resource}"
                : _Resource;
        }

        #endregion
    }
}
