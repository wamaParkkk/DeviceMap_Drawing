using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DeviceMap_Drawing
{
    public partial class WaferMapForm : Form
    {
        private readonly GuideValues _values;
        private int _totalChips = 0;
        private int _emptyChips = 0;

        public WaferMapForm(Dictionary<string, decimal> guideValues)
        {
            InitializeComponent();
            _values = new GuideValues(guideValues);
            DoubleBuffered = true;
            Paint += WaferMapForm_Paint;
        }

        private void WaferMapForm_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);

                // 파라미터
                float waferSize = _values.WaferRealSize;
                float safety = _values.SafetyDistance;
                float xPitch = _values.PKG_X_Pitch;
                float yPitch = _values.PKG_Y_Pitch;
                float xSize = _values.PKG_X_Size;
                float ySize = _values.PKG_Y_Size;
                int xCount = _values.PKG_X_Count;
                int yCount = _values.PKG_Y_Count;

                float scale = 3.0f;
                float centerX = this.ClientSize.Width / 2f;
                float centerY = this.ClientSize.Height / 2f;
                float waferRadius = waferSize / 2 * scale;
                float usableRadius = waferRadius - (safety * scale);

                g.DrawEllipse(Pens.Red, centerX - waferRadius, centerY - waferRadius, waferRadius * 2, waferRadius * 2);

                _totalChips = 0;
                _emptyChips = 0;
                int chipNumber = 1;

                for (int y = 0; y < yCount; y++)
                {
                    float chipCenterY = centerY - ((yCount - 1) * yPitch / 2 * scale) + y * yPitch * scale;

                    for (int x = 0; x < xCount; x++)
                    {
                        float chipCenterX = centerX - ((xCount - 1) * xPitch / 2 * scale) + x * xPitch * scale;

                        float dx = chipCenterX - centerX;
                        float dy = chipCenterY - centerY;
                        float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                        RectangleF chipRect = new RectangleF(
                            chipCenterX - (xSize / 2 * scale),
                            chipCenterY - (ySize / 2 * scale),
                            xSize * scale,
                            ySize * scale
                        );

                        if (distance <= usableRadius)
                        {
                            g.FillRectangle(Brushes.DimGray, chipRect);
                            g.DrawRectangle(Pens.Black, chipRect.X, chipRect.Y, chipRect.Width, chipRect.Height);

                            string label = chipNumber.ToString();
                            SizeF textSize = g.MeasureString(label, this.Font);
                            g.DrawString(label, this.Font, Brushes.White,
                                chipCenterX - textSize.Width / 2,
                                chipCenterY - textSize.Height / 2);

                            chipNumber++;
                            _totalChips++;
                        }
                        else
                        {
                            g.FillRectangle(Brushes.White, chipRect);
                            g.DrawRectangle(Pens.Gray, chipRect.X, chipRect.Y, chipRect.Width, chipRect.Height);
                            _emptyChips++;
                        }
                    }
                }

                // X축 라벨 (하단 바깥쪽)
                for (int x = 0; x < xCount; x++)
                {
                    float labelX = centerX - ((xCount - 1) * xPitch / 2 * scale) + x * xPitch * scale;
                    string label = (x + 1).ToString();
                    SizeF textSize = g.MeasureString(label, this.Font);
                    g.DrawString(label, this.Font, Brushes.Black,
                        labelX - textSize.Width / 2,
                        centerY + waferRadius - 40); // 위치
                }

                // Y축 라벨 (좌측 바깥쪽, 아래에서 1시작)
                for (int y = 0; y < yCount; y++)
                {
                    float labelY = centerY - ((yCount - 1) * yPitch / 2 * scale) + y * yPitch * scale;
                    string label = (yCount - y).ToString(); // 아래가 1
                    SizeF textSize = g.MeasureString(label, this.Font);
                    g.DrawString(label, this.Font, Brushes.Black,
                        centerX - waferRadius - textSize.Width - 10,
                        labelY - textSize.Height / 2); // 위치
                }

                string summary = $"총 Chip 수: {_totalChips} / 비어있는 Chip: {_emptyChips}";
                g.DrawString(summary, this.Font, Brushes.DarkBlue, 1000, this.ClientSize.Height - 950);
            }
            catch { }            
        }
    }

    public class GuideValues
    {
        public float SafetyDistance { get; }
        public float WaferRealSize { get; }
        public float PKG_X_Size { get; }
        public float PKG_Y_Size { get; }
        public float PKG_X_Pitch { get; }
        public float PKG_Y_Pitch { get; }
        public int PKG_X_Count { get; }
        public int PKG_Y_Count { get; }
        
        public GuideValues(Dictionary<string, decimal> values)
        {
            SafetyDistance = (float)values["Safety_Distance"];
            WaferRealSize = (float)values["Wafer_Real_Size"];
            PKG_X_Size = (float)values["PKG_X_Size"];
            PKG_Y_Size = (float)values["PKG_Y_Size"];
            PKG_X_Pitch = (float)values["PKG_X_Pitch"];
            PKG_Y_Pitch = (float)values["PKG_Y_Pitch"];
            PKG_X_Count = (int)values["PKG_X_Count"];
            PKG_Y_Count = (int)values["PKG_Y_Count"];
        }
    }
}