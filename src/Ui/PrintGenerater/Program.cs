global using Microsoft.Extensions.DependencyInjection;
global using PrintGenerater.Controller;
global using PrintGenerater.Factories;
global using PrintGenerater.Services;
global using PrintGenerater.Services.Archive;
global using PrintGenerater.Services.Setup;
global using PrintGenerater.Services.Tex;
global using TempriDomain.Archive;
global using TempriDomain.Entity;
global using TempriDomain.Interfaces;
global using TempriDomain.Config;
global using System.Diagnostics;
global using System.Runtime.InteropServices;
global using System.Xml;
global using System.Xml.Linq;
global using System.Threading;
global using PrintGenerater.PrintTask;
using PrintGenerater.Interfaces.Services;
using PrintGenerater.Interfaces.Infrastructure;

var services = new ServiceCollection();

// インフラストラクチャサービス
services.AddSingleton<IGoogleDriveConnector, GoogleDriveConnector>();
services.AddSingleton<ISlidesConnecter, SlidesConnecter>();
services.AddSingleton<IConsoleRepository, ConsoleRepository>();
services.AddSingleton<IImageService, ImageService>();
services.AddSingleton<IQrGenerator, QrGenerator>();
services.AddSingleton<IBarcodeGenerator, BarcodeGenerator>();
services.AddSingleton<ILolipopService, LolipopService>();
services.AddSingleton<ISheetConnecter, SheetConnecter>();

// プリント関連サービス
services.AddSingleton<IPrintFactory, PrintFactory>();
services.AddSingleton<IPrintController, PrintController>();
services.AddSingleton<IPrintClassGetter, PrintClassGetter>();
services.AddSingleton<IPrintImageInserter, PrintImageInserter>();
services.AddSingleton<IPrintSlideRepository, PrintSlideRepository>();
services.AddSingleton<IPrintMasterRepository, PrintMasterRepository>();

// スライド関連サービス
services.AddSingleton<ISlidesService, SlidesService>();
services.AddSingleton<ISlideSetter, SlideSetter>();
services.AddSingleton<ITestSlideSetter, TestSlideSetter>();
services.AddSingleton<IPresentationService, PresentationService>();
services.AddSingleton<IEtzySlideRepository, EtzySlideRepository>();
services.AddSingleton<IEtzyThumbSlideRepository, EtzyThumbSlideRepository>();
services.AddSingleton<ISlidePrintSheetRepository, SlidePrintSheetRepository>();

// シート関連サービス
services.AddSingleton<ISheetCrudService, SheetCrudService>();
services.AddSingleton<ISheetsService, SheetsService>();
services.AddSingleton<IProductSheetRepository, ProductSheetRepository>();
services.AddSingleton<IWorksheetOpener, WorksheetOpener>();
services.AddSingleton<ITableService, TableService>();

// ユーティリティサービス
services.AddSingleton<IAuthorityService, AuthorityService>();
services.AddSingleton<IExportService, ExportService>();
services.AddSingleton<IQrAttacher, QrAttacher>();
services.AddSingleton<IDeleteService, DeleteService>();
services.AddSingleton<IDuplicateService, DuplicateService>();
services.AddSingleton<IInkscapeExporter, InkscapeExporter>();
services.AddSingleton<IPdfCompiler, PdfCompiler>();

// 既存のインターフェース登録
services.AddSingleton<IAmazonImageGenerator, AmazonImageGenerator>();
services.AddSingleton<IBaseSvgGenerator, BaseSvgGenerator>();
services.AddSingleton<ICoverSetter, CoverSetter>();
services.AddSingleton<ICoverWorksheetGenerator, CoverWorksheetGenerator>();
services.AddSingleton<IEcBaseGenerator, EcBaseGenerator>();
services.AddSingleton<IEtzyImageGenerator, EtzyImageGenerator>();
services.AddSingleton<IFtpUploader, FtpUploader>();
services.AddSingleton<IGroupPdfGenerator, GroupPdfGenerator>();
services.AddSingleton<IHtmlGenerator, HtmlGenerator>();
services.AddSingleton<IPdf2PngConverter, Pdf2PngConverter>();
services.AddSingleton<IPdf2SvgConverter, PrintGenerater.Services.Tex.Pdf2SvgConverter>();
services.AddSingleton<IPdfGenerator, PdfGenerator>();
services.AddSingleton<IPdfSplitter, PrintGenerater.Services.PdfSplitter>();
services.AddSingleton<IPrintMasterGetter, PrintMasterGetter>();
services.AddSingleton<IProductImageUploader, ProductImageUploader>();
services.AddSingleton<IQaSheetOpener, QaSheetOpener>();
services.AddSingleton<IStandaloneSvgConverter, PrintGenerater.Services.Tex.StandaloneSvgConverter>();
services.AddSingleton<ISvg2PdfConverter, Svg2PdfConverter>();
services.AddSingleton<ITemplateDuplicator, TemplateDuplicator>();
services.AddSingleton<ITemplateResetter, PrintGenerater.Services.Setup.TemplateResetter>();
services.AddSingleton<ITex2PdfConverter, PrintGenerater.Services.Tex.Tex2PdfConverter>();
services.AddSingleton<ITexDeleter, PrintGenerater.Services.Tex.TexDeleter>();
services.AddSingleton<ITexGenerator, PrintGenerater.Services.Tex.TexGenerator>();
services.AddSingleton<ITexTemplateSelector, PrintGenerater.Services.Setup.TexTemplateSelector>();
services.AddSingleton<IWebpageOpener, WebpageOpener>();
services.AddSingleton<IWorksheetBuilder, PrintGenerater.Services.Setup.WorksheetBuilder>();

// PrintTask登録
services.AddTransient<IPrintTask, HtmlPrintTask>();
services.AddTransient<IPrintTask, QaPrintTask>();
services.AddTransient<IPrintTask, UploadPrintTask>();
services.AddTransient<IPrintTask, WebpagePrintTask>();

var serviceProvider = services.BuildServiceProvider();
var controller = serviceProvider.GetRequiredService<IPrintController>();

using var cts = new CancellationTokenSource();
await controller.Execute(cts.Token);






