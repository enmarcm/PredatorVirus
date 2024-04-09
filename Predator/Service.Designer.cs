namespace Predator
{
    partial class Service
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TimerSearch = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.TimerSearch)).BeginInit();
            // 
            // TimerSearch
            // 
            this.TimerSearch.Enabled = true;
            this.TimerSearch.Elapsed += new System.Timers.ElapsedEventHandler(this.TimerSearch_Elapsed);
            // 
            // Service
            // 
            this.ServiceName = "Service";
            ((System.ComponentModel.ISupportInitialize)(this.TimerSearch)).EndInit();

        }

        #endregion

        private System.Timers.Timer TimerSearch;
    }
}
