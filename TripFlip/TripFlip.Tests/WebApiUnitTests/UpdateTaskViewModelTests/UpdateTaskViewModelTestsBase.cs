using TripFlip.ViewModels.Enums;
using TripFlip.ViewModels.TaskViewModels;

namespace WebApiUnitTests.UpdateTaskViewModelTests
{
    public abstract class UpdateTaskViewModelTestsBase
    {
        /// <summary>
        /// Creates UpdateTaskViewModel object with given Id, 
        /// Description, Priority level and Completeness.
        /// </summary>
        /// <param name="id">Task id.</param>
        /// <param name="description">Task description.</param>
        /// <param name="priorityLevel">Task priority level.</param>
        /// <param name="isCompleted">Task completeness</param>
        /// <returns>Created UpdateTaskViewModel object.</returns>
        protected static UpdateTaskViewModel BuildUpdateTaskViewModel(int id = 3,
            string description = "Default",
            int priorityLevel = (int)TaskPriorityLevel.Low,
            bool isCompleted = false)
        {
            var updateTaskViewModel = new UpdateTaskViewModel()
            {
                Id = id,
                Description = description,
                PriorityLevel = (TaskPriorityLevel)priorityLevel,
                IsCompleted = isCompleted
            };

            return updateTaskViewModel;
        }
    }
}
