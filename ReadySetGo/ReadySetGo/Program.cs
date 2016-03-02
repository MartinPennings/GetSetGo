using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace ReadySetGo
{
    class Program
    {
        static void Main(string[] args)
        {

            // Instantiate a stopwatch
            Stopwatch sw = new Stopwatch();

            // Init the tracing
            // Output to the system tempfolder
            String fileLocation = Path.Combine(Path.GetTempPath(), "MyTraceFile.txt");
            Stream outputFile = File.Create(fileLocation);

            // Setup the tracing
            TextWriterTraceListener textListener = new TextWriterTraceListener(outputFile);
            TraceSource traceSource = new TraceSource("Stopwatch Trace", SourceLevels.All);

            traceSource.Listeners.Clear(); // remove the default ConsoleTraceListener
            traceSource.Listeners.Add(textListener); // add our textlistener

            sw.Start(); // start the stopwatch

            traceSource.TraceEvent(TraceEventType.Verbose, 100, "Invoking DoSomeHeavyWork");
            DoSomeHeavyWork();

            sw.Stop();// stop the stopwatch

            Console.WriteLine("Tracefile written to {0}", fileLocation);

            if (sw.ElapsedMilliseconds > 1000)
            {
                traceSource.TraceEvent(TraceEventType.Warning, 100, "DoSomeHeavyWork took {0} miliseconds", sw.ElapsedMilliseconds.ToString());
            }
            else
            {
                traceSource.TraceEvent(TraceEventType.Verbose, 100, "DoSomeHeavyWork took {0} miliseconds", sw.ElapsedMilliseconds.ToString());
            }
            
            // finish up the traceSource
            traceSource.Flush();
            traceSource.Close();

            // for your convenience: show the tracefile in your default editor
            Process.Start(fileLocation);

            Console.WriteLine("Press a key to exit");
            Console.Read();

        }

        static void DoSomeHeavyWork()
        {
            //List<string> lst = new List<string>();
            
            //for (int i = 0; i <= 20000; i++)
            //{
            //    if (!lst.Contains(i.ToString())){
            //        lst.Add(i.ToString());
            //    }
            //}
        }
    }
}
