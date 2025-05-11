using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dz_oop
{
    public partial class Form1 : Form, ITaskView
    {
        private List<TaskItem> _tasks = new List<TaskItem>();
        private const int CHECKBOX_AREA_WIDTH = 20; 

        public Form1()
        {
            InitializeComponent();
            new TaskPresenter(this);
           
            tasksListBox.CheckOnClick = false; 
            tasksListBox.MouseDown += TasksListBox_MouseDown;
            tasksListBox.SelectionMode = SelectionMode.One;
        }

        public string TaskDescription => taskDescriptionTextBox.Text;
        public event EventHandler AddTaskClicked;
        public event Action<TaskItem> ToggleTaskStatus;
        public event Action<TaskItem> DeleteTaskRequested;

        public void ClearInput() => taskDescriptionTextBox.Clear();

        public void DisplayTasks(List<TaskItem> tasks)
        {
            _tasks = tasks;
            tasksListBox.BeginUpdate();
            tasksListBox.Items.Clear();
            foreach (var task in tasks)
            {
                tasksListBox.Items.Add(task.Description, task.IsCompleted);
            }
            tasksListBox.EndUpdate();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddTaskClicked?.Invoke(this, EventArgs.Empty);
        }

        private void TasksListBox_MouseDown(object sender, MouseEventArgs e)
        {
            int index = tasksListBox.IndexFromPoint(e.Location);
            if (index >= 0 && index < _tasks.Count)
            {
                if (e.X < CHECKBOX_AREA_WIDTH)
                {
                    var task = _tasks[index];
                    task.ToggleStatus();
                    ToggleTaskStatus?.Invoke(task);
                    tasksListBox.SetItemChecked(index, task.IsCompleted);
                }
                else
                {
                    tasksListBox.SelectedIndex = index;
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (tasksListBox.SelectedIndex != -1)
            {
                var task = _tasks[tasksListBox.SelectedIndex];
                DeleteTaskRequested?.Invoke(task);
            }
        }
    }
}
