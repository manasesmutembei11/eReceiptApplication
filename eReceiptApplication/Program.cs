using Carter;
using eReceiptApplication.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        builder => builder.WithOrigins("http://localhost:4200") // Update this to the Angular app's URL
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});


License.LicenseKey = builder.Configuration["IronPdf:LicenseKey"];

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorTemplating();

builder.Services.AddCarter();


builder.Services.AddSingleton<InvoiceFactory>();

var app = builder.Build();
app.UseCors("AllowAngular");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCarter();
app.Run();