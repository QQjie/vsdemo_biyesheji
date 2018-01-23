<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<qiang.Models.Test>>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>List</title>
</head>
<body>
    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>
    <table>
        <tr>
            <th>
                <%: Html.DisplayNameFor(model => model.id) %>
               
            </th>
            <th>
                 <%: Html.DisplayNameFor(model => model.name) %>
            </th>
        </tr>
    
    <% foreach (var item in Model) { %>
        <tr>
            <td>
                <%: Html.DisplayFor(modelItem => item.id) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.name) %>
            </td>
            <td>
                <%: Html.ActionLink("编辑", "Edit", new { /* id=item.PrimaryKey */ }) %> |
                <%: Html.ActionLink("详情", "Details", new { /* id=item.PrimaryKey */ }) %> |
                <%: Html.ActionLink("删除", "Delete", new { /* id=item.PrimaryKey */ }) %>
            </td>
        </tr>
    <% } %>
    
    </table>
</body>
</html>
