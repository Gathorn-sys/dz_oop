using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz_oop
{
    public delegate void StatusChangedEventHandler(TaskItem sender, bool newStatus);

    public class TaskItem
    {
        public string Description { get; }
        public bool IsCompleted { get; set; } 

        public event StatusChangedEventHandler StatusChanged;

        public TaskItem(string description)
        {
            Description = description;
            IsCompleted = false;
        }

        public void ToggleStatus()
        {
            IsCompleted = !IsCompleted;
            StatusChanged?.Invoke(this, IsCompleted);
        }
    }
}