# personal-blog
#
This is an example app that I built to fill in the gaps for a project that was starting. Its a modern MVC 5 application with serverside validation based on bootstrap hooked up through a unit of work generic repository to nHibernate connected with Oracle driver to Oracle DB (Oracle DB 11g). The authentication is simple forms authentication with no role scope. For lose coupling and testability the repositories are injected into controllers with Ninject. Username is admin, Password is admin.
#
### Architectural Notes:
* .hbm mappings are a planned constraint of this exercise, for new projects this should be avoided in favour of Fluent nHibernate Code First mapping
* Custom validation logic present is very limited and should not be used for bigger projects, for error handling I suggest extending the Html.ValidationSummary with relevant logic through an extension method and then handling error response overriding OnResultExecuted in the BaseController
* Javascript in this exercise is used scarcily to render lib objects such as summernote text editor hence it is present directly in the views, on larger projects it would benefit the project to structure js code in something like Revealing Module Pattern

### Paterns Used:
* Unit Of Work
* MVC
* Dependency Injection - IoC
* Attribute Routing
* Simple Forms Authentication

### Dependencies:
* Bootstrap, ASP.NET MVC 5, Forms Authentication, nHibernate with hbm.xml mapping pinned to Oracle Database 11g, Log4Net, AutoMapper, Ninject, Summernote, Typeahead, Datatables, Font Awesome
