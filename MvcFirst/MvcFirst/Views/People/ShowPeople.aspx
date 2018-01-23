<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="MvcFirst.Models" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>ShowPeople</title>
</head>
<body>
    <div>
        <% People p = ViewData["people"] as MvcFirst.Models.People; %>

        <table>
            <tr>
            <td>姓名：</td><td><%= p.name %></td>
            </tr>
             <tr>
            <td>编号：</td><td><%= p.id %></td>
            </tr>
             <tr>
            <td>邮箱：</td><td><%= p.email %></td>
            </tr>
        </table>
    </div>
</body>
</html>
