﻿namespace Ui
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
            this.btn_AddInQueue = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btn_StopQueue = new System.Windows.Forms.Button();
            this.btn_EraseQueue = new System.Windows.Forms.Button();
            this.btn_Canselation = new System.Windows.Forms.Button();
            this.btn_StopPlayer = new System.Windows.Forms.Button();
            this.btn_ClearQueue = new System.Windows.Forms.Button();
            this.lw_QueueEvent = new System.Windows.Forms.ListView();
            this.btn_Filter = new System.Windows.Forms.Button();
            this.btn_getInfo = new System.Windows.Forms.Button();
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
            this.btn_StopQueue.Location = new System.Drawing.Point(834, 152);
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
            this.btn_EraseQueue.Location = new System.Drawing.Point(834, 276);
            this.btn_EraseQueue.Name = "btn_EraseQueue";
            this.btn_EraseQueue.Size = new System.Drawing.Size(199, 53);
            this.btn_EraseQueue.TabIndex = 6;
            this.btn_EraseQueue.Text = "EraseQueue";
            this.btn_EraseQueue.UseVisualStyleBackColor = true;
            this.btn_EraseQueue.Click += new System.EventHandler(this.btn_EraseQueue_Click);
            // 
            // btn_Canselation
            // 
            this.btn_Canselation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Canselation.Location = new System.Drawing.Point(512, 276);
            this.btn_Canselation.Name = "btn_Canselation";
            this.btn_Canselation.Size = new System.Drawing.Size(199, 53);
            this.btn_Canselation.TabIndex = 7;
            this.btn_Canselation.Text = "Canselation";
            this.btn_Canselation.UseVisualStyleBackColor = true;
            // 
            // btn_StopPlayer
            // 
            this.btn_StopPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_StopPlayer.Location = new System.Drawing.Point(512, 212);
            this.btn_StopPlayer.Name = "btn_StopPlayer";
            this.btn_StopPlayer.Size = new System.Drawing.Size(199, 53);
            this.btn_StopPlayer.TabIndex = 8;
            this.btn_StopPlayer.Text = "Stop Player";
            this.btn_StopPlayer.UseVisualStyleBackColor = true;
            this.btn_StopPlayer.Click += new System.EventHandler(this.btn_StopPlayer_Click);
            // 
            // btn_ClearQueue
            // 
            this.btn_ClearQueue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_ClearQueue.Location = new System.Drawing.Point(834, 212);
            this.btn_ClearQueue.Name = "btn_ClearQueue";
            this.btn_ClearQueue.Size = new System.Drawing.Size(199, 53);
            this.btn_ClearQueue.TabIndex = 9;
            this.btn_ClearQueue.Text = "ClearQueue";
            this.btn_ClearQueue.UseVisualStyleBackColor = true;
            this.btn_ClearQueue.Click += new System.EventHandler(this.btn_ClearQueue_Click);
            // 
            // lw_QueueEvent
            // 
            this.lw_QueueEvent.Location = new System.Drawing.Point(512, 428);
            this.lw_QueueEvent.Name = "lw_QueueEvent";
            this.lw_QueueEvent.Size = new System.Drawing.Size(521, 234);
            this.lw_QueueEvent.TabIndex = 10;
            this.lw_QueueEvent.UseCompatibleStateImageBehavior = false;
            // 
            // btn_Filter
            // 
            this.btn_Filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Filter.Location = new System.Drawing.Point(834, 335);
            this.btn_Filter.Name = "btn_Filter";
            this.btn_Filter.Size = new System.Drawing.Size(199, 53);
            this.btn_Filter.TabIndex = 11;
            this.btn_Filter.Text = "AplyFilterToQueue";
            this.btn_Filter.UseVisualStyleBackColor = true;
            this.btn_Filter.Click += new System.EventHandler(this.btn_Filter_Click);
            // 
            // btn_getInfo
            // 
            this.btn_getInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_getInfo.Location = new System.Drawing.Point(307, 599);
            this.btn_getInfo.Name = "btn_getInfo";
            this.btn_getInfo.Size = new System.Drawing.Size(199, 53);
            this.btn_getInfo.TabIndex = 12;
            this.btn_getInfo.Text = "Get info";
            this.btn_getInfo.UseVisualStyleBackColor = true;
            this.btn_getInfo.Click += new System.EventHandler(this.btn_getInfo_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 674);
            this.Controls.Add(this.btn_getInfo);
            this.Controls.Add(this.btn_Filter);
            this.Controls.Add(this.lw_QueueEvent);
            this.Controls.Add(this.btn_ClearQueue);
            this.Controls.Add(this.btn_StopPlayer);
            this.Controls.Add(this.btn_Canselation);
            this.Controls.Add(this.btn_EraseQueue);
            this.Controls.Add(this.btn_StopQueue);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btn_AddInQueue);
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
        private System.Windows.Forms.Button btn_AddInQueue;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btn_StopQueue;
        private System.Windows.Forms.Button btn_EraseQueue;
        private System.Windows.Forms.Button btn_Canselation;
        private System.Windows.Forms.Button btn_StopPlayer;
        private System.Windows.Forms.Button btn_ClearQueue;
        private System.Windows.Forms.ListView lw_QueueEvent;
        private System.Windows.Forms.Button btn_Filter;
        private System.Windows.Forms.Button btn_getInfo;
    }
}