<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<qiang.Models.Test>>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Show</title>
</head>
<body>
    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>
    <table>
        <tr>
            <th>
                <%: Html.DisplayNameFor(model => model.name) %>
            </th>
            <th></th>
        </tr>
    
    <% foreach (var item in Model) { %>
        <tr>
            <td>
                <%: Html.DisplayFor(modelItem => item.name) %>
            </td>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) %> |
                <%: Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ }) %> |
                <%: Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ }) %>
            </td>
        </tr>
    <% } %>
    
    </table>
</body>
</html>
