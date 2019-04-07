using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    public class WorldObject: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int x;
        private int y;
        private int[] pos;

        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
                //Position[0] = x;
                SetProperty("X");
            }
        }
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                //Position[1] = y;
                SetProperty("Y");
            }
        }

        //Takes two arguments an x and y coordinate respectively
        public virtual int[] Position {
            get
            {
                return pos;
            }
            set
            {
                int[] p = pos;
                pos = value;
                if(p != null)
                {
                    X = pos[0];
                    Y = pos[1];
                }

                SetProperty("Position");
            }
        }
        public string PictureName { get; set; }

        public void Pos(int x, int y)
        {
            Position[0] = x;
            Position[1] = y;
        }

        protected void SetProperty(string source)
        {
            PropertyChangedEventHandler handle = PropertyChanged;
            if (handle != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(source));
            }
        }
    }
}
