using Microsoft.AspNetCore.Components;
using BlazorExam.Services;
using BlazorExam.Models;

namespace BlazorExam.Components.Pages
{
    public partial class TaskList
    {
        [Inject]
        protected NavigationManager? NavigationManager { get; set; }

        [Inject]
        protected TaskSessionService? TaskSessionService { get;set;}

        private List<TaskModel> tasks = new List<TaskModel>();
        private bool isStated = false;

        protected override async Task OnInitializedAsync()
        {
            tasks = await TaskSessionService?.GetTasksAsync()!;
            isStated = TaskSessionService.IsStated();
        }

        private void HandleAddTask()
        {
            NavigationManager?.NavigateTo("/addtask");
        }
    }
}