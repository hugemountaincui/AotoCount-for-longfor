using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autocount
{
    public class contain
    {
        public contain() { }

        public contain(int sum,int wz,int jp,int sq,int wt,int zd)
        {
            this.Sum = sum;
            this.Wz = wz;
            this.Jp = jp;
            this.Sq = sq;
            this.Wt = wt;
            this.Zd = zd;
        }

        public void set(int sum, int wz, int jp, int sq, int wt, int zd)
        {
            this.Sum = sum;
            this.Wz = wz;
            this.Jp = jp;
            this.Sq = sq;
            this.Wt = wt;
            this.Zd = zd;
        }

        public int Sum { get; set; }

        public int Wz { get; set; }

        public int Jp{ get; set; }

        public int Sq { get; set; }

        public int Wt { get; set; }

        public int Zd { get; set; }

    }
}
