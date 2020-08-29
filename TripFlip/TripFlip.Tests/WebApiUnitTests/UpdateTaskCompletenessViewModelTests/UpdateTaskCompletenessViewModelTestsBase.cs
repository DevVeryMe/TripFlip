using TripFlip.ViewModels.TaskViewModels;

namespace WebApiUnitTests.UpdateTaskCompletenessViewModelTests
{
    public abstract class UpdateTaskCompletenessViewModelTestsBase
    {
        /// <summary>
        /// Creates UpdateTaskCompletenessViewModel object with given Id and Completeness.
        /// </summary>
        /// <param name="id">Task id.</param>
        /// <param name="isCompleted">Task completeness</param>
        /// <returns>Created UpdateTaskCompletenessViewModel object.</returns>
        protected static UpdateTaskCompletenessViewModel BuildUpdateTaskCompletenessViewModel(int id = 3,
            bool isCompleted = false)
        {
            var updateTaskCompletenessViewModel = new UpdateTaskCompletenessViewModel()
            {
                Id = id,
                IsCompleted = isCompleted
            };

            return updateTaskCompletenessViewModel;
        }
    }
}
