using System;
using System.Collections.Generic;
using TripFlip.ViewModels.TaskViewModels;

namespace TripFlip.ViewModels.TaskListViewModels
{
    public class TaskListWithIncludesViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<TaskViewModel> Tasks { get; set; }
    }
}
