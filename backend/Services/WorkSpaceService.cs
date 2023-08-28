using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.Data.Repositories;
using backend.Models.Dtos.UserWorkSpace;
using backend.Models.Entities;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class WorkSpaceService: IWorkSpaceService
    {
        private readonly IWorkSpaceRepository _workspaceRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public WorkSpaceService(IWorkSpaceRepository workspaceRepo, IMapper mapper, IUserRepository userRepo)
        {
            _workspaceRepo = workspaceRepo;
            _mapper = mapper;
            _userRepo = userRepo;
        }

        public async Task<List<WorkSpaceResult>> GetAllAsync(){
            return _mapper.Map<List<WorkSpaceResult>>(await _workspaceRepo.GetAll());
        }

        public async Task<WorkSpaceResult> GetByIdAsync(Guid id){
            return _mapper.Map<WorkSpaceResult>(await _workspaceRepo.GetById(id));
        }

        public async Task<Guid?> CreateAsync(AddWorkSpace model, string username){
            var entity = _mapper.Map<UserWorkSpace>(model);
            var user = await _userRepo.GetByUserNameAsync(username);
            if(!await _userRepo.IsExist(user.Id)){
                return null;
            }
            entity.OwnerId = user.Id;
            return await _workspaceRepo.Add(entity);
        }

        public async Task<bool> UpdateAsync(EditWorkSpace model){
            if(!await _workspaceRepo.IsExist(model.Id.ToString())) return false;
            var entityNew = _mapper.Map<UserWorkSpace>(model);
            await _workspaceRepo.Update(entityNew);

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id){
            if(!await _workspaceRepo.IsExist(id.ToString())) return false;
            await _workspaceRepo.Remove(await _workspaceRepo.GetById(id));

            return true;
        }

        public async Task<List<WorkSpaceResult>> GetByUserAsync(string id)
        {
            return _mapper.Map<List<WorkSpaceResult>>(await _workspaceRepo.GetUserWorkSpaces(id));
        }
    }
}