using System;
using System.Collections.Generic;
using TripFlip.ViewModels.TaskViewModels;

namespace TripFlip.ViewModels.TaskListViewModels
{
    public class TaskListWithTasksViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<TaskWithAssigneesViewModel> Tasks { get; set; }
    }
}
