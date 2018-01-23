<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<qiang.Models.Test>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Delete</title>
</head>
<body>
    <h3>Are you sure you want to delete this?</h3>
    <fieldset>
        <legend>Test</legend>
    
        <div class="display-label">
            <%: Html.DisplayNameFor(model => model.name) %>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.name) %>
        </div>
    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
            <input type="submit" value="Delete" /> |
            <%: Html.ActionLink("Back to List", "List") %>
        </p>
    <% } %>
    
</body>
</html>
