#define PROFILING

using System;
using System.Diagnostics;

//usage: using var profiler = new ProfilerUtil();

#if PROFILING
public class ProfilerUtil : IDisposable
{
    private Stopwatch sw;
    private bool disposed = false;

    public ProfilerUtil()
    {
        sw = Stopwatch.StartNew();
    }
    // This method is called when the object is no longer needed
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Stop()
    {
        sw.Stop();
        UnityEngine.Debug.Log((new System.Diagnostics.StackTrace()).GetFrame(2).GetMethod().Name + " " + sw.Elapsed.TotalSeconds + " Seconds, (" + sw.Elapsed.TotalMilliseconds + " ms).");
    }
    // This method is called by the Dispose() method or by the finalizer
    // to release the unmanaged resources used by the object
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                UnityEngine.Debug.Log((new System.Diagnostics.StackTrace()).GetFrame(2).GetMethod().Name + " " + sw.Elapsed.TotalSeconds + " Seconds, (" + sw.Elapsed.TotalMilliseconds + " ms).");
            }
            // Release unmanaged resources here
            disposed = true;
        }
    }

    // Destructor
    ~ProfilerUtil()
    {
        Dispose(false);
    }
}
#else
public class ProfilerUtil : IDisposable
{
    private bool disposed = false;

    public ProfilerUtil()
    {
   
    }
    // This method is called when the object is no longer needed
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    // This method is called by the Dispose() method or by the finalizer
    // to release the unmanaged resources used by the object
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
  
            }
            // Release unmanaged resources here
            disposed = true;
        }
    }

    // Destructor
    ~ProfilerUtil()
    {
        Dispose(false);
    }
}
#endif