namespace StravaViewer.Models.AbstractPlot
{
    public class BoundingRectangle
    {
        public double top;
        public double bottom;
        public double left;
        public double right;
        public double width;
        public double height;
        public double verticalCenter;
        public double horizontalCenter;

        public BoundingRectangle(double height, double verticalCenter, double bottom, double width)
        {
            this.height = height;
            this.verticalCenter = verticalCenter;
            this.bottom = bottom;
            this.width = width;

            this.top = bottom + height;

            this.left = verticalCenter - width/2;
            this.right = verticalCenter + width/2;

            this.horizontalCenter = bottom + height/2;
        }

        public static BoundingRectangle Empty()
        {
            return new BoundingRectangle(0, 0, 0, 0);
        }

        public bool Contains(double x, double y)
        {
            bool horizontal_fit = (x > left) && (x < right);
            bool vertical_fit = (y > bottom) && (y < top);

            if (vertical_fit && horizontal_fit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return String.Format("Top: {0}, Center: {1}, Bottom: {2}", top, verticalCenter, bottom);
        }


    }
}
