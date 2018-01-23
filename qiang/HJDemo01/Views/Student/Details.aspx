<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<HJDemo01.Models.Student>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Details</title>
</head>
<body>
    <fieldset>
        <legend>Student</legend>
    
        <div class="display-label">
            <%: Html.DisplayNameFor(model => model.Name) %>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.Name) %>
        </div>
    
        <div class="display-label">
            <%: Html.DisplayNameFor(model => model.Sex) %>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.Sex) %>
        </div>
    
        <div class="display-label">
            <%: Html.DisplayNameFor(model => model.Age) %>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.Age) %>
        </div>
    
        <div class="display-label">
            <%: Html.DisplayNameFor(model => model.Email) %>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.Email) %>
        </div>
    </fieldset>
    <p>
    
        <%: Html.ActionLink("Edit", "Edit", new { id=Model.Id }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>
</body>
</html>
