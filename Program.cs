using FAQ;
using OpenAI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration configuration = builder.Configuration;

/// <summary>
/// Creates an HTTP Client. 
/// </summary>
/// <returns></returns>
//async Task<HttpClient> InitializeFAQHttpClient( IConfigurationSection configurationSection )
//{
//    string apiKey = configurationSection.GetSection("API_KEY").Value;
//    string openAiUrl = configurationSection.GetSection("API_URL").Value;
//    HttpClient client = new HttpClient();
//    client.BaseAddress = new Uri(openAiUrl);
//    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
//    return client;
//}

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins(configuration["AllowedHosts"])
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddOpenAIService<ChatGPTClient>(settings =>
//{
//    settings.ApiKey = configuration["OpenAI:API_KEY"];
//    settings.Model = configuration["OpenAI:MODEL"]

//});
//builder.Services.AddSingleton<HttpClient>(InitializeFAQHttpClient(configuration.GetSection("OpenAi")).GetAwaiter().GetResult());
//builder.Services.AddSingleton<IFAQClient, FAQClient>();
builder.Services.AddSingleton<ChatGPTClient>();
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();


app.Run();
