using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using TaalMaat.Application.Services;
using TaalMaat.Core.Entities;
using TaalMaat.Core.Interfaces;
using TaalMaat.Infrastructure.Data;
using TaalMaat.Infrastructure.Data.SeedData;
using TaalMaat.Infrastructure.Hubs;
using TaalMaat.Infrastructure.Repositories;
using TaalMaat.Infrastructure.Services;
using TaalMaat.WebUI.Components;
using TaalMaat.WebUI.Components.Account;

var builder = WebApplication.CreateBuilder(args);

// Vervang AddDbContext door AddDbContextFactory om gelijktijdige database acties veilig te verwerken
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/toegang-geweigerd";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

builder.Services.AddMudServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

if (builder.Environment.IsDevelopment())
{
    builder.Services.Configure<Microsoft.AspNetCore.Components.Server.CircuitOptions>(options =>
    {
        options.DetailedErrors = true;
    });
}

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("VrijwilligerOnly", policy => policy.RequireRole("Vrijwilliger"));
    options.AddPolicy("AnderstaligOnly", policy => policy.RequireRole("Anderstalig"));
});

builder.Services.AddSignalR();

// === Repositories ===
builder.Services.AddScoped<IGebruikerRepository, GebruikerRepository>();
builder.Services.AddScoped<IBeschikbaarheidRepository, BeschikbaarheidRepository>();
builder.Services.AddScoped<IBuddyRepository, BuddyRepository>();
builder.Services.AddScoped<INotificatieService, NotificatieService>();
builder.Services.AddScoped<ISessieRepository, SessieRepository>();
builder.Services.AddScoped<IOefeningRepository, OefeningRepository>();
builder.Services.AddScoped<IBerichtRepository, BerichtRepository>();
builder.Services.AddScoped<IChatRapportRepository, ChatRapportRepository>();
builder.Services.AddScoped<IExterneBronRepository, ExterneBronRepository>();

// === Application Services ===
builder.Services.AddScoped<GebruikerService>();
builder.Services.AddScoped<BuddyService>();
builder.Services.AddScoped<SessieService>();
builder.Services.AddScoped<JitsiService>();
builder.Services.AddScoped<CalendarService>();
builder.Services.AddScoped<OefeningService>();
builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<ExterneBronService>();

// Singleton Event Services voor Blazor Server interne broadcasts
builder.Services.AddSingleton<ChatEventService>();
builder.Services.AddSingleton<EncryptieService>();

// Cascading authentication state
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapHub<NotificatieHub>("/notificatiehub");


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapPost("/Account/Logout", async (SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return TypedResults.LocalRedirect("/login");
});
app.MapGet("/Account/Logout", async (SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return TypedResults.LocalRedirect("/login");
});

//app.MapGet("/Account/PerformLogin", async (string email, string code, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) =>
//{
//    var user = await userManager.FindByEmailAsync(email);
//    if (user != null && await userManager.CheckPasswordAsync(user, code))
//    {
//        await signInManager.SignInAsync(user, isPersistent: false);

//        if (user.Rol == TaalMaat.Core.Enums.GebruikerRol.Admin)
//            return TypedResults.LocalRedirect("/admin");
        
//        if (user.Rol == TaalMaat.Core.Enums.GebruikerRol.Vrijwilliger)
//        {
//            if (user.IsAfgekeurd || (!user.IsGeaccepteerd || !user.HeeftWachtkamerGezien))
//                return TypedResults.LocalRedirect("/wachtkamer");
//            else
//                return TypedResults.LocalRedirect("/volunteer");
//        }
        
//        return TypedResults.LocalRedirect("/student");
//    }
//    return TypedResults.LocalRedirect("/");
//});



app.MapPost("/Account/PerformLogin", async (
    HttpContext context,
    [Microsoft.AspNetCore.Mvc.FromServices] SignInManager<ApplicationUser> signInManager,
    [Microsoft.AspNetCore.Mvc.FromServices] UserManager<ApplicationUser> userManager,
    [Microsoft.AspNetCore.Mvc.FromForm] string email,
    [Microsoft.AspNetCore.Mvc.FromForm] string code) =>
{
    var user = await userManager.FindByEmailAsync(email);
    if (user != null && await userManager.CheckPasswordAsync(user, code))
    {
        await signInManager.SignInAsync(user, isPersistent: false);

        if (user.Rol == TaalMaat.Core.Enums.GebruikerRol.Admin)
            return Results.LocalRedirect("/admin");

        if (user.Rol == TaalMaat.Core.Enums.GebruikerRol.Vrijwilliger)
        {
            if (user.IsAfgekeurd || (!user.IsGeaccepteerd || !user.HeeftWachtkamerGezien))
                return Results.LocalRedirect("/wachtkamer");
            else
                return Results.LocalRedirect("/volunteer");
        }

        return Results.LocalRedirect("/student");
    }

    return Results.LocalRedirect("/login?error=invalid");
}).DisableAntiforgery();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await db.Database.MigrateAsync();

    // Gemeente aanmaken als er nog geen is
    if (!await db.Gemeenten.AnyAsync())
    {
        db.Gemeenten.Add(new Gemeente { Naam = "Genk" });
        await db.SaveChangesAsync();
        Console.WriteLine("[SEED] Gemeente 'Genk' is toegevoegd.");
    }

    string[] rollen = ["Admin", "Vrijwilliger", "Anderstalig"];
    foreach (var rol in rollen)
    {
        if (!await roleManager.RoleExistsAsync(rol))
            await roleManager.CreateAsync(new IdentityRole(rol));
    }

    var adminEmail = "admin@taalmaat.be";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            Rol = TaalMaat.Core.Enums.GebruikerRol.Admin,
            IsGeaccepteerd = true,
            AccepteertVoorwaarden = true,
            IsActief = true,
            GeheimWoord = "admin2026"
        };
        var result = await userManager.CreateAsync(adminUser, "Admin@TaalMaat2026!");
        if (result.Succeeded)
            await userManager.AddToRoleAsync(adminUser, "Admin");
    }
    else
    {
        var resetToken = await userManager.GeneratePasswordResetTokenAsync(adminUser);
        await userManager.ResetPasswordAsync(adminUser, resetToken, "Admin@TaalMaat2026!");
    }


    // Password hasher instantie aanmaken voor de update
    var passwordHasher = new PasswordHasher<ApplicationUser>();
    // Alle bestaande gebruikers zonder GemeenteId, GeheimWoord of Voornaam updaten
    var alleGebruikers = await userManager.Users.Where(u => u.GeheimWoord == null || u.GemeenteId == null || string.IsNullOrEmpty(u.Voornaam)).ToListAsync();
    foreach (var g in alleGebruikers)
    {
        bool isUpdated = false;

        if (string.IsNullOrEmpty(g.Voornaam))
        {
            g.Voornaam = g.Email?.Split('@')[0] ?? "Gebruiker";
            g.Achternaam = "TaalMaat";
            Console.WriteLine($"[SEED] Naam ingesteld voor {g.Email}: {g.Voornaam} {g.Achternaam}");
            isUpdated = true;
        }

        if (g.GeheimWoord == null)
        {
            string ongehashtWoord = g.Email?.Split('@')[0] + "1234";
            g.GeheimWoord = passwordHasher.HashPassword(g, ongehashtWoord);
            Console.WriteLine($"[SEED] GeheimWoord voor {g.Email}: {g.GeheimWoord}");
            isUpdated = true;
        }

        if (g.GemeenteId == null)
        {
            // Weet zeker dat GemeenteId 1 (Genk) is toegevoegd
            g.GemeenteId = 1;
            Console.WriteLine($"[SEED] GemeenteId voor {g.Email} ingesteld op 1 (Genk).");
            isUpdated = true;
        }

        if (isUpdated)
        {
            await userManager.UpdateAsync(g);
        }
    }


    
    // Haal alle gebruikers op die een GeheimWoord hebben dat nog niet is gehasht (begint niet met AQAAAA)
    var gebruikersOmTeUpdaten = await userManager.Users
        .Where(u => u.GeheimWoord != null && !u.GeheimWoord.StartsWith("AQAAAA"))
        .ToListAsync();

    if (gebruikersOmTeUpdaten.Any())
    {
        foreach (var g in gebruikersOmTeUpdaten)
        {
            string huidigOngehashtWoord = g.GeheimWoord;
            g.GeheimWoord = passwordHasher.HashPassword(g, huidigOngehashtWoord);
            await userManager.UpdateAsync(g);

            Console.WriteLine($"[MIGRATIE] Bestaand GeheimWoord is succesvol gehasht voor: {g.Email}");
        }
    }
  


    // Seed oefeningen: per niveau (A1 t/m C2) drie oefeningen (video, tekst, audio)
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (!await dbContext.Oefeningen.AnyAsync())
    {
        var seedOefeningen = OefeningSeedData.GetOefeningen();
        dbContext.Oefeningen.AddRange(seedOefeningen);
        await dbContext.SaveChangesAsync();
        Console.WriteLine($"[SEED] {seedOefeningen.Count} oefeningen (A1-C2) zijn toegevoegd.");
    }
    // Versleutel oude onversleutelde berichten
    var encryptieService = scope.ServiceProvider.GetRequiredService<EncryptieService>();
    var alleBerichten = await dbContext.Berichten.ToListAsync();
    int encryptedCount = 0;
    foreach (var bericht in alleBerichten)
    {
        var decrypted = encryptieService.Decrypt(bericht.Inhoud);
        // Als Decrypt() exact de originele string teruggeeft, was deze waarschijnlijk onversleuteld (catch blok)
        if (decrypted == bericht.Inhoud && !string.IsNullOrEmpty(bericht.Inhoud))
        {
            bericht.Inhoud = encryptieService.Encrypt(bericht.Inhoud);
            encryptedCount++;
        }
    }
    if (encryptedCount > 0)
    {
        await dbContext.SaveChangesAsync();
        Console.WriteLine($"[SEED] {encryptedCount} oude berichten zijn succesvol versleuteld in de database.");
    }
}

app.Run();
