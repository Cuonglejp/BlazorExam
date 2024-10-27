using BlazorExam.Models;

namespace BlazorExam.Services
{
    public class TaskSessionService
    {
        private List<TaskModel> displayTasks = new List<TaskModel>();

        private bool isFirstLoaded = false;
        private bool isChanged = false;

        public bool IsStated() => isFirstLoaded || !isChanged;

        public async Task<List<TaskModel>> GetTasksAsync()
        {
            //First load
            if(isFirstLoaded == false)
            {
                //First load from DB など
                //displayTasks = GetDBAsync()
                //............
                await Task.Delay(500);

                // 3件のタスクを追加
                displayTasks.Add(new TaskModel
                {
                    Title = "タスク1",
                    DueDate = DateTime.Now.AddDays(1),
                    Status =  TaskModel.TaskModelStatus.未着手,
                    Content = "タスク1の内容です。"
                });

                displayTasks.Add(new TaskModel
                {
                    Title = "タスク2",
                    DueDate = DateTime.Now.AddDays(-2),
                    Status = TaskModel.TaskModelStatus.仕掛中,
                    Content = "タスク2の内容です。\n内容の2行目です。"
                });

                displayTasks.Add(new TaskModel
                {
                    Title = "タスク3",
                    DueDate = DateTime.Now.AddDays(3),
                    Status = TaskModel.TaskModelStatus.完了,
                    Content = "タスク3の内容です。"
                });

                isFirstLoaded = true;
            }

            //Changed 
            if(isChanged == true)
            {
                //Sort display task
                displayTasks.Sort((x, y) => x.CompareTo(y));

                isChanged = false;  
            }

            return displayTasks; 
        }

        public async Task AddTaskAsync(TaskModel task)
        {
            var intIndex = displayTasks.FindIndex(existTask => existTask.ID == task.ID);
            if (intIndex == -1)
            {
                displayTasks.Add(task);

                //Add to DB とか
                await Task.Delay(100);

                isChanged = true;
            }
            else
            {
                throw new Exception("Task ID is existed");
            }
        }

        public async Task EdittaskAsync(TaskModel task)
        {
            var intIndex = displayTasks.FindIndex(existTask => existTask.ID == task.ID);
            if (intIndex == -1)
            {
                throw new Exception("Task is not exist");
            }
            else
            {
                displayTasks[intIndex] = task;

                //Update to DB とか
                await Task.Delay(100);

                isChanged = true;
            }
        }

        public async Task DeletetaskAsync(TaskModel task)
        {
            var intIndex = displayTasks.FindIndex(existTask => existTask.ID == task.ID);
            if (intIndex == -1)
            {
                throw new Exception("Task is not exist");
            }
            else
            {
                displayTasks.RemoveAt(intIndex);

                //Delete to DB とか
                await Task.Delay(100);

                isChanged = true;
            }
        }
    }
}
