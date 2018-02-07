# Nibo.Framework

A collection of useful implementations to make easy to deal with a few things =)

**USAGE:**

**_Authentication_**
Implement interface _IAuthOptions_

```csharp
public interface IAuthOptions
{
    string PassportUrl { get; }
    string JwtPassword { get; }
    string ClientId { get; }
    string ClientSecret { get; }
    string ReturnUrl { get; }
}
```

The, in the _Startup.cs_ do as follow:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    ...

    services.AddNiboAuthentication(new MyAuthOptionsImplementation());

    ...
}
```

Or, if you don't want to implement your own class, just do like this:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddNiboAuthentication(options =>
            options
                .AsClient(this.Configuration.GetValue<string>("Auth:ClientId"))
                .WithSecret(this.Configuration.GetValue<string>("Auth:ClientSecret"))
                .And()
                .AuthenticateOn(this.Configuration.GetValue<string>("Auth:PassportUrl"))
                .WithJwtPassword(this.Configuration.GetValue<string>("Auth:JwtPassword"))
                .And()
                .ReturnToUrl(this.Configuration.GetValue<string>("Auth:ReturnUrl"))
                .Build());
}
```

**_SPA (Single Page Application)_**

Want to use a SPA?

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    ...

    app.UseSPA();

    ...
}
```

Simple as it should be, it will read the files from _wwwroot_, in your WebApi Project directory.