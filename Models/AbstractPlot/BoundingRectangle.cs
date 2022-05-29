namespace StravaViewer.Models.AbstractPlot
{
    public class BoundingRectangle
    {
        double top;
        double bottom;
        double left;
        double right;
        double width;
        double height;
        double verticalCenter;
        double horizontalCenter;

        public BoundingRectangle(double height, double verticalCenter, double bottom, double width)
        {
            this.height = height;
            this.verticalCenter = verticalCenter;
            this.bottom = bottom;
            this.width = width;

            this.left = verticalCenter - width/2;
            this.right = verticalCenter + width/2;

            this.horizontalCenter = bottom + height/2;
        }

        public BoundingRectangle Empty()
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


    }
}
