using Domain.Common;
using Domain.Timesheets.DTO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace RazorClassLibrary.Services
{
    public class TimesheetService : EntityService<TimesheetDTO, TimesheetForCreationDTO, TimesheetForUpdateDTO, int>, ITimesheetService
    {
        public TimesheetService(string baseAddress, string addressSuffix, IIdentityService identityService) : base(baseAddress, addressSuffix, identityService)
        {
        }

        public async Task<DateTime> GetPeriodStartDate(DateTime periodDate)
        {
            using (var httpClient = await CreateHttpClient(_baseAddress))
            {
                var response = await httpClient.GetAsync($"api/timesheets/getperiodstartdate?periodDate={periodDate}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<DateTime>();
                }
                return default;
            }
        }

        public async Task<DateTime> GetPeriodEndDate(DateTime periodDate)
        {
            using (var httpClient = await CreateHttpClient(_baseAddress))
            {
                var response = await httpClient.GetAsync($"api/timesheets/getperiodenddate?periodDate={periodDate}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<DateTime>();
                }
                return default;
            }
        }

        public async Task<string> GetNewTimesheetNumber()
        {
            using (var httpClient = await CreateHttpClient(_baseAddress))
            {
                var response = await httpClient.GetAsync($"api/timesheets/getnewtimesheetnumber");
                if (response.IsSuccessStatusCode)
                {
                    return (await response.Content.ReadAsStringAsync())!;
                }
                return default!;
            }
        }

        public async Task<bool> CreateAsync(TimesheetForCreationDTO entity, string action = default!)
        {
            using (var httpClient = await CreateHttpClient(_baseAddress))
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(
                    $"api/timesheets?action={action}", entity);
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> UpdateAsync(TimesheetForUpdateDTO entity, string action = default!)
        {
            using (var httpClient = await CreateHttpClient(_baseAddress))
            {
                HttpResponseMessage response = await httpClient.PutAsJsonAsync(
                    $"api/timesheets/{entity.Id}?action={action}", entity);
                return response.IsSuccessStatusCode;
            }
        }

    }
}