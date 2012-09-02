/* Bytrix Engine - HSB class
 * Last modified January 23rd 2012 by Martin Caine
 * --------------------------------------------------
 * This class allows us to create HSB colors and convert
 * them to RGB when required.
 * 
 * It's used for smooth hue transitions and brightness fades
 * of colors on sreen.
 */

using Microsoft.Xna.Framework;

namespace BaseGame
{
    /// <summary>
    /// This class provides an HSB formatted Colour.
    /// </summary>
    public class BxHsb
    {
        // Hue is in the range 0 - 360
        public float H
        {
            get { return h; }
            set { h = value % 360.0f; GenerateColor(); }
        }
        private float h;

        // Saturation is in the range 0 to 1
        public float S
        {
            get { return s; }
            set { s = value; GenerateColor(); }
        }
        private float s;

        // Brightness is in the range 0 to 1
        public float B
        {
            get { return b; }
            set { b = value; GenerateColor(); }
        }
        private float b;

        /// <summary>
        /// Creates an HSB formatted Colour
        /// </summary>
        /// <param name="inH">Hue - In the range 0 to 360</param>
        /// <param name="inS">Saturation - In the range 0 to 1</param>
        /// <param name="inB">Brightness - In the range 0 to 1</param>
        public BxHsb(float inH, float inS, float inB)
        {
            h = inH % 360.0f;
            s = inS;
            b = inB;
            GenerateColor();
        }


        private Color color;
        public Color Color
        {
            get { return color; }
            set { color = value; } // should never set this
        }

        public Vector3 Vector3
        {
            get { return new Vector3(color.R, color.G, color.B); }
        }

        // NOTE this function does not check for values out of bounds (mainly hue), 
        // always make sure hue is 0 <= h < 360
        // looking at it, should be able to just do segment % 6...
        private void GenerateColor()
        {
            // if no saturation we just return a grey tone
            if (s == 0)
            {
                color = new Color(b, b, b);
                return;
            }

            int segment;
            float h2, f, p, q, t;

            //divide into 6 segments
            h2 = h / 60.0f;
            // store segment
            segment = (int)h2;
            // store remainder in f
            f = h2 - segment;

            // three parts depending which segment we're in we show a particular piece.
            p = b * (1.0f - s);
            q = b * (1.0f - s * f);
            t = b * (1.0f - s * (1.0f - f));

            switch (segment)
            {
                case 0:
                    color = new Color(b, t, p);
                    return;
                case 1:
                    color = new Color(q, b, p);
                    return;
                case 2:
                    color = new Color(p, b, t);
                    return;
                case 3:
                    color = new Color(p, q, b);
                    return;
                case 4:
                    color = new Color(t, p, b);
                    return;
                default: // case 5, I see now this will take anything not 0 to 4, but should still fit it into the wheel..
                    color = new Color(b, p, q);
                    return;
            }
        }

    }
} // end class bxHSB
