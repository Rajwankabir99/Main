using ResourceLibrary.Resources;
using Main.Services;
using WebApp.Infrastructure;

internal class Program
{
    private static void Main ( string[] args )
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        AppSettings.Current =
             builder.Configuration
                    .GetSection ( "MyAppSettings" )
                    .Get<MyConfigSettings> ( ) ?? new MyConfigSettings ( );


        builder.Services.AddHttpContextAccessor ( );

        builder.Services.AddScoped<IUserContext,UserContext> ( );

        builder.Services.AddServiceDependencies ( builder.Configuration );

        builder.Services.AddCustomLocalization ( );

        builder.Services.AddControllersWithViews ( );

        builder.Services.AddWebOptimizer ( pipeline =>
        {
            pipeline.CompileLessFiles ( );
        } );

        builder.Logging.ClearProviders ( );

        builder.Logging.AddConsole ( );


        var app = builder.Build();


        if ( app.Environment.IsDevelopment ( ) )
        {
            var options = new DeveloperExceptionPageOptions
            {
                SourceCodeLineCount = 20
            };

            app.UseDeveloperExceptionPage ( options );
        }
        else
        {
            app.UseExceptionHandler ( "/Shared/Error" );
            app.UseHsts ( );
        }

        app.UseStatusCodePages ( );

        //builder.Services.AddHttpsRedirection ( options =>
        //{
        //    options.HttpsPort = 443;
        //} );

        app.UseWebOptimizer ( );

        app.UseStaticFiles ( );

        app.UseRouting ( );

        app.UseSession ( );

        app.UseResponseCaching ( );

        app.UseCors ( "AllowFrontendApp" );    // for dev: "AllowAll"  
                                               // for production: "AllowFrontendApp"    

        app.UseCustomLocalization ( );

        app.UseAuthentication ( );

        app.UseAuthorization ( );

        app.MapControllers ( );

        app.MapDefaultControllerRoute ( );

        app.Run ( );
    }
}