global using ConsoleLibrary.Repository;
global using Google.Apis.Sheets.v4;
global using Google.Apis.Slides.v1;
global using GoogleDriveLibrary.Config;
global using GoogleDriveLibrary.Services;
global using GoogleSlideLibrary.Archive.Repository.Print;
global using GoogleSlideLibrary.Config;
global using GoogleSlideLibrary.Repository;
global using GoogleSlideLibrary.Services;
global using LolipopLibrary;
global using Microsoft.Extensions.DependencyInjection;
global using PrintGenerater.Controller;
global using PrintGenerater.Factories;
global using PrintGenerater.Services;
global using PrintGenerater.Services.Archive;
global using PrintGenerater.Services.Cover;
global using PrintGenerater.Services.Html;
global using PrintGenerater.Services.Qa;
global using PrintGenerater.Services.Setup;
global using PrintGenerater.Services.Tex;
global using PrintGenerater.Services.Upload;
global using PrintGenerater.Services.Webpage;
global using SellerServiceLibrary.Service;
global using SpreadSheetLibrary.Config;
global using SpreadSheetLibrary.Repository.Tempri;
global using SpreadSheetLibrary.Services;
global using TempriDomain.Archive;
global using TempriDomain.Entity;
global using TempriDomain.Interfaces;
global using TempriDomain.Config;
global using System.Diagnostics;
global using System.Runtime.InteropServices;
global using System.Xml;
global using System.Xml.Linq;
global using InkscapeLibrary.Services;
global using System.Threading;
using TempriApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<TaskGenerator>();
builder.Services.AddScoped<IPageService, PageService>();
builder.Services.AddScoped<PageService>();
builder.Services.AddScoped<PrintFactory>();
builder.Services.AddScoped<PdfGenerator>();
builder.Services.AddScoped<AuthorityService>();
builder.Services.AddScoped<GoogleDriveConnector>();
builder.Services.AddScoped<SlidesConnecter>();
builder.Services.AddScoped<ConsoleRepository>();
builder.Services.AddScoped<ExportService>();
builder.Services.AddScoped<PrintController>();
builder.Services.AddScoped<TemplateDuplicator>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<HtmlGenerator>();
builder.Services.AddScoped<QrGenerator>();
builder.Services.AddScoped<QrAttacher>();
builder.Services.AddScoped<EcBaseGenerator>();
builder.Services.AddScoped<BarcodeGenerator>();
builder.Services.AddScoped<CoverGenerator>();
builder.Services.AddScoped<LolipopService>();
builder.Services.AddScoped<UploadService>();
builder.Services.AddScoped<AmazonSlideRepository>();
builder.Services.AddScoped<AmazonImageGenerator>();
builder.Services.AddScoped<SlidesService>();
builder.Services.AddScoped<SheetCrudService>();
builder.Services.AddScoped<SheetConnecter>();
builder.Services.AddScoped<DeleteService>();
builder.Services.AddScoped<SellerJpSheetRepository>();
builder.Services.AddScoped<SlidePrintSheetRepository>();
builder.Services.AddScoped<PrintClassGetter>();
builder.Services.AddScoped<SheetsService>();
builder.Services.AddScoped<UploadProductImageService>();
builder.Services.AddScoped<ProductImageUploader>();
builder.Services.AddScoped<ProductSheetRepository>();
builder.Services.AddScoped<EtzySlideRepository>();
builder.Services.AddScoped<EtzyImageGenerator>();
builder.Services.AddScoped<EtzyThumbSlideRepository>();
builder.Services.AddScoped<WorksheetBuilder>();
builder.Services.AddScoped<QaSheetOpener>();
builder.Services.AddScoped<DuplicateService>();
builder.Services.AddScoped<PrintMasterRepository>();
builder.Services.AddScoped<WorksheetOpener>();
builder.Services.AddScoped<TexGenerator>();
builder.Services.AddScoped<QaSheetRepository>();
builder.Services.AddScoped<PrintMasterGetter>();
builder.Services.AddScoped<PrintSlideRepository>();
builder.Services.AddScoped<PrintImageInserter>();
builder.Services.AddScoped<TestSlideSetter>();
builder.Services.AddScoped<SlideSetter>();
builder.Services.AddScoped<PresentationService>();
builder.Services.AddScoped<TableService>();
builder.Services.AddScoped<WebpageOpener>();
builder.Services.AddScoped<TexTemplateSelector>();
builder.Services.AddScoped<BaseSvgGenerator>();
builder.Services.AddScoped<TexDeleter>();
builder.Services.AddScoped<Pdf2SvgConverter>();
builder.Services.AddScoped<Pdf2PngConverter>();
builder.Services.AddScoped<Tex2PdfConverter>();
builder.Services.AddScoped<InkscapeExporter>();
builder.Services.AddScoped<PdfSplitter>();
builder.Services.AddScoped<StandaloneSvgConverter>();
builder.Services.AddScoped<FtpUploader>();
builder.Services.AddScoped<TemplateResetter>();
builder.Services.AddScoped<CoverWorksheetGenerator>();
builder.Services.AddScoped<GroupPdfGenerator>();
builder.Services.AddScoped<CoverSetter>();
builder.Services.AddScoped<Svg2PdfConverter>();
builder.Services.AddScoped<IConsoleRepository, ConsoleRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapPost("/generate", async (PrintTaskRequest data, TaskGenerator generator) =>
{
    await generator.GeneratePrintTask(data.PrintId, data.Option);
    return Results.Ok("処理完了");
});

app.Run();
