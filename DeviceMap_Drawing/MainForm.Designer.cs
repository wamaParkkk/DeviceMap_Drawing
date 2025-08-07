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
            this.btnWaferMapDraw = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnWaferMapDraw
            // 
            this.btnWaferMapDraw.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnWaferMapDraw.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWaferMapDraw.ForeColor = System.Drawing.Color.Navy;
            this.btnWaferMapDraw.Location = new System.Drawing.Point(0, 757);
            this.btnWaferMapDraw.Name = "btnWaferMapDraw";
            this.btnWaferMapDraw.Size = new System.Drawing.Size(630, 50);
            this.btnWaferMapDraw.TabIndex = 4;
            this.btnWaferMapDraw.Text = "Wafer Map Draw";
            this.btnWaferMapDraw.UseVisualStyleBackColor = true;
            this.btnWaferMapDraw.Click += new System.EventHandler(this.btnWaferMapDraw_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(630, 807);
            this.Controls.Add(this.btnWaferMapDraw);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainForm";
            this.Text = "Wafer map parameter";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnWaferMapDraw;
    }
}

