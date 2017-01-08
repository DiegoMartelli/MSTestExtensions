﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTestExtensions
{
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static class ExceptionAssert
    {
        public static T Throws<T>(Action task, string expectedMessage, ExceptionMessageCompareOptions messageOptions, ExceptionInheritanceOptions inheritOptions) where T : Exception
        {
            try
            {
                task();
            }
            catch (Exception ex)
            {
                return CheckException<T>(expectedMessage, messageOptions, inheritOptions, ex);
            }

            OnNoExceptionThrown<T>();

            return default(T);
        }


        /// <summary>
        /// This check if an async method throws an exception as InnerException (chained exception are ignored)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <param name="expectedMessage"></param>
        /// <param name="messageOptions"></param>
        /// <param name="inheritOptions"></param>
        /// <returns></returns>
        public static T ThrowsAsync<T>(Task task, string expectedMessage, ExceptionMessageCompareOptions messageOptions, ExceptionInheritanceOptions inheritOptions) where T : Exception
        {
            try
            {
                task.Wait();
            }
            catch (AggregateException aggregateEx)
            {
                return CheckException<T>(expectedMessage, messageOptions, inheritOptions, aggregateEx.InnerException);
            }

            OnNoExceptionThrown<T>();

            return default(T);
        }

        private static void OnNoExceptionThrown<T>() where T : Exception
        {
            if (typeof(T).Equals(new Exception().GetType()))
            {
                Assert.Fail("Expected exception but no exception was thrown.");
            }
            else
            {
                Assert.Fail(string.Format("Expected exception of type {0} but no exception was thrown.", typeof(T)));
            }
        }

        private static T CheckException<T>(string expectedMessage, ExceptionMessageCompareOptions messageOptions, ExceptionInheritanceOptions inheritOptions, Exception ex) where T : Exception
        {
            AssertExceptionType<T>(ex, inheritOptions);
            AssertExceptionMessage(ex, expectedMessage, messageOptions);
            return (T)ex;
        }



        #region Overloaded methods

        public static T Throws<T>(this IAssertion assertion, Action task, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            return Throws<T>(task, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        public static T Throws<T>(Action task, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            return Throws<T>(task, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        public static T Throws<T>(this IAssertion assertion, Action task, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            return Throws<T>(task, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static T Throws<T>(Action task, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            return Throws<T>(task, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static T Throws<T>(this IAssertion assertion, Action task, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            return Throws<T>(task, expectedMessage, options, inheritOptions);
        }

        public static Exception Throws(this IAssertion assertion, Action task, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            return Throws<Exception>(task, expectedMessage, options, inheritOptions);
        }

        public static Exception Throws(Action task, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            return Throws<Exception>(task, expectedMessage, options, inheritOptions);
        }

        public static Exception Throws(this IAssertion assertion, Action task, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            return Throws<Exception>(task, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static Exception Throws(Action task, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            return Throws<Exception>(task, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static Exception Throws(this IAssertion assertion, Action task, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            return Throws<Exception>(task, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        public static Exception Throws(Action task, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            return Throws<Exception>(task, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }


        /*************/

        public static T ThrowsAsync<T>(this IAssertion assertion, Task task, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            return ThrowsAsync<T>(task, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        public static T ThrowsAsync<T>(Task task, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            return ThrowsAsync<T>(task, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        public static T ThrowsAsync<T>(this IAssertion assertion, Task task, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            return ThrowsAsync<T>(task, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static T ThrowsAsync<T>(Task task, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            return ThrowsAsync<T>(task, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static T ThrowsAsync<T>(this IAssertion assertion, Task task, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits) where T : Exception
        {
            return ThrowsAsync<T>(task, expectedMessage, options, inheritOptions);
        }

        public static Exception ThrowsAsync(this IAssertion assertion, Task task, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            return ThrowsAsync<Exception>(task, expectedMessage, options, inheritOptions);
        }

        public static Exception ThrowsAsync(Task task, string expectedMessage, ExceptionMessageCompareOptions options, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            return ThrowsAsync<Exception>(task, expectedMessage, options, inheritOptions);
        }

        public static Exception ThrowsAsync(this IAssertion assertion, Task task, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            return ThrowsAsync<Exception>(task, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static Exception ThrowsAsync(Task task, string expectedMessage, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            return ThrowsAsync<Exception>(task, expectedMessage, ExceptionMessageCompareOptions.Exact, inheritOptions);
        }

        public static Exception ThrowsAsync(this IAssertion assertion, Task task, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            return ThrowsAsync<Exception>(task, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }

        public static Exception ThrowsAsync(Task task, ExceptionInheritanceOptions inheritOptions = ExceptionInheritanceOptions.Inherits)
        {
            return ThrowsAsync<Exception>(task, null, ExceptionMessageCompareOptions.None, inheritOptions);
        }


        #endregion

        private static void AssertExceptionNotInherited<T>(Exception ex)
        {
            Assert.IsFalse(ex is T);
        }

        private static void AssertExceptionType<T>(Exception ex, ExceptionInheritanceOptions options)
        {
            Assert.IsInstanceOfType(ex, typeof(T), "Expected exception type failed.");
            switch (options)
            {
                case ExceptionInheritanceOptions.Exact:
                    AssertExceptionNotInherited<T>(ex);
                    break;
                case ExceptionInheritanceOptions.Inherits:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("options");

            }
        }

        private static void AssertExceptionMessage(Exception ex, string expectedMessage, ExceptionMessageCompareOptions options)
        {
            if (!string.IsNullOrEmpty(expectedMessage))
            {
                switch (options)
                {
                    case ExceptionMessageCompareOptions.Exact:
                        Assert.AreEqual(expectedMessage.ToUpper(), ex.Message.ToUpper(), "Expected exception message failed.");
                        break;
                    case ExceptionMessageCompareOptions.Contains:
                        Assert.IsTrue(ex.Message.Contains(expectedMessage), string.Format("Expected exception message does not contain <{0}>.", expectedMessage));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("options");
                }

            }
        }
    }
}
