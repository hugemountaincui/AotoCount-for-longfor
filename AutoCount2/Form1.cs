using Autocount;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoCount2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static project project_backup = new project();//数据还原点
        public static project project_now = new project();//当前时段数据
        public static contain c_jh = new contain(); //江辰还原点
        public static contain c_ba = new contain(); //彼岸还原点
        bool firstcopy1 = true;
        bool firstcopy = true;//判断是否重复复制

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Checked == true)
                this.TopMost = true;
            else
                this.TopMost = false;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            rtb.Text = rtb1.Text = "";
            firstcopy = true;
            tb_wz.Text = tb_wt.Text = tb_sq.Text = tb_zd.Text = tb_qd.Text = tb_jp.Text =
                tb_hjzx.Text = tb_yf.Text = tb_dkh.Text = tb_zj.Text = tb_qt.Text = tb_sum.Text = "0";
            GC.Collect();
        }

        private void btn_transform_Click(object sender, EventArgs e)
        {
            if (rtb.Text == "")
            {
                MessageBox.Show("填写本时段数据");
                return;
            }
            if (rtb1.Text != "")
            {
                project project1 = transport(rtb1.Text, "上一时段");
                tb_wz1.Text = project1.wz.ToString();
                tb_wt1.Text = project1.wt.ToString();
                tb_sq1.Text = project1.sq.ToString();
                tb_jp1.Text = project1.jp.ToString();
                tb_zd1.Text = project1.zd.ToString();
                tb_qd1.Text = project1.qudao.ToString();
                tb_hjzx1.Text = project1.hj.ToString();
                tb_yf1.Text = project1.yf.ToString();
                tb_dkh1.Text = project1.dkh.ToString();
                tb_zj1.Text = project1.zj.ToString();
                tb_qt1.Text = project1.qt.ToString();
                tb_sum1.Text = project1.sum.ToString();

                project_backup.Set(project1);//备份数据
            }
            else
            {
                project_backup.wz = Convert.ToInt32(tb_wz1.Text);
                project_backup.wt = Convert.ToInt32(tb_wt1.Text);
                project_backup.sq = Convert.ToInt32(tb_sq1.Text);
                project_backup.jp = Convert.ToInt32(tb_jp1.Text);
                project_backup.zd = Convert.ToInt32(tb_zd1.Text);
                project_backup.qudao = Convert.ToInt32(tb_qd1.Text);
                project_backup.hj = Convert.ToInt32(tb_hjzx1.Text);
                project_backup.yf = Convert.ToInt32(tb_yf1.Text);
                project_backup.dkh = Convert.ToInt32(tb_dkh1.Text);
                project_backup.zj = Convert.ToInt32(tb_zj1.Text);
                project_backup.qt = Convert.ToInt32(tb_qt1.Text);
                project_backup.sum = Convert.ToInt32(tb_sum1.Text);
            }

            project_now.Set(transport(rtb.Text, "本时段"));
            project project0 = new project();
            project0.Set(project_now);
            project0.Minus(project_backup);
            tb_wz.Text = project0.wz.ToString();
            tb_wt.Text = project0.wt.ToString();
            tb_sq.Text = project0.sq.ToString();
            tb_jp.Text = project0.jp.ToString();
            tb_zd.Text = project0.zd.ToString();
            tb_qd.Text = project0.qudao.ToString();
            tb_hjzx.Text = project0.hj.ToString();
            tb_yf.Text = project0.yf.ToString();
            tb_dkh.Text = project0.dkh.ToString();
            tb_zj.Text = project0.zj.ToString();
            tb_qt.Text = project0.qt.ToString();
            tb_sum.Text = project0.sum.ToString();

            firstcopy = true;
            lbt.Text = "转换成功";
        }

        /// <summary>
        /// 计算各项数据
        /// </summary>
        /// <param name="text">前台台账</param>
        /// <returns></returns>
        private project transport(string text, string t)
        {
            project project = new project();

            text = text.Replace("来访总计", "总计来访");
            text = text.Replace(" ", "");
            text = text.Replace(";", ":");
            text = text.Replace("；", ":");
            text = text.Replace("：", "");
            text = text.Replace(":", "");
            project.sum = Getcount("总计来访", text, t);
            project.wz = Getcount("外展", text, t);
            project.wt = Getcount("外拓", text, t);
            project.sq1 = Getcount("社区", text, t);
            project.jp = Getcount("竞品", text, t);
            project.zd = Getcount("追电", text, t);
            project.zysc = Getcount("专业市场", text, t);
            project.hj = Getcount("呼叫", text, t);
            project.yf = Getcount("约访", text, t);
            project.zrdf = Getcount("自然到访", text, t);
            project.dkh = Getcount("大客户", text, t);
            project.qt = Getcount("其他", text, t);
            project.zj = Getcount("中介", text, t);
            project.count();

            return project;
        }

        /// <summary>
        /// 计算对应项目的数据
        /// </summary>
        /// <param name="text">总文字</param>
        /// <param name="jh">项目名称</param>
        /// <returns></returns>
        private int Getcount(string text, string jh, string t)
        {
            try
            {
                Match m = Regex.Match(jh, "(?<=" + text + @")\d{1,}");
                return Convert.ToInt32(m.Value);
            }
            catch (Exception e)
            {
                MessageBox.Show(t + "中：" + text + "未找到！");
                return 0;
            }
        }

        private void btn_copy_long_Click(object sender, EventArgs e)
        {
            string text;
            text = tb_wz.Text + "\t" + tb_wt.Text + "\t" + tb_sq.Text + "\t" + tb_jp.Text + "\t" + tb_zd.Text + "\t"
                + tb_hjzx.Text + "\t" + tb_yf.Text + "\t" + tb_dkh.Text + "\t" + tb_zj.Text + "\t" + tb_qt.Text;
            Clipboard.SetText(text);
            lbt.Text = "复制成功";
            Firstcopy();
        }

        private void Firstcopy()
        {
            if (firstcopy)
            {
                firstcopy = false;
                //备份数据
                project_backup.wz = Convert.ToInt32(tb_wz1.Text);
                project_backup.wt = Convert.ToInt32(tb_wt1.Text);
                project_backup.sq = Convert.ToInt32(tb_sq1.Text);
                project_backup.jp = Convert.ToInt32(tb_jp1.Text);
                project_backup.zd = Convert.ToInt32(tb_zd1.Text);
                project_backup.qudao = Convert.ToInt32(tb_qd1.Text);
                project_backup.hj = Convert.ToInt32(tb_hjzx1.Text);
                project_backup.yf = Convert.ToInt32(tb_yf1.Text);
                project_backup.dkh = Convert.ToInt32(tb_dkh1.Text);
                project_backup.zj = Convert.ToInt32(tb_zj1.Text);
                project_backup.qt = Convert.ToInt32(tb_qt1.Text);
                project_backup.sum = Convert.ToInt32(tb_sum1.Text);
                //加至上时段
                tb_wz1.Text = project_now.wz.ToString();
                tb_wt1.Text = project_now.wt.ToString();
                tb_sq1.Text = project_now.sq.ToString();
                tb_jp1.Text = project_now.jp.ToString();
                tb_zd1.Text = project_now.zd.ToString();
                tb_qd1.Text = project_now.qudao.ToString();
                tb_hjzx1.Text = project_now.hj.ToString();
                tb_yf1.Text = project_now.yf.ToString();
                tb_dkh1.Text = project_now.dkh.ToString();
                tb_zj1.Text = project_now.zj.ToString();
                tb_qt1.Text = project_now.qt.ToString();
                tb_sum1.Text = project_now.sum.ToString();
            }
        }

        private void rtb1_DoubleClick(object sender, EventArgs e)
        {
            RichTextBox r = sender as RichTextBox;
            r.Text = Clipboard.GetText();
        }

        private void btn_redo_Click(object sender, EventArgs e)
        {
            // rtb.Text = rtb1.Text = "撤销成功";
            lbt.Text = "撤销成功";
            firstcopy = true;
            tb_wz.Text = tb_wt.Text = tb_sq.Text = tb_zd.Text = tb_qd.Text = tb_jp.Text =
                tb_hjzx.Text = tb_yf.Text = tb_dkh.Text = tb_zj.Text = tb_qt.Text = tb_sum.Text = "0";

            tb_wz1.Text = project_backup.wz.ToString();
            tb_wt1.Text = project_backup.wt.ToString();
            tb_sq1.Text = project_backup.sq.ToString();
            tb_jp1.Text = project_backup.jp.ToString();
            tb_zd1.Text = project_backup.zd.ToString();
            tb_qd1.Text = project_backup.qudao.ToString();
            tb_hjzx1.Text = project_backup.hj.ToString();
            tb_yf1.Text = project_backup.yf.ToString();
            tb_dkh1.Text = project_backup.dkh.ToString();
            tb_zj1.Text = project_backup.zj.ToString();
            tb_qt1.Text = project_backup.qt.ToString();
            tb_sum1.Text = project_backup.sum.ToString();
        }

        private void btn_copy_short_Click(object sender, EventArgs e)
        {
            string text;
            text = tb_sum.Text + "\t" + tb_hjzx.Text + "\t" + tb_dkh.Text + "\t" + tb_qd.Text;
            Clipboard.SetText(text);
            lbt.Text = "复制成功";
            Firstcopy();
        }

        private void btn_tranform1_Click(object sender, EventArgs e)
        {
            if (rtb_ba.Text == "" || rtb_jh.Text == "")
            {
                MessageBox.Show("两侧都需要填满");
                return;
            }

            contain contain_jc = transport1(rtb_jh,"江宸数据");//转换江辰数据
            contain contain_ba = transport1(rtb_ba,"彼岸数据");//转换彼岸数据

            int zlf1 = Convert.ToInt32(tb_b_zlf1.Text);
            int wz1 = Convert.ToInt32(tb_b_wz1.Text);
            int jp1 = Convert.ToInt32(tb_b_jp1.Text);
            int sq1 = Convert.ToInt32(tb_b_sq1.Text);
            int wt1 = Convert.ToInt32(tb_b_wt1.Text);
            c_ba.set(zlf1, wz1, jp1, sq1, wt1, 0);//设置彼岸还原点
            contain contain_b = calculator_z(contain_ba, zlf1, wz1, jp1, sq1, wt1);//计算彼岸时段数据差值

            int zlf2 = Convert.ToInt32(tb_j_zlf1.Text);
            int wz2 = Convert.ToInt32(tb_j_wz1.Text);
            int jp2 = Convert.ToInt32(tb_j_jp1.Text);
            int sq2 = Convert.ToInt32(tb_j_sq1.Text);
            int wt2 = Convert.ToInt32(tb_j_wt1.Text);
            c_jh.set(zlf2, wz2, jp2, sq2, wt2, 0);//设置江辰还原点
            contain contain_j = calculator_z(contain_jc, zlf2, wz2, jp2, sq2, wt2);//计算江辰时段数据差值

            tb_b_zlf.Text = contain_b.Sum.ToString();
            tb_b_wz.Text = contain_b.Wz.ToString();
            tb_b_jp.Text = contain_b.Jp.ToString();
            tb_b_sq.Text = contain_b.Sq.ToString();
            tb_b_wt.Text = contain_b.Wt.ToString();

            tb_j_zlf.Text = contain_j.Sum.ToString();
            tb_j_wz.Text = contain_j.Wz.ToString();
            tb_j_jp.Text = contain_j.Jp.ToString();
            tb_j_sq.Text = contain_j.Sq.ToString();
            tb_j_wt.Text = contain_j.Wt.ToString();

            firstcopy1 = true;
            lb_t.Text = "转换成功";
        }

        private contain transport1(RichTextBox richTextBox, string t)//转换数据
        {
            contain contain = new contain();
            string s = richTextBox.Text;

            s = s.Replace("来访总计", "总计来访");
            s = s.Replace(" ", "");
            s = s.Replace(";", ":");
            s = s.Replace("；", ":");
            s = s.Replace("：", "");
            s = s.Replace(":", "");
            contain.Sum = Getcount("总计来访", s, t);
            contain.Wz = Getcount("外展", s, t);
            contain.Jp = Getcount("竞品", s, t);
            contain.Sq = Getcount("社区", s, t);
            contain.Wt = Getcount("外拓", s, t);
            return contain;
        }

        private contain calculator_z(contain contain, int zlf, int wz, int jp, int sq, int wt)
        {
            contain contain_z = new contain();
            contain_z.Sum = contain.Sum - zlf;
            contain_z.Wz = contain.Wz - wz;
            contain_z.Jp = contain.Jp - jp;
            contain_z.Sq = contain.Sq - sq;
            contain_z.Wt = contain.Wt - wt;
            return contain_z;
        }

        private void btn_copy_Click(object sender, EventArgs e)
        {
            string s = tb_b_zlf.Text + "\t" + tb_b_wz.Text + "\t" + tb_b_jp.Text + "\t" + tb_b_sq.Text + "\t" +
                tb_b_wt.Text + "\n" + tb_j_zlf.Text + "\t" + tb_j_wz.Text + "\t" + tb_j_jp.Text + "\t" +
                tb_j_sq.Text + "\t" + tb_j_wt.Text;

            Clipboard.SetText(s);
            lb_t.Text = "复制成功";

            if (firstcopy1)
            {
                int zlf1 = Convert.ToInt32(tb_b_zlf1.Text);
                int wz1 = Convert.ToInt32(tb_b_wz1.Text);
                int jp1 = Convert.ToInt32(tb_b_jp1.Text);
                int sq1 = Convert.ToInt32(tb_b_sq1.Text);
                int wt1 = Convert.ToInt32(tb_b_wt1.Text);
                c_ba.set(zlf1, wz1, jp1, sq1, wt1, 0);//设置彼岸还原点

                int zlf2 = Convert.ToInt32(tb_j_zlf1.Text);
                int wz2 = Convert.ToInt32(tb_j_wz1.Text);
                int jp2 = Convert.ToInt32(tb_j_jp1.Text);
                int sq2 = Convert.ToInt32(tb_j_sq1.Text);
                int wt2 = Convert.ToInt32(tb_j_wt1.Text);
                c_jh.set(zlf2, wz2, jp2, sq2, wt2, 0);//设置江辰还原点

                tb_b_zlf1.Text = (Convert.ToInt32(tb_b_zlf1.Text) + Convert.ToInt32(tb_b_zlf.Text)).ToString();
                tb_b_wz1.Text = (Convert.ToInt32(tb_b_wz1.Text) + Convert.ToInt32(tb_b_wz.Text)).ToString();
                tb_b_jp1.Text = (Convert.ToInt32(tb_b_jp1.Text) + Convert.ToInt32(tb_b_jp.Text)).ToString();
                tb_b_sq1.Text = (Convert.ToInt32(tb_b_sq1.Text) + Convert.ToInt32(tb_b_sq.Text)).ToString();
                tb_b_wt1.Text = (Convert.ToInt32(tb_b_wt1.Text) + Convert.ToInt32(tb_b_wt.Text)).ToString();

                tb_j_zlf1.Text = (Convert.ToInt32(tb_j_zlf1.Text) + Convert.ToInt32(tb_j_zlf.Text)).ToString();
                tb_j_wz1.Text = (Convert.ToInt32(tb_j_wz1.Text) + Convert.ToInt32(tb_j_wz.Text)).ToString();
                tb_j_jp1.Text = (Convert.ToInt32(tb_j_jp1.Text) + Convert.ToInt32(tb_j_jp.Text)).ToString();
                tb_j_sq1.Text = (Convert.ToInt32(tb_j_sq1.Text) + Convert.ToInt32(tb_j_sq.Text)).ToString();
                tb_j_wt1.Text = (Convert.ToInt32(tb_j_wt1.Text) + Convert.ToInt32(tb_j_wt.Text)).ToString();

                firstcopy1 = false;
            }
        }

        private void btn_redo1_Click(object sender, EventArgs e)
        {
            tb_b_zlf.Text = tb_b_wz.Text = tb_b_jp.Text = tb_b_sq.Text = tb_b_wt.Text = "0";
            tb_j_zlf.Text = tb_j_wz.Text = tb_j_jp.Text = tb_j_sq.Text = tb_j_wt.Text = "0";

            tb_b_zlf1.Text = c_ba.Sum.ToString();
            tb_b_wz1.Text = c_ba.Wz.ToString();
            tb_b_jp1.Text = c_ba.Jp.ToString();
            tb_b_sq1.Text = c_ba.Sq.ToString();
            tb_b_wt1.Text = c_ba.Wt.ToString();

            tb_j_zlf1.Text = c_jh.Sum.ToString();
            tb_j_wz1.Text = c_jh.Wz.ToString();
            tb_j_jp1.Text = c_jh.Jp.ToString();
            tb_j_sq1.Text = c_jh.Sq.ToString();
            tb_j_wt1.Text = c_jh.Wt.ToString();

            firstcopy1 = true;
            lb_t.Text = "撤销成功";
        }

        private void btn_clear1_Click(object sender, EventArgs e)
        {
            tb_j_zlf.Text = tb_j_wz.Text = tb_j_jp.Text = tb_j_sq.Text = tb_j_wt.Text = "0";
            tb_b_zlf.Text = tb_b_wz.Text = tb_b_jp.Text = tb_b_sq.Text = tb_b_wt.Text = "0";
            firstcopy1 = true;
            rtb_ba.Text = rtb_jh.Text = "";
            lb_t.Text = "";
            GC.Collect();
        }
    }
}
