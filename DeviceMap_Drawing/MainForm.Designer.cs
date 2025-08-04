namespace DeviceMap_Drawing
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PKG_X_Count_Nud = new System.Windows.Forms.NumericUpDown();
            this.PKG_Y_Count_Nud = new System.Windows.Forms.NumericUpDown();
            this.btnWaferMapDraw = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PKG_X_Count_Nud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PKG_Y_Count_Nud)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Azure;
            this.panel3.Controls.Add(this.PKG_Y_Count_Nud);
            this.panel3.Controls.Add(this.PKG_X_Count_Nud);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(12, 669);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(600, 130);
            this.panel3.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "X PKG 개수";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y PKG 개수";
            // 
            // PKG_X_Count_Nud
            // 
            this.PKG_X_Count_Nud.Location = new System.Drawing.Point(102, 20);
            this.PKG_X_Count_Nud.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.PKG_X_Count_Nud.Name = "PKG_X_Count_Nud";
            this.PKG_X_Count_Nud.Size = new System.Drawing.Size(100, 21);
            this.PKG_X_Count_Nud.TabIndex = 2;
            // 
            // PKG_Y_Count_Nud
            // 
            this.PKG_Y_Count_Nud.Location = new System.Drawing.Point(101, 60);
            this.PKG_Y_Count_Nud.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.PKG_Y_Count_Nud.Name = "PKG_Y_Count_Nud";
            this.PKG_Y_Count_Nud.Size = new System.Drawing.Size(100, 21);
            this.PKG_Y_Count_Nud.TabIndex = 3;
            // 
            // btnWaferMapDraw
            // 
            this.btnWaferMapDraw.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWaferMapDraw.ForeColor = System.Drawing.Color.Navy;
            this.btnWaferMapDraw.Location = new System.Drawing.Point(618, 669);
            this.btnWaferMapDraw.Name = "btnWaferMapDraw";
            this.btnWaferMapDraw.Size = new System.Drawing.Size(158, 130);
            this.btnWaferMapDraw.TabIndex = 4;
            this.btnWaferMapDraw.Text = "Wafer Map\r\nDraw";
            this.btnWaferMapDraw.UseVisualStyleBackColor = true;
            this.btnWaferMapDraw.Click += new System.EventHandler(this.btnWaferMapDraw_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1084, 811);
            this.Controls.Add(this.btnWaferMapDraw);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainForm";
            this.Text = "Wafer map parameter";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PKG_X_Count_Nud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PKG_Y_Count_Nud)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown PKG_Y_Count_Nud;
        private System.Windows.Forms.NumericUpDown PKG_X_Count_Nud;
        private System.Windows.Forms.Button btnWaferMapDraw;
    }
}

