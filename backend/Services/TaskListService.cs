using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.Data.Repositories;
using backend.Models.Dtos.TaskList;
using backend.Models.Entities;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class TaskListService: ITaskListService
    {
        private readonly IWorkSpaceRepository _workspaceRepo;
        private readonly ITaskListRepository _listRepo;
        private readonly IMapper _mapper;

        public TaskListService(IWorkSpaceRepository workspaceRepo, IMapper mapper, ITaskListRepository listRepo)
        {
            _workspaceRepo = workspaceRepo;
            _mapper = mapper;
            _listRepo = listRepo;
        }

        public async Task<List<TaskListResult>> GetAllAsync(){
            return _mapper.Map<List<TaskListResult>>(await _listRepo.GetAll());
        }

        public async Task<TaskListResult> GetByIdAsync(Guid id){
            return _mapper.Map<TaskListResult>(await _listRepo.GetById(id));
        }

        public async Task<Guid?> CreateAsync(AddTaskList model){
            var entity = _mapper.Map<TaskList>(model);
            entity.Order = (await _listRepo.Search(e=>e.WorkSpaceId == model.WorkSpaceId)).Count()+1;
            if(!await _workspaceRepo.IsExist(model.WorkSpaceId)){
                return null;
            }
            return await _listRepo.Add(entity);
        }

        public async Task<bool> UpdateAsync(EditTaskList model){
            
            if(!await _listRepo.IsExist(model.Id)
            || !await _workspaceRepo.IsExist(model.WorkSpaceId)) return false;
            var entityNew = _mapper.Map<TaskList>(model);
            await _listRepo.Update(entityNew);

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id){
            if(!await _listRepo.IsExist(id)) return false;
            await _listRepo.Remove(await _listRepo.GetById(id));

            return true;
        }


        public async Task<List<TaskListResult>> GetByWorkSpaceAsync(Guid id)
        {
            return _mapper.Map<List<TaskListResult>>(await _listRepo.GetByWorkSpace(id));
        }
    }
}