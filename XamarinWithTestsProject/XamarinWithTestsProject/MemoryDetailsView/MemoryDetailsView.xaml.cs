using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinWithTestsProject.MemoryDetailsView
{
    public partial class MemoryDetailsView : ContentView
    {
        private readonly IMemoryService memory;

        public MemoryDetailsView()
        {
            InitializeComponent();

            memory = DependencyService.Get<IMemoryService>();
            RefreshScreen();
        }

        public double MemoryUsage { get { return memory.GetInfo().Usage(); } }

        protected void StartMeasuring(object sender, EventArgs e)
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(250), () => { RefreshScreen(); return true; });
        }
        protected void HandleClicked(object sender, EventArgs e)
        {
            RefreshScreen();
        }

        void RefreshScreen()
        {

            UsedMemory.Text = "";
            FreeMemory.Text = "";
            HeapMemory.Text = "";
            MaxMemory.Text = "";
            HeapUsage.Text = "";
            TotalUsage.Text = "";

            UsedMemory.TextColor = Color.Black;
            FreeMemory.TextColor = Color.Black;
            HeapMemory.TextColor = Color.Black;
            MaxMemory.TextColor = Color.Black;
            HeapUsage.TextColor = Color.Black;
            TotalUsage.TextColor = Color.Black;


            if (memory != null)
            {
                MemoryInfo info = memory.GetInfo();
                if (info != null)
                {
                    UsedMemory.Text = String.Format("{0:N}", info.UsedMemory);

                    FreeMemory.Text = String.Format("{0:N}", info.FreeMemory);

                    HeapMemory.Text = String.Format("{0:N}", info.TotalMemory);

                    MaxMemory.Text = String.Format("{0:N}", info.MaxMemory);
                    HeapUsage.Text = String.Format("{0:P}", info.HeapUsage());
                    TotalUsage.Text = String.Format("{0:P}", info.Usage());
                    
                    if (info.Usage() > 0.8)
                    {
                        FreeMemory.TextColor = Color.Red;
                        UsedMemory.TextColor = Color.Red;
                        TotalUsage.TextColor = Color.Red;
                    }
                }

            }
        }
    }
}
