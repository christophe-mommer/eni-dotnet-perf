
using BenchmarkDotNet.Running;
using ZipLibBench;

//var b = new Benchmark();
//b.Setup();
//b.IterationSetup();
//b.SharpZipLib();
//b.SystemIOCompression();

BenchmarkRunner.Run<Benchmark>();