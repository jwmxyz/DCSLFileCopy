using System;

namespace CopyDirectory.Shared.Utils
{
    public interface IMethodExecutionUtils
    {
        /// <summary>
        /// This should really be ina 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="someMethod">Some method that we want to execute within a try catch</param>
        /// <param name="errorMessage">The partial error message to display if this fails.</param>
        /// <returns>T the default return of the execution method.</returns>
        T TryCatchMethod<T>(Func<T> someMethod, string errorMessage);
    }
}