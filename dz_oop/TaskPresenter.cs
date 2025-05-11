using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz_oop
{
    public class TaskPresenter
    {
        private readonly ITaskView _view;
        private readonly List<TaskItem> _tasks = new List<TaskItem>();

        public TaskPresenter(ITaskView view)
        {
            _view = view;
            _view.AddTaskClicked += OnAddTask;
            _view.ToggleTaskStatus += OnToggleTaskStatus;
            _view.DeleteTaskRequested += OnDeleteTask;
        }

        private void OnAddTask(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_view.TaskDescription))
            {
                var task = new TaskItem(_view.TaskDescription);
                task.StatusChanged += (t, status) => _view.DisplayTasks(_tasks);
                _tasks.Add(task);
                _view.ClearInput();
                _view.DisplayTasks(_tasks);
            }
        }

        private void OnToggleTaskStatus(TaskItem task)
        {
            _view.DisplayTasks(_tasks);
        }

        private void OnDeleteTask(TaskItem task)
        {
            _tasks.Remove(task);
            _view.DisplayTasks(_tasks);
        }
    }
}