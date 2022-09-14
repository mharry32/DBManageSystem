﻿using Ardalis.Result;
using DBManageSystem.Core.ProjectAggregate;

namespace DBManageSystem.Core.Interfaces;
public interface IToDoItemSearchService
{
  Task<Result<ToDoItem>> GetNextIncompleteItemAsync(int projectId);
  Task<Result<List<ToDoItem>>> GetAllIncompleteItemsAsync(int projectId, string searchString);
}
