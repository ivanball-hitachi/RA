using System.Net.Http.Json;
using System.Text.Json;
using System.Xml.Linq;
using Domain.Common;
using Domain.Timesheets.DTO;
using Microsoft.Extensions.Configuration;

namespace RazorClassLibrary.Services;

public class TimesheetLineService : ITimesheetLineService
{
    HttpClient httpClient;
    public TimesheetLineService(IConfiguration configuration)
    {
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(configuration["APIBaseURL"]);
        httpClient.Timeout = new TimeSpan(0, 0, 30);
    }

    public async Task<(List<TimesheetLineDTO>?, PaginationMetadata?)> GetTimesheetLinesAsync(int timesheetId, string? searchValue = default, int pageNumber = 1, int pageSize = 10)
    {
        var response = await httpClient.GetAsync($"api/timesheets/{timesheetId}/timesheetlines/paged?searchValue={searchValue}&pageNumber={pageNumber}&pageSize={pageSize}");
        if (response.IsSuccessStatusCode)
        {
            var collectionToReturn = await response.Content.ReadFromJsonAsync<List<TimesheetLineDTO>>();
            PaginationMetadata? paginationMetadata = default;
            if (response.Headers.Contains("X-Pagination"))
            {
                paginationMetadata = JsonSerializer.Deserialize<PaginationMetadata>(response.Headers.GetValues("X-Pagination").First());
            }
            return (collectionToReturn, paginationMetadata);
        }
        else return (null, null);
    }

    public async Task<TimesheetLineDTO?> GetTimesheetLineByIdAsync(int timesheetId, int timesheetLineId, string? searchValueForTimesheetLineDetails = default)
    {
        TimesheetLineDTO? timesheetLine = null;
        var response = await httpClient.GetAsync($"api/timesheets/{timesheetId}/timesheetlines/{timesheetLineId}?searchValue={searchValueForTimesheetLineDetails}");
        if (response.IsSuccessStatusCode)
        {
            timesheetLine = await response.Content.ReadFromJsonAsync<TimesheetLineDTO>();
        }
        return timesheetLine;
    }

    public async Task<bool> AddTimesheetLineAsync(int timesheetId, TimesheetLineForCreationDTO timesheetLine)
    {
        HttpResponseMessage response = await httpClient.PostAsJsonAsync(
            $"api/timesheets/{timesheetId}/timesheetlines", timesheetLine);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateTimesheetLineAsync(int timesheetId, TimesheetLineForUpdateDTO timesheetLine)
    {
        HttpResponseMessage response = await httpClient.PutAsJsonAsync(
            $"api/timesheets/{timesheetId}/timesheetlines/{timesheetLine.Id}", timesheetLine);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteTimesheetLineAsync(int timesheetId, int timesheetLineId)
    {
        HttpResponseMessage response = await httpClient.DeleteAsync(
            $"api/timesheets/{timesheetId}/timesheetlines/{timesheetLineId}");
        return response.IsSuccessStatusCode;
    }
}
