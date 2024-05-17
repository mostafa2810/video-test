// See https://aka.ms/new-console-template for more information
using FFmpeg.AutoGen;
using NReco.VideoConverter;
using NReco.VideoInfo;

Console.WriteLine("Hello, World!");
//string inputFilePath = "C:\\Users\\redaco\\Source\\Repos\\Compress-and-Extract-Audito-from-Mp4\\ConsoleApp19\\lv_0_33.H264";
//string outputFilePath = "C:\\Users\\redaco\\Source\\Repos\\Compress-and-Extract-Audito-from-Mp4\\ConsoleApp19\\lv_0_36.mp4";
//string inputFilePath = "C:\\Users\\redaco\\Source\\Repos\\Compress-and-Extract-Audito-from-Mp4\\ConsoleApp19\\lv_0_33.H264";
//string AudioinputFilePath = "C:\\Users\\redaco\\Source\\Repos\\Compress-and-Extract-Audito-from-Mp4\\ConsoleApp19\\lv_0_2132.mp3";
//string audioFilePath = "C:\\Users\\redaco\\Source\\Repos\\Compress-and-Extract-Audito-from-Mp4\\ConsoleApp19\\lv_0_20240516150810.mp3";
string inputFilePath = "C:\\Users\\redaco\\Source\\Repos\\Compress-and-Extract-Audito-from-Mp4\\ConsoleApp19\\0517.mp4";  
string outputFilePath = "C:\\Users\\redaco\\Source\\Repos\\Compress-and-Extract-Audito-from-Mp4\\ConsoleApp19\\lv_20.mp4";
//string outputFilePath = "C:\\Users\\redaco\\Source\\Repos\\Compress-and-Extract-Audito-from-Mp4\\ConsoleApp19\\lv_12.mp4";
//string outputFilePath1000 = "C:\\Users\\redaco\\Source\\Repos\\Compress-and-Extract-Audito-from-Mp4\\ConsoleApp19\\lv_0_1000.mp4";
//string outputFilePath2000 = "C:\\Users\\redaco\\Source\\Repos\\Compress-and-Extract-Audito-from-Mp4\\ConsoleApp19\\lv_0_2000.mp4";
//string outputFilePath2500 = "C:\\Users\\redaco\\Source\\Repos\\Compress-and-Extract-Audito-from-Mp4\\ConsoleApp19\\lv_0_2500.mp4";
//string outputFilePath3000 = "C:\\Users\\redaco\\Source\\Repos\\Compress-and-Extract-Audito-from-Mp4\\ConsoleApp19\\lv_0_3000.mp4";

try
{
    //await Task.WhenAll(
    //    ExtractAudioAsync(inputFilePath, audioFilePath)
    //   , CompressVideoAsync(inputFilePath, outputFilePath)
    //    );
    //ExtractAudioAsync(inputFilePath, audioFilePath);
    //CompressVideoAsync(inputFilePath, AudioinputFilePath, autputFilePath);
    CompressVideosAsync(inputFilePath, outputFilePath, 1000);
    //CompressVideosAsync(inputFilePath, outputFilePath1000, 1000);
    //CompressVideosAsync(inputFilePath, outputFilePath2000, 2000);
    //CompressVideosAsync(inputFilePath, outputFilePath2500, 2500);
    //CompressVideosAsync(inputFilePath, outputFilePath3000, 3000);

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
static async Task CompressVideoAsync(string inputFile,string inputAudio, string outputFile)
{
    //FileStream fs = File.Create(outputFile);
    var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
    var x = new ConvertSettings();
    //x.VideoFrameRate = 25;
    //x.AppendSilentAudioStream = false;
    //x.MaxDuration = 90;
    //x.CustomOutputArgs = $"-b:v {3500}k -b:a {128}k";
    x.CustomOutputArgs = "-c:v copy -c:a aac -strict experimental";
    //ffMpeg.ConvertMedia(inputFile,null, outputFile, Format.mp4, x);
    //ffMpeg.ConvertMedia(inputFile, outputFile, "mp3");
    ffMpeg.ConvertMedia(new[]
    {
        new FFMpegInput(inputFile),new FFMpegInput(inputAudio)
    },outputFile, Format.mp4, x);

}
static async Task CompressVideosAsync(string inputFile, string outputFile, int? bitRate)
{
    //FileStream fs = File.Create(outputFile);
    var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
    var x = new ConvertSettings();
    x.MaxDuration = 90;
    if (bitRate.HasValue)
    {
        //new FFMpegInput(inputFile).
        var xs = new FFProbe()?.GetMediaInfo(inputFile)?.Streams?.FirstOrDefault(s=>s.CodecType == "video");

        x.VideoFrameRate = 25;
        x.AppendSilentAudioStream = false;
        x.CustomOutputArgs = $"-b:v {bitRate}k -b:a {128}k";
    }
    ffMpeg.ConvertMedia(inputFile,null, outputFile, "mp4", x);
    //ffMpeg.ConvertMedia(inputFile,null, outputFile, Format.mp4, x);
}
static async Task ExtractAudioAsync(string inputFile, string audioFilePath)
{
    //FileStream fs = File.Create(outputFile);
    var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
    ffMpeg.ConvertMedia(inputFile, audioFilePath, "mp3");
}
