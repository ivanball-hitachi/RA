using Domain.Common;
using System.Net.Http.Json;
using System.Text.Json;

namespace RazorClassLibrary.Services
{
    public class EntityService<TEntityDTO, TEntityForCreationDTO, TEntityForUpdateDTO, TIdentifier> : IEntityService<TEntityDTO, TEntityForCreationDTO, TEntityForUpdateDTO, TIdentifier>
        where TEntityDTO : AuditableDTO, IBaseEntity<TIdentifier>
        where TEntityForCreationDTO : AuditableDTO
        where TEntityForUpdateDTO : AuditableDTO, IBaseEntity<TIdentifier>
    {
        protected readonly string _baseAddress;
        private readonly string _addressSuffix;
        private readonly IIdentityService _identityService;

        public EntityService(string baseAddress, string addressSuffix, IIdentityService identityService)
        {
            _baseAddress = baseAddress;
            _addressSuffix = addressSuffix;
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        protected async Task<HttpClient> CreateHttpClient(string serviceBaseAddress)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(serviceBaseAddress);
            httpClient.Timeout = new TimeSpan(0, 0, 30);
            string token = await _identityService.GetToken();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            return httpClient;
        }

        public async Task<IEnumerable<TEntityDTO>?> GetAllAsync()
        {
            using (var httpClient = await CreateHttpClient(_baseAddress))
            {
                var response = await httpClient.GetAsync($"{_addressSuffix}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<TEntityDTO>>();
                }
                else return null;
            }
        }

        public async Task<(IEnumerable<TEntityDTO>?, PaginationMetadata?)> GetAllAsync(string? searchValue, int pageNumber = 1, int pageSize = 10)
        {
            using (var httpClient = await CreateHttpClient(_baseAddress))
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
        }

        public async Task<TEntityDTO?> GetByIdAsync(TIdentifier identifier)
        {
            using (var httpClient = await CreateHttpClient(_baseAddress))
            {
                var response = await httpClient.GetAsync($"{_addressSuffix}/{identifier?.ToString()}");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<TEntityDTO>();
                else return null;
            }
        }

        public async Task<bool> CreateAsync(TEntityForCreationDTO entity)
        {
            using (var httpClient = await CreateHttpClient(_baseAddress))
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(
                    _addressSuffix, entity);
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> UpdateAsync(TEntityForUpdateDTO entity)
        {
            using (var httpClient = await CreateHttpClient(_baseAddress))
            {
                HttpResponseMessage response = await httpClient.PutAsJsonAsync(
                    $"{_addressSuffix}/{entity.Id}", entity);
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> DeleteAsync(TIdentifier identifier)
        {
            using (var httpClient = await CreateHttpClient(_baseAddress))
            {
                HttpResponseMessage response = await httpClient.DeleteAsync(
                    $"{_addressSuffix}/{identifier?.ToString()}");
                return response.IsSuccessStatusCode;
            }
        }
    }
}