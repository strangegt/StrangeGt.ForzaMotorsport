using System;

namespace StrangeGt.ForzaMotorsport.Listener
{
    public class ExceptionEventArgs:EventArgs
    {
        public ExceptionEventArgs(Exception exception):base()
        {
            Exception = exception;
        }
        public Exception Exception { get ; private set; }
    }
}