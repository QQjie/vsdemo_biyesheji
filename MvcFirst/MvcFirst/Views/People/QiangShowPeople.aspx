<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<MvcFirst.Models.People>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>QianShowPeople</title>
</head>
<body>
    <div>
        
         <table>
            <tr>
            <td>姓名：</td><td><%= Model.name %></td>
            </tr>
             <tr>
            <td>编号：</td><td><%: Model.id %></td>
            </tr>
             <tr>
            <td>邮箱：</td><td><%= Model.email %></td>
            </tr>
        </table>
    </div>
</body>
</html>
