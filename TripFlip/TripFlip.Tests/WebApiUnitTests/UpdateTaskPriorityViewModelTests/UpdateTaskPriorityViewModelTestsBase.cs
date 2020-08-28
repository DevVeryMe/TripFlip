using TripFlip.ViewModels.Enums;
using TripFlip.ViewModels.TaskViewModels;

namespace WebApiUnitTests.UpdateTaskPriorityViewModelTests
{
    public abstract class UpdateTaskPriorityViewModelTestsBase
    {
        /// <summary>
        /// Creates UpdateTaskViewModel object with given Id and Priority level.
        /// </summary>
        /// <param name="id">Task id.</param>
        /// <param name="priorityLevel">Task priority level.</param>
        /// <returns>Created UpdateTaskViewModel object.</returns>
        protected static UpdateTaskPriorityViewModel BuildUpdateTaskViewModel(int id = 3,
            int priorityLevel = (int)TaskPriorityLevel.Low)
        {
            var updateTaskPriorityViewModel = new UpdateTaskPriorityViewModel()
            {
                Id = id,
                PriorityLevel = (TaskPriorityLevel)priorityLevel,
            };

            return updateTaskPriorityViewModel;
        }
    }
}
