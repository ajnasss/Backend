using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FountainController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _dbContext;
        private const string D1MiniEndpoint = "http://<D1_MINI_IP_ADDRESS>/control";

        public FountainController(HttpClient httpClient, AppDbContext dbContext)
        {
            _httpClient = httpClient;
            _dbContext = dbContext;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartFountain()
        {
            var response = await SendCommandToD1Mini("start");
            if (response.IsSuccessStatusCode)
                return Ok("Fountain started successfully.");
            return StatusCode((int)response.StatusCode, "Failed to start the fountain.");
        }

        [HttpPost("stop")]
        public async Task<IActionResult> StopFountain()
        {
            var response = await SendCommandToD1Mini("stop");
            if (response.IsSuccessStatusCode)
                return Ok("Fountain stopped successfully.");
            return StatusCode((int)response.StatusCode, "Failed to stop the fountain.");
        }

        [HttpPost("schedule")]
        public async Task<IActionResult> ScheduleFountain([FromBody] ScheduleRequest request)
        {
            var schedule = new FountainSchedule
            {
                StartTime = request.StartTime,
                EndTime = request.EndTime
            };

            _dbContext.FountainSchedules.Add(schedule);
            await _dbContext.SaveChangesAsync();

            return Ok($"Fountain scheduled from {request.StartTime} to {request.EndTime}.");
        }

        private async Task<HttpResponseMessage> SendCommandToD1Mini(string command)
        {
            var requestUri = $"{D1MiniEndpoint}?command={command}";
            return await _httpClient.GetAsync(requestUri);
        }
    }
}
