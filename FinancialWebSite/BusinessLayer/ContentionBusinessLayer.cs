using FinancialWebSite.Data_Access_Layer;
using FinancialWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.BusinessLayer
{
    public class ContentionBusinessLayer
    {

        public Boolean DeleteContention(int CID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var delItem = db.Contentions.FirstOrDefault(m => m.ContentionID == CID);
            if (delItem != null)
            {
                db.Contentions.Remove(delItem);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean DeleteTitle(int TID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var contentions = db.Contentions.Where(m => m.TitleID == TID);
            var titles = db.Titles.SingleOrDefault(m => m.TitleID == TID);
            if (contentions != null)
            {
                foreach (var item in contentions)
                {
                    db.Contentions.Remove(item);
                }
                db.Titles.Remove(titles);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        // 通过内容ID判断是否为一楼
        public Boolean IsFirstFloor(int CID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var contents = from m in db.Contentions
                           select m;

            if (CID != 0)
            {
                contents = contents.Where(s => s.ContentionID == CID);
            }

            return contents.SingleOrDefault<Contention>().CIsFirstFloor;
        }

        // 通过评论ID获取标题ID
        public int GetTitleID(int CID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var contents = from m in db.Contentions
                           select m;

            if(CID != 0)
            {
                contents = contents.Where(s => s.ContentionID == CID);
            }

            return contents.SingleOrDefault<Contention>().TitleID;

        }


        public int SaveTitle(Title t)
        {
            FinancialERPDAL finanDal = new FinancialERPDAL();
            finanDal.Titles.Add(t);
            finanDal.SaveChanges();
            return GetTitle(t.TName);
        }

        public String SaveContention(Contention c)
        {
            FinancialERPDAL finanDal = new FinancialERPDAL();
            finanDal.Contentions.Add(c);
            finanDal.SaveChanges();
            return "提交成功";
        }

        public String GetTitle(int TID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var titles = from m in db.Titles
                         select m;

            if(TID != 0)
            {
                titles = titles.Where(s => s.TitleID == TID);
            }

            Title t = titles.FirstOrDefault<Title>();

            return t.TName;
        }

        public int GetTitle(String Tname)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var titles = from m in db.Titles
                         select m;

            if (!String.IsNullOrEmpty(Tname))
            {
                titles = titles.Where(s => s.TName.Equals(Tname));
            }

            Title t = titles.FirstOrDefault<Title>();

            return t.TitleID;
        }

        public List<Title> GetTitleList(int UID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var titles = from m in db.Titles
                         select m;

            if (UID != 0)
            {
                titles = titles.Where(s => s.UserID == UID);
            }

            return titles.ToList<Title>();
        }

        public List<Title> SearchTitle(String SearchKey)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var titles = from m in db.Titles
                         select m;

            if (!String.IsNullOrEmpty(SearchKey))
            {
                titles = titles.Where(s => s.TName.Contains(SearchKey));
            }

            return titles.OrderByDescending(s => s.TDate).ToList<Title>();
        }

        public List<Title> GetTitleList()
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var titles = from m in db.Titles
                         select m;
            return titles.OrderByDescending(s => s.TDate).ToList<Title>();
        }

        public List<Contention> GetContentionList(int TID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var contents = from m in db.Contentions
                         select m;
            if(TID != 0)
            {
                contents = contents.Where(s => s.TitleID == TID);
                contents = contents.OrderBy(s => s.CDate);
            }
            return contents.ToList<Contention>();
        }

        // 获取回复数
        public List<ReplyNum> GetReplyNum()
        {
            FinancialERPDAL db = new FinancialERPDAL();
            List<ReplyNum> NumR = new List<ReplyNum>();

            var contents = from m in db.Contentions
                           select m;

            var query = from l in contents
                        group l by new { l.TitleID } into g
                        select new
                        {
                            TitleID = g.Key.TitleID,
                            ReplyNumbers = g.Count()
                        };

            foreach (var q in query)
            {
                ReplyNum t = new ReplyNum();
                t.TitleID = q.TitleID;
                t.ReplyNumber = q.ReplyNumbers - 1;
                NumR.Add(t);
            }

            return NumR;
        }

    }
}