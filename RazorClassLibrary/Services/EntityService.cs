using Domain.Common;
using System.Net.Http.Json;
using System.Text.Json;

namespace RazorClassLibrary.Services
{
    public class EntityService<TEntityDTO, TEntityForCreationDTO, TEntityForUpdateDTO, TIdentifier> : IDisposable, IEntityService<TEntityDTO, TEntityForCreationDTO, TEntityForUpdateDTO, TIdentifier>
        where TEntityDTO : AuditableDTO, IBaseEntity<TIdentifier>
        where TEntityForCreationDTO : AuditableDTO
        where TEntityForUpdateDTO : AuditableDTO, IBaseEntity<TIdentifier>
    {
        protected HttpClient httpClient;
        protected readonly string _baseAddress;
        private readonly string _addressSuffix;
        private bool disposed = false;

        public EntityService(string baseAddress, string addressSuffix)
        {
            _baseAddress = baseAddress;
            _addressSuffix = addressSuffix;
            httpClient = CreateHttpClient(_baseAddress);
        }

        protected virtual HttpClient CreateHttpClient(string serviceBaseAddress)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(serviceBaseAddress);
            httpClient.Timeout = new TimeSpan(0, 0, 30);
            return httpClient;
        }

        public async Task<IEnumerable<TEntityDTO>?> GetAllAsync()
        {
            var response = await httpClient.GetAsync($"{_addressSuffix}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<TEntityDTO>>();
            }
            else return null;
        }

        public async Task<(IEnumerable<TEntityDTO>?, PaginationMetadata?)> GetAllAsync(string? searchValue, int pageNumber = 1, int pageSize = 10)
        {
            var response = await httpClient.GetAsync($"{_addressSuffix}/paged?searchValue={searchValue}&pageNumber={pageNumber}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var collectionToReturn = await response.Content.ReadFromJsonAsync<IEnumerable<TEntityDTO>>();
                PaginationMetadata? paginationMetadata = default;
                if (response.Headers.Contains("X-Pagination"))
                {
                    paginationMetadata = JsonSerializer.Deserialize<PaginationMetadata>(response.Headers.GetValues("X-Pagination").First());
                }
                return (collectionToReturn, paginationMetadata);
            }
            else return (null, null);
        }

        public async Task<TEntityDTO?> GetByIdAsync(TIdentifier identifier)
        {
            var response = await httpClient.GetAsync($"{_addressSuffix}/{identifier?.ToString()}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<TEntityDTO>();
            else return null;
        }

        public async Task<bool> CreateAsync(TEntityForCreationDTO entity)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(
                _addressSuffix, entity);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(TEntityForUpdateDTO entity)
        {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync(
                $"{_addressSuffix}/{entity.Id}", entity);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(TIdentifier identifier)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(
                $"{_addressSuffix}/{identifier?.ToString()}");
            return response.IsSuccessStatusCode;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                if (httpClient != null)
                {
                    httpClient.Dispose();
                }
                disposed = true;
            }
        }
    }
}