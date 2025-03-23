using DevicesBackend.Models;
using DevicesBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DevicesBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly DeviceService _deviceService;

        public DevicesController(DeviceService deviceService) 
        {
            _deviceService = deviceService;
        }

        [HttpGet("/api/devices")]
        public async Task<IActionResult> GetAllDevices()
        {
            List<Device> devices = await _deviceService.GetTopDevices();
            return Ok(devices);
        }

        [HttpPost("/api/devices/{id}")]
        public async Task<IActionResult> AddNewDevice(int id, [FromBody] JObject createDeviceRequest)
        {
            string? deviceType = createDeviceRequest.GetValue("type")?.ToString();

            if(string.IsNullOrEmpty(deviceType))
            {
                return BadRequest("Type of device cannot be null or empty");
            }

            Device newDevice = new Device 
            { 
                Id = id, 
                Type = deviceType 
            };

            try
            {
                await _deviceService.AddDevice(newDevice);
            }
            catch (ArgumentException ex)
            {
                return Conflict($"Device with id = {id} already added");
            }
            
            return Ok();
        }

        [HttpDelete("/api/devices/{id}")]
        public async Task<IActionResult> DeleteDeviceById(int id)
        {
            await _deviceService.RemoveDeviceWithId(id);
            return Ok();
        }

        [HttpDelete("/api/devices/type/{type}")]
        public async Task<IActionResult> DeleteDeviceByType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return BadRequest("Type of device cannot be null or empty");
            }
            else
            {
                await _deviceService.RemoveDevicesWithType(type);
                return Ok();
            }
        }
    }
}
