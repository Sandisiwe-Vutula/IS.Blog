using IS.Domain.Models;
using IS.Repository;
using IS.Services.Contracts;
using IS.Services.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<BlogDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogConnectionString"), options => options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
});

// Add Identity services
builder.Services.AddIdentity<IS.Domain.Models.User, IdentityRole>()
    .AddEntityFrameworkStores<BlogDBContext>()
    .AddDefaultTokenProviders();

// Learn more about configuring Swagger/OpenAPI at (link unavailable)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JwtBearerPostConfigureOptions>(builder.Configuration.GetSection("Jwt:Key"));

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    options.User.AllowedUserNameCharacters =
//        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
//});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(jwt =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value.ToString());
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", info: new OpenApiInfo
    {
        Version = "v1",
        Title = "IS Blogger",
        Description = "An API that facilitates the communication between Front-end and Database for the blog"
    });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

builder.Services.AddScoped<IFollowersService, FollowersService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();
app.Run();