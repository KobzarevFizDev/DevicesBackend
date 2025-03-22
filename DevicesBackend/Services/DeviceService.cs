using DevicesBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace DevicesBackend.Services
{
    public class DeviceService
    {
        private readonly ApplicationDbContext _dbContext;

        public DeviceService(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<List<Device>> GetTopDevices(int maxDevices = 100)
        {
            return await _dbContext.Devices.Take(maxDevices).ToListAsync();
        }

        public async Task AddDevice(Device newDevice)
        {
            Device? device = await _dbContext.Devices.FirstOrDefaultAsync(d => d.Id == newDevice.Id);
            if (device != null)
                throw new ArgumentException($"Device with id = {newDevice.Id} already exist");

            await _dbContext.Devices.AddAsync(newDevice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveDeviceWithId(int id)
        {
            Device? device = await _dbContext.Devices.FirstOrDefaultAsync(d => d.Id == id);
            if (device == null)
                return;

            _dbContext.Devices.Remove(device);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveDeviceWithType(string type)
        {
            Device? device = await _dbContext.Devices.FirstOrDefaultAsync(d => d.Type == type);
            if (device == null)
                return;

            _dbContext.Devices.Remove(device);
            await _dbContext.SaveChangesAsync();
        }
    }
}
