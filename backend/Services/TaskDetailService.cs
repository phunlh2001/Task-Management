using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.Data.Repositories;
using backend.Models.Dtos.TaskDetail;
using backend.Models.Entities;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class TaskDetailService: ITaskDetailService
    {
        private readonly ITaskListRepository _listRepo;
        private readonly ITaskDetailRepository _taskRepo;
        private readonly IMapper _mapper;

        public TaskDetailService(ITaskListRepository listRepo, IMapper mapper, ITaskDetailRepository taskRepo)
        {
            _listRepo = listRepo;
            _mapper = mapper;
            _taskRepo = taskRepo;
        }

        public async Task<List<TaskDetailResult>> GetAllAsync(){
            return _mapper.Map<List<TaskDetailResult>>(await _taskRepo.GetAll());
        }

        public async Task<TaskDetailResult> GetByIdAsync(Guid id){
            return _mapper.Map<TaskDetailResult>(await _taskRepo.GetById(id));
        }

        public async Task<Guid?> CreateAsync(AddTaskDetail model){
            var entity = _mapper.Map<TaskDetail>(model);
            if(!await _listRepo.IsExist(model.ListId)){
                return null;
            }
            entity.Order = (await _taskRepo.Search(e=>e.ListId == model.ListId)).Count()+1;
            return await _taskRepo.Add(entity);
        }

        public async Task<bool> UpdateAsync(EditTaskDetail model){
            if(!await _taskRepo.IsExist(model.Id)
            || !await _listRepo.IsExist(model.ListId)) return false;
            var entityNew = _mapper.Map<TaskDetail>(model);
            await _taskRepo.Update(entityNew);

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id){
            if(!await _taskRepo.IsExist(id)) return false;
            await _taskRepo.Remove(await _taskRepo.GetById(id));

            return true;
        }


        public async Task<List<TaskDetailResult>> GetByListAsync(Guid id)
        {
            return _mapper.Map<List<TaskDetailResult>>(await _taskRepo.GetByList(id));
        }
    }
}