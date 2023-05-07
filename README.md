# DiscriminatedUnions
A Web Api that uses Discriminated Unions such as OneOf, FluentResults and ErrorOr to return multiple types from the service layer. All examples use the same Movies.Api project with Postgresql as the database.

### Dependencies

* Npgsql PostgreSQL Integration Visual Studio extension
	* Use Server Explorer window to connect to PostgreSQL database
* Microsoft.EntityFrameworkCore.Tools
* Npgsql.EntityFrameworkCore.PostgreSQL

### Movies.Api

* Install Microsoft.EntityFrameworkCore.Tools to use migrations in package manager console.
* Adding Migrations:
	* Run "dotnet ef migrations add Initial --context Movies.Api.Persistence.ApplicationDbContext -o Persistence/Migrations" in the directory of Movies.Api.
	* Go to package manager console and run "Add-Migration initial -OutputDir "Persistence/Migrations"" inside "default project" (from dropdown menu) Movies.Api.

### OneOf

* Install OneOf nuget package.
* Using the OneOf class, we are able to return multiple types from a method and depending on the type returned the calling code can react appropriately.
* When the number of types that can be returned from a method gets large, you can create a new class that inherits from OneOfBase and specifies the return types and instead use the new class you created.
* With the above, you will be required to specify the implicit casts for the various types, to avoid having to do this yourself, you can install OneOf.SourceGenerator and add the attribute [GenerateOneOf] to the class which will then generate the casts for you.