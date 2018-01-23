using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
//using System.Web.Mvc;

namespace System.Web.Mvc  //一般情况下扩展方法所在的命名空间跟要扩展类型的命名空间搞成一致
{
    //扩展方法必须是静态放啊，所在类必须是静态类，所在的命名空间必须改为System.Web.Mvc，则能省略页面中必须添加命名空间的约束
    public static class MyHtmlHelper
    {
        public static string MyLabel(this HtmlHelper helper,string lbText) 
        {
            return string.Format("<p>{0}</p>",lbText);
        }

        //HtmlHelper中遇到了MvcHtmlString时候，不进行编码处理
        public static HtmlString MyMvcHtmlStringLabel(this HtmlHelper helper, string lbText)
        {
            string str = string.Format("<p>{0}</p>", lbText);
            return new HtmlString(str);
        }

        //输出分页的超级链接标签
        public static HtmlString ShowPageNavigate(this HtmlHelper htmlHelper,int currentPage, int pageSize,int totalCount) 
        {
            var redirectTo = htmlHelper.ViewContext.RequestContext.HttpContext.Request.Url.AbsolutePath;
            pageSize = pageSize == 0 ? 3 : pageSize;
            var totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1);//总页数
            var output = new StringBuilder();
            if (totalPages>1)
            {
                if (currentPage!=1)
                {//处理首页链接
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex=1&pageSize={1}'>首页</a>",redirectTo,pageSize);
                }
                if (currentPage > 1)
                {//处理上一页链接
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>上一页</a>", redirectTo,currentPage-1 ,pageSize);
                }
                output.Append("");

                int currint = 5;
                for (int i = 0; i < 10; i++)
                {//一共最多显示10个页码，前5个后5个
                    if ((currentPage + i - currint) >= 1 && (currentPage + i - currint)<=totalPages)
                    {
                        if (currentPage==i)
                        {//处理当前页
                            output.AppendFormat("<a class='cpb' href='{0}?pageIndex={1}&pageSize={2}'>{3}</a>", redirectTo,currentPage ,pageSize,currentPage);
                        }
                        else
                        {//一般页处理
                            output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>{3}</a>", redirectTo, currentPage+i-currint, pageSize, currentPage+i-currint);
                        }
                    }
                    output.Append("  ");
                }
                if (currentPage<totalPages)
                {//处理下一页的连接
                     output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>下一页</a>", redirectTo,currentPage+1,pageSize);
                }
                output.Append("  ");

                if (currentPage!=totalPages)
                {
                     output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>末页</a>", redirectTo,totalPages,pageSize);
                }
                output.Append("  ");

            }
            output.AppendFormat("第{0}页/共{1}页",currentPage,totalPages);
            return new HtmlString(output.ToString());
        }
        public static HtmlString ShowPageNavigateAjax(this AjaxHelper htmlHelper, int currentPage, int pageSize, int totalCount)
        {
            var redirectTo = htmlHelper.ViewContext.RequestContext.HttpContext.Request.Url.AbsolutePath;
            pageSize = pageSize == 0 ? 3 : pageSize;
            var totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1);//总页数
            var output = new StringBuilder();
            if (totalPages > 1)
            {
                if (currentPage != 1)
                {//处理首页链接
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex=1&pageSize={1}'>首页</a>", redirectTo, pageSize);
                }
                if (currentPage > 1)
                {//处理上一页链接
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>上一页</a>", redirectTo, currentPage - 1, pageSize);
                }
                output.Append("");

                int currint = 5;
                for (int i = 0; i < 10; i++)
                {//一共最多显示10个页码，前5个后5个
                    if ((currentPage + i - currint) >= 1 && (currentPage + i - currint) <= totalPages)
                    {
                        if (currentPage == i)
                        {//处理当前页
                            output.AppendFormat("<a class='cpb' href='{0}?pageIndex={1}&pageSize={2}'>{3}</a>", redirectTo, currentPage, pageSize, currentPage);
                        }
                        else
                        {//一般页处理
                            output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>{3}</a>", redirectTo, currentPage + i - currint, pageSize, currentPage + i - currint);
                        }
                    }
                    output.Append("  ");
                }
                if (currentPage < totalPages)
                {//处理下一页的连接
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>下一页</a>", redirectTo, currentPage + 1, pageSize);
                }
                output.Append("  ");

                if (currentPage != totalPages)
                {
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>末页</a>", redirectTo, totalPages, pageSize);
                }
                output.Append("  ");

            }
            output.AppendFormat("第{0}页/共{1}页", currentPage, totalPages);
            return new HtmlString(output.ToString());
        }
    }
}