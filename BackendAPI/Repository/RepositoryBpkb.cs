using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Repository
{
    public class RepositoryBpkb : IRepositoryBpkb
    {
        private readonly MasterDBContext _dbContext;
        public RepositoryBpkb(MasterDBContext context)
        {
            _dbContext = context;
        }

        public async Task AddTaskAsync(TrBpkb bpkb)
        {
            var newData = new TrBpkb
            {
                Agreementnumber = bpkb.Agreementnumber,
                BpkbNo = bpkb.BpkbNo,
                BranchId = bpkb.BranchId,
                BpkbDate = bpkb.BpkbDate,
                FakturNo = bpkb.FakturNo,
                FakturDate = bpkb.FakturDate,
                LocationId = bpkb.LocationId,
                PoliceNo = bpkb.PoliceNo,
                BpkbDateIn = bpkb.BpkbDateIn,
                CreatedBy = bpkb.CreatedBy,
                CreatedOn = DateTime.Now
            };
            _dbContext.TrBpkbs.Add(newData);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(string uId)
        {
            var bpkb = await _dbContext.TrBpkbs.FirstOrDefaultAsync(u=>u.Agreementnumber  == uId);
            if (bpkb == null)
            {
                return;
            }
            _dbContext.TrBpkbs.Remove(bpkb);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TrBpkb> GetTaskByIdAsync(string uId)
        {
            var bpkb = await _dbContext.TrBpkbs.FirstOrDefaultAsync(u => u.Agreementnumber == uId);
            if(bpkb == null)
            {
                return null; 
            }
            return new TrBpkb
            {
                Agreementnumber = bpkb.Agreementnumber,
                BpkbNo = bpkb.BpkbNo,
                BranchId = bpkb.BranchId,
                BpkbDate = bpkb.BpkbDate,
                FakturNo = bpkb.FakturNo,
                FakturDate = bpkb.FakturDate,
                LocationId = bpkb.LocationId,
                PoliceNo = bpkb.PoliceNo,
                BpkbDateIn = bpkb.BpkbDateIn,
                CreatedBy = bpkb.CreatedBy,
                CreatedOn = DateTime.Now
            };
        }

        public async Task<IEnumerable<TrBpkb>> GetTasksAsync()
        {
            var bpkbs = await _dbContext.TrBpkbs.ToListAsync();
            return bpkbs.Select(bpkb => new TrBpkb
            {
                Agreementnumber = bpkb.Agreementnumber,
                BpkbNo = bpkb.BpkbNo,
                BranchId = bpkb.BranchId,
                BpkbDate = bpkb.BpkbDate,
                FakturNo = bpkb.FakturNo,
                FakturDate = bpkb.FakturDate,
                LocationId = bpkb.LocationId,
                PoliceNo = bpkb.PoliceNo,
                BpkbDateIn = bpkb.BpkbDateIn,
                CreatedBy = bpkb.CreatedBy,
                CreatedOn = DateTime.Now
            });
        }

        public async Task UpdateTaskAsync(string uId, TrBpkb bpkb)
        {
            var existing = await _dbContext.TrBpkbs.FindAsync(uId);
            if(existing == null) {
                return; 
            }
            existing.Agreementnumber = bpkb.Agreementnumber;
            existing.BpkbNo = bpkb.BpkbNo;
            existing.BranchId = bpkb.BranchId;
            existing.BpkbDate = bpkb.BpkbDate;
            existing.FakturNo = bpkb.FakturNo;
            existing.FakturDate = bpkb.FakturDate;
            existing.LocationId = bpkb.LocationId;
            existing.PoliceNo = bpkb.PoliceNo;
            existing.BpkbDateIn = bpkb.BpkbDateIn;
            existing.CreatedBy = bpkb.CreatedBy;
            existing.CreatedOn = DateTime.Now;
            existing.LastUpdatedBy = bpkb.LastUpdatedBy;
            existing.LastUpdatedOn = DateTime.Now;
            _dbContext.TrBpkbs.Update(existing);
            await _dbContext.SaveChangesAsync();

        }
        public async Task<IEnumerable<MsStorageLocation>> GetDropdown()
        {
            var locationstorage = await _dbContext.MsStorageLocations.ToListAsync();
            return locationstorage.Select(loc => new MsStorageLocation
            {
                LocationId = loc.LocationId,
                LocationName = loc.LocationName,
            });
        }
    }
    
}
