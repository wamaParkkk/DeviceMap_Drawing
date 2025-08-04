using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceMap_Drawing
{
    public partial class MainForm : Form
    {
        private readonly int panelLeft = 12;
        private readonly int panelTop = 12;
        private readonly int panelSize = 600;
        
        private readonly int chipSize = 40;
        private readonly int chipSpacing = 5;
        private readonly int[] chipCounts = { 3, 7, 9, 9, 11, 11, 11, 9, 9, 7, 3 };
        
        private List<Point> chipCenters = new List<Point>();
        private Dictionary<string, Point> guidePositions = new Dictionary<string, Point>();

        public MainForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Shown += MainForm_Shown;
        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {            
            await Task.Run(() =>
            {
                CalculateChipCenters();

                this.Invoke(new MethodInvoker(() =>
                {
                    Invalidate();           // OnPaint 강제 호출
                    Application.DoEvents(); // guidePosition 확정
                    AddGuideControls();     // Label + NumericUpDown 생성
                }));
            });
        }

        private void CalculateChipCenters()
        {
            try
            {
                chipCenters.Clear();

                int totalRows = chipCounts.Length;
                int totalHeight = totalRows * chipSize + (totalRows - 1) * chipSpacing;
                int startY = panelTop + (panelSize - totalHeight) / 2;

                for (int row = 0; row < totalRows; row++)
                {
                    int chipCount = chipCounts[row];
                    int rowWidth = chipCount * chipSize + (chipCount - 1) * chipSpacing;
                    int startX = panelLeft + (panelSize - rowWidth) / 2;

                    for (int col = 0; col < chipCount; col++)
                    {
                        int x = startX + col * (chipSize + chipSpacing);
                        int y = startY + row * (chipSize + chipSpacing);
                        chipCenters.Add(new Point(x + chipSize / 2, y + chipSize / 2));
                    }
                }
            }
            catch { }            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);
                Graphics g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // 패널 영역 채우기
                Brush panelBrush = new SolidBrush(Color.LightGray);
                g.FillRectangle(panelBrush, panelLeft, panelTop, panelSize, panelSize);

                // 칩 그리기
                Brush chipBrush = Brushes.Gray;
                foreach (Point center in chipCenters)
                {
                    int x = center.X - chipSize / 2;
                    int y = center.Y - chipSize / 2;
                    g.FillRectangle(chipBrush, x, y, chipSize, chipSize);
                }

                // 펜 설정
                Pen redPen = new Pen(Color.Red, 2);

                // 가이드라인 함수 정의
                void DrawHorizontal(string key, Point left, Point right, int offsetY)
                {
                    int y = left.Y + offsetY;
                    g.DrawLine(redPen, left.X, y, right.X, y);
                    g.DrawLine(redPen, left.X, y - 5, left.X, y + 5);   // 왼쪽
                    g.DrawLine(redPen, right.X, y - 5, right.X, y + 5); // 오른쪽
                    guidePositions[key] = new Point((left.X + right.X) / 2, y);
                }

                void DrawVertical(string key, Point top, Point bottom, int offsetX)
                {
                    int x = top.X + offsetX;
                    g.DrawLine(redPen, x, top.Y, x, bottom.Y);
                    g.DrawLine(redPen, x - 5, top.Y, x + 5, top.Y);       // 위
                    g.DrawLine(redPen, x - 5, bottom.Y, x + 5, bottom.Y); // 아래
                    guidePositions[key] = new Point(x, (top.Y + bottom.Y) / 2);
                }

                // ====== PKG X Pitch ======
                int xPitchRowIndex = 1;
                int xPitchStartIndex = 0;
                for (int i = 0; i < xPitchRowIndex; i++)
                    xPitchStartIndex += chipCounts[i];

                if (chipCounts[xPitchRowIndex] >= 2 && chipCenters.Count > xPitchStartIndex + 1)
                {
                    Point xPitchChip1 = chipCenters[xPitchStartIndex];
                    Point xPitchChip2 = chipCenters[xPitchStartIndex + 1];
                    DrawHorizontal("PKG_X_Pitch", xPitchChip1, xPitchChip2, -30);
                }

                // ====== PKG Y Pitch ======
                int yPitchRow1 = 2;
                int yPitchRow2 = 3;
                int startIdx1 = 0;
                int startIdx2 = 0;
                for (int i = 0; i < yPitchRow1; i++)
                    startIdx1 += chipCounts[i];

                for (int i = 0; i < yPitchRow2; i++)
                    startIdx2 += chipCounts[i];

                if (chipCounts[yPitchRow1] > 0 && chipCounts[yPitchRow2] > 0)
                {
                    int idx1 = startIdx1 + chipCounts[yPitchRow1] - 1;
                    int idx2 = startIdx2 + chipCounts[yPitchRow2] - 1;
                    if (idx1 < chipCenters.Count && idx2 < chipCenters.Count)
                    {
                        DrawVertical("PKG_Y_Pitch", chipCenters[idx1], chipCenters[idx2], 30);
                    }
                }

                // ====== PKG X Size ======
                int xSizeRow = 4;
                int xSizeCol = 0;
                int xStartIdx = 0;
                for (int i = 0; i < xSizeRow; i++)
                    xStartIdx += chipCounts[i];

                if (xSizeRow < chipCounts.Length && chipCounts[xSizeRow] > xSizeCol)
                {
                    int xSizeIdx = xStartIdx + xSizeCol;
                    if (xSizeIdx < chipCenters.Count)
                    {
                        Point start = new Point(chipCenters[xSizeIdx].X - 20, chipCenters[xSizeIdx].Y + 90);
                        Point end = new Point(start.X + 40, start.Y);
                        DrawHorizontal("PKG_X_Size", start, end, 30);
                    }
                }

                // ====== PKG Y Size ======
                int ySizeTargetRow = 8;
                int ySizeTargetCol = chipCounts[ySizeTargetRow] - 1;
                int ySizeStartIdx = 0;
                for (int i = 0; i < ySizeTargetRow; i++)
                    ySizeStartIdx += chipCounts[i];

                int ySizeIdx = ySizeStartIdx + ySizeTargetCol;
                if (ySizeIdx < chipCenters.Count)
                {
                    Point top = new Point(chipCenters[ySizeIdx].X, chipCenters[ySizeIdx].Y - 20);
                    Point bottom = new Point(chipCenters[ySizeIdx].X, chipCenters[ySizeIdx].Y + 20);
                    DrawVertical("PKG_Y_Size", top, bottom, 30);
                }

                // ====== Wafer Real Size ======
                int centerX = panelLeft + panelSize / 2;
                DrawVertical("Wafer_Real_Size", new Point(centerX, panelTop + 10), new Point(centerX, panelTop + panelSize - 10), 0);

                // ====== Safety Distance ======
                DrawVertical("Safety_Distance", new Point(centerX + 50, panelTop + 3), new Point(centerX + 50, panelTop + 50), 0);
            }
            catch { }            
        }

        private void AddGuideControls()
        {
            try
            {
                Font labelFont = new Font("Segoe UI", 10, FontStyle.Bold);

                // 항목별 위치 및 크기 오프셋 설정 (LabelX, LabelY, NudX, NudY, NudWidth)
                var layoutMap = new Dictionary<string, Tuple<int, int, int, int, int>>
            {
                { "PKG_X_Pitch",     Tuple.Create(-50, -50, -50, -30, 80) },
                { "PKG_Y_Pitch",     Tuple.Create(10, -20, 10, 0, 80) },
                { "PKG_X_Size",      Tuple.Create(-65,  15, -65,  35, 80) },
                { "PKG_Y_Size",      Tuple.Create(10, -20, 10, 0, 80) },
                { "Wafer_Real_Size", Tuple.Create(10, 255, 10, 275, 80) },
                { "Safety_Distance", Tuple.Create(10, -20, 10, 0, 80) }
            };

                void AddPair(string key, string labelText, int decimalPlaces)
                {
                    if (!guidePositions.ContainsKey(key) || !layoutMap.ContainsKey(key))
                        return;

                    Point basePoint = guidePositions[key];
                    var layout = layoutMap[key];

                    int lblX = basePoint.X + layout.Item1;
                    int lblY = basePoint.Y + layout.Item2;
                    int nudX = basePoint.X + layout.Item3;
                    int nudY = basePoint.Y + layout.Item4;
                    int nudW = layout.Item5;

                    // 라벨 생성
                    Label lbl = new Label();
                    lbl.Text = labelText;
                    lbl.Font = labelFont;
                    lbl.AutoSize = true;
                    lbl.Location = new Point(lblX, lblY);
                    lbl.Name = key + "_Lbl";

                    // NumericUpDown 생성
                    NumericUpDown nud = new NumericUpDown();
                    nud.DecimalPlaces = decimalPlaces;
                    nud.Increment = (decimalPlaces == 3) ? 0.001M : 0.1M;
                    nud.Minimum = 0;
                    nud.Maximum = 1000;
                    nud.Value = (decimalPlaces == 3) ? 0.100M : 1.0M;
                    nud.Width = nudW;
                    nud.Location = new Point(nudX, nudY);
                    nud.Name = key + "_Nud";

                    Controls.Add(lbl);
                    Controls.Add(nud);
                }

                AddPair("Safety_Distance", "Safety Distance", 1);
                AddPair("Wafer_Real_Size", "Wafer Real Size", 1);
                AddPair("PKG_X_Size", "PKG X Size", 1);
                AddPair("PKG_Y_Size", "PKG Y Size", 1);
                AddPair("PKG_X_Pitch", "PKG X Pitch", 3);
                AddPair("PKG_Y_Pitch", "PKG Y Pitch", 3);            
            }
            catch { }
        }            

        private void btnWaferMapDraw_Click(object sender, EventArgs e)
        {
            try
            {
                var guideValues = GetGuideValues();
                if (guideValues.Count == 0)
                {
                    MessageBox.Show("입력값을 확인하세요", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                WaferMapForm form = new WaferMapForm(guideValues);
                form.Show();
            }
            catch { }            
        }

        private Dictionary<string, decimal> GetGuideValues()
        {            
            try
            {
                string[] keys = {
                "Safety_Distance", "Wafer_Real_Size",
                "PKG_X_Size", "PKG_Y_Size",
                "PKG_X_Pitch", "PKG_Y_Pitch",
                "PKG_X_Count", "PKG_Y_Count"
                };

                Dictionary<string, decimal> result = new Dictionary<string, decimal>();

                foreach (string key in keys)
                {
                    var control = Controls.Find(key + "_Nud", true);
                    if (control.Length > 0 && control[0] is NumericUpDown nud)
                    {
                        result[key] = nud.Value;
                    }
                    else
                    {
                        MessageBox.Show($"{key} 값을 찾을 수 없습니다", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return new Dictionary<string, decimal>(); // 오류 발생 시 빈 딕셔너리 반환
                    }
                }

                return result;
            }
            catch 
            {
                return null;
            }
        }                            
    }
}
