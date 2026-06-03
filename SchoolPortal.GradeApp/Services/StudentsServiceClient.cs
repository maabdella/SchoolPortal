using System.Net.Http.Json;
using System.Text.Json;
using SchoolPortal.GradeApp.Models;

namespace SchoolPortal.GradeApp.Services;

public class StudentsServiceClient
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _httpClient;
    private readonly ILogger<StudentsServiceClient> _logger;

    public StudentsServiceClient(HttpClient httpClient, ILogger<StudentsServiceClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<StudentDto>?> GetAllStudents()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Student/GetAll");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning(
                    "Students API returned {StatusCode} from {Url}",
                    response.StatusCode,
                    _httpClient.BaseAddress + "api/Student/GetAll");
                return null;
            }

            return await response.Content.ReadFromJsonAsync<List<StudentDto>>(JsonOptions) ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogWarning(
                ex,
                "Could not reach Students service at {BaseAddress}. Is StudentApp running on port 5166?",
                _httpClient.BaseAddress);
            return null;
        }
    }

    public async Task<StudentDto?> GetStudentById(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Student/GetById?id={id}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<StudentDto>(JsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not get student {StudentId} from Students service", id);
            return null;
        }
    }
}
