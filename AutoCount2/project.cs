using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoCount2
{
     public class project : information 
    {
        public project() { }

        public int qudao { get; set; }

        public int sq { get; set; }

        public int sum { get; set; }

        public int qt { get; set; }

        public void count()
        {
            this.sq = sq1 + zysc;
            this.qt = other + zrdf;
            this.qudao = wz + wt + sq + jp + zysc + zd;            
        }

        public void Minus(project project1)
        {
            wz -= project1.wz;
            wt -= project1.wt;
            sq -= project1.sq;
            jp -= project1.jp;
            zd -= project1.zd;
            qudao -= project1.qudao;
            hj -= project1.hj;
            dkh -= project1.dkh;
            zj -= project1.zj;
            zrdf -= project1.zrdf;
            qt -= project1.qt;
            sum -= project1.sum;
        }

        public void Set(project project1)
        {
            wz = project1.wz;
            wt = project1.wt;
            sq = project1.sq;
            jp = project1.jp;
            zd = project1.zd;
            qudao = project1.qudao;
            hj = project1.hj;
            dkh = project1.dkh;
            zj = project1.zj;
            zrdf = project1.zrdf;
            qt = project1.qt;
            sum = project1.sum;
        }
    }
}
