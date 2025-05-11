using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz_oop
{
    public interface ITaskView
    {
        string TaskDescription { get; }
        void ClearInput();
        void DisplayTasks(List<TaskItem> tasks);

        event EventHandler AddTaskClicked;
        event Action<TaskItem> ToggleTaskStatus;
        event Action<TaskItem> DeleteTaskRequested;
    }
}
