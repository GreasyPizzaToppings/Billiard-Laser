using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace billiard_laser
{
    public class DoubleRangeSelector : Control
    {
        private int minimum;
        private int maximum;
        private int rangeMin;
        private int rangeMax;

        private Rectangle minThumb;
        private Rectangle maxThumb;
        private bool draggingMin;
        private bool draggingMax;
        
        public DoubleRangeSelector()
        {
            this.Minimum = 0;
            this.Maximum = 100;
            this.RangeMin = 25;
            this.RangeMax = 75;

            this.minThumb = new Rectangle(0, 0, 10, 20);
            this.maxThumb = new Rectangle(0, 0, 10, 20);

            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
        }

        [Category("Behavior")]
        [DefaultValue(0)]
        public int Minimum
        {
            get => minimum;
            set
            {
                if (minimum != value)
                {
                    minimum = value;
                    this.Invalidate();
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(100)]
        public int Maximum
        {
            get => maximum;
            set
            {
                if (maximum != value)
                {
                    maximum = value;
                    this.Invalidate();
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(25)]
        public int RangeMin
        {
            get => rangeMin;
            set
            {
                if (rangeMin != value)
                {
                    rangeMin = Math.Max(minimum, Math.Min(value, rangeMax));
                    this.Invalidate();
                    this.OnRangeMinChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(75)]
        public int RangeMax
        {
            get => rangeMax;
            set
            {
                if (rangeMax != value)
                {
                    rangeMax = Math.Max(rangeMin, Math.Min(value, maximum));
                    this.Invalidate();
                    this.OnRangeMaxChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler RangeMinChanged;
        public event EventHandler RangeMaxChanged;

        protected virtual void OnRangeMinChanged(EventArgs e)
        {
            this.RangeMinChanged?.Invoke(this, e);
        }

        protected virtual void OnRangeMaxChanged(EventArgs e)
        {
            this.RangeMaxChanged?.Invoke(this, e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (this.minThumb.Contains(e.Location))
            {
                this.draggingMin = true;
            }
            else if (this.maxThumb.Contains(e.Location))
            {
                this.draggingMax = true;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.draggingMin = false;
            this.draggingMax = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.draggingMin)
            {
                int newValue = this.PixelToValue(e.X);
                this.RangeMin = Math.Min(newValue, this.rangeMax);
            }
            else if (this.draggingMax)
            {
                int newValue = this.PixelToValue(e.X);
                this.RangeMax = Math.Max(newValue, this.rangeMin);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int trackHeight = this.Height / 4;
            int trackY = (this.Height - trackHeight) / 2;
            int trackX = this.ValueToPixel(this.minimum);
            int trackWidth = this.ValueToPixel(this.maximum) - trackX;

            using (Brush trackBrush = new SolidBrush(Color.Gray))
            {
                e.Graphics.FillRectangle(trackBrush, trackX, trackY, trackWidth, trackHeight);
            }

            this.minThumb.X = this.ValueToPixel(this.rangeMin) - this.minThumb.Width / 2;
            this.minThumb.Y = trackY - (this.minThumb.Height - trackHeight) / 2;

            this.maxThumb.X = this.ValueToPixel(this.rangeMax) - this.maxThumb.Width / 2;
            this.maxThumb.Y = trackY - (this.maxThumb.Height - trackHeight) / 2;

            using (Brush thumbBrush = new SolidBrush(Color.Blue))
            {
                e.Graphics.FillRectangle(thumbBrush, this.minThumb);
                e.Graphics.FillRectangle(thumbBrush, this.maxThumb);
            }
        }

        private int ValueToPixel(int value)
        {
            float range = this.maximum - this.minimum;
            float scale = (float)(this.Width - this.minThumb.Width) / range;
            return (int)((value - this.minimum) * scale) + this.minThumb.Width / 2;
        }

        private int PixelToValue(int pixel)
        {
            float range = this.maximum - this.minimum;
            float scale = range / (float)(this.Width - this.minThumb.Width);
            return (int)((pixel - this.minThumb.Width / 2) * scale) + this.minimum;
        }
    }
}

