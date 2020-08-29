using TripFlip.ViewModels.Enums;
using TripFlip.ViewModels.TaskViewModels;

namespace WebApiUnitTests.CreateTaskViewModelTests
{
    public abstract class CreateTaskViewModelTestsBase
    {
        /// <summary>
        /// Creates CreateTaskViewModel object with given Description, 
        /// Priority level and Task list id.
        /// </summary>
        /// <param name="description">Task description.</param>
        /// <param name="priorityLevel">Task priority level.</param>
        /// <param name="taskListId">Task list id</param>
        /// <returns>Created CreateTaskViewModel object.</returns>
        protected static CreateTaskViewModel BuildCreateTaskViewModel(
            string description = "Default",
            int priorityLevel = (int)TaskPriorityLevel.Low,
            int taskListId = 3)
        {
            var createTaskViewModel = new CreateTaskViewModel()
            {
                Description = description,
                PriorityLevel = (TaskPriorityLevel)priorityLevel,
                TaskListId = taskListId
            };

            return createTaskViewModel;
        }
    }
}
