namespace Ui
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_load = new System.Windows.Forms.Button();
            this.chList_LoadedFiles = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.btn_PlayAll = new System.Windows.Forms.Button();
            this.btn_AddInQueue = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btn_StopQueue = new System.Windows.Forms.Button();
            this.btn_EraseQueue = new System.Windows.Forms.Button();
            this.btn_Canselation = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chList_LoadedFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_load
            // 
            this.btn_load.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_load.Location = new System.Drawing.Point(22, 27);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(199, 53);
            this.btn_load.TabIndex = 0;
            this.btn_load.Text = "Load Wav";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // chList_LoadedFiles
            // 
            this.chList_LoadedFiles.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chList_LoadedFiles.Appearance.Options.UseFont = true;
            this.chList_LoadedFiles.Location = new System.Drawing.Point(22, 102);
            this.chList_LoadedFiles.Name = "chList_LoadedFiles";
            this.chList_LoadedFiles.Size = new System.Drawing.Size(481, 375);
            this.chList_LoadedFiles.TabIndex = 1;
            // 
            // btn_PlayAll
            // 
            this.btn_PlayAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_PlayAll.Location = new System.Drawing.Point(834, 27);
            this.btn_PlayAll.Name = "btn_PlayAll";
            this.btn_PlayAll.Size = new System.Drawing.Size(199, 53);
            this.btn_PlayAll.TabIndex = 2;
            this.btn_PlayAll.Text = "Play all";
            this.btn_PlayAll.UseVisualStyleBackColor = true;
            this.btn_PlayAll.Click += new System.EventHandler(this.btn_PlayAll_Click);
            // 
            // btn_AddInQueue
            // 
            this.btn_AddInQueue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_AddInQueue.Location = new System.Drawing.Point(512, 27);
            this.btn_AddInQueue.Name = "btn_AddInQueue";
            this.btn_AddInQueue.Size = new System.Drawing.Size(199, 53);
            this.btn_AddInQueue.TabIndex = 3;
            this.btn_AddInQueue.Text = "Add in Queue";
            this.btn_AddInQueue.UseVisualStyleBackColor = true;
            this.btn_AddInQueue.Click += new System.EventHandler(this.btn_AddInQueue_Click);
            // 
            // btnPause
            // 
            this.btnPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPause.Location = new System.Drawing.Point(512, 153);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(199, 53);
            this.btnPause.TabIndex = 4;
            this.btnPause.Text = "Pause Player";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btn_StopQueue
            // 
            this.btn_StopQueue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_StopQueue.Location = new System.Drawing.Point(834, 153);
            this.btn_StopQueue.Name = "btn_StopQueue";
            this.btn_StopQueue.Size = new System.Drawing.Size(199, 53);
            this.btn_StopQueue.TabIndex = 5;
            this.btn_StopQueue.Text = "StopQueue";
            this.btn_StopQueue.UseVisualStyleBackColor = true;
            this.btn_StopQueue.Click += new System.EventHandler(this.btn_StopQueue_Click);
            // 
            // btn_EraseQueue
            // 
            this.btn_EraseQueue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_EraseQueue.Location = new System.Drawing.Point(834, 212);
            this.btn_EraseQueue.Name = "btn_EraseQueue";
            this.btn_EraseQueue.Size = new System.Drawing.Size(199, 53);
            this.btn_EraseQueue.TabIndex = 6;
            this.btn_EraseQueue.Text = "EraseQueue";
            this.btn_EraseQueue.UseVisualStyleBackColor = true;
            // 
            // btn_Canselation
            // 
            this.btn_Canselation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Canselation.Location = new System.Drawing.Point(512, 212);
            this.btn_Canselation.Name = "btn_Canselation";
            this.btn_Canselation.Size = new System.Drawing.Size(199, 53);
            this.btn_Canselation.TabIndex = 7;
            this.btn_Canselation.Text = "Canselation";
            this.btn_Canselation.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 489);
            this.Controls.Add(this.btn_Canselation);
            this.Controls.Add(this.btn_EraseQueue);
            this.Controls.Add(this.btn_StopQueue);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btn_AddInQueue);
            this.Controls.Add(this.btn_PlayAll);
            this.Controls.Add(this.chList_LoadedFiles);
            this.Controls.Add(this.btn_load);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.chList_LoadedFiles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_load;
        private DevExpress.XtraEditors.CheckedListBoxControl chList_LoadedFiles;
        private System.Windows.Forms.Button btn_PlayAll;
        private System.Windows.Forms.Button btn_AddInQueue;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btn_StopQueue;
        private System.Windows.Forms.Button btn_EraseQueue;
        private System.Windows.Forms.Button btn_Canselation;
    }
}