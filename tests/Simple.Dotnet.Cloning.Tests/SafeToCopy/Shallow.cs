﻿using FluentAssertions;
using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Simple.Dotnet.Cloning.Tests.SafeToCopy
{
    public sealed class Shallow
    {
        [Fact]
        public void ShallowClone_Should_Return_Same_Ref_Instance()
        {
            ShouldBeSame(TimeZoneInfo.Utc);
            ShouldBeSame(new Timer(t => { }));
            ShouldBeSame(new System.Timers.Timer());
            ShouldBeSame("string");
            ShouldBeSame(Task.CompletedTask);
            ShouldBeSame(Task.FromResult(10));
            ShouldBeSame(new Thread(() => { }));
            ShouldBeSame(new TaskCompletionSource<object>());
            ShouldBeSame(new CancellationTokenSource());
            ShouldBeSame(new Lazy<int>(() => 1));
            ShouldBeSame(new AsyncLocal<int>());
            ShouldBeSame(new ThreadLocal<int>());
            ShouldBeSame(new AutoResetEvent(false));
            ShouldBeSame(new Barrier(1));
            ShouldBeSame(new CountdownEvent(1));
            ShouldBeSame(new ManualResetEvent(true));
            ShouldBeSame(new ManualResetEventSlim());
            ShouldBeSame(new Mutex());
            ShouldBeSame(new ReaderWriterLock());
            ShouldBeSame(new SemaphoreSlim(1));
            ShouldBeSame(GetType().Assembly);
            ShouldBeSame(GetType());
            ShouldBeSame(typeof(string).GetMembers().First());
            ShouldBeSame(typeof(string).GetConstructors().First());
            ShouldBeSame(typeof(string).GetFields().First());
            ShouldBeSame(new Exception());
            ShouldBeSame(new ArgumentException());
            ShouldBeSame(new Action(() => { }));
            ShouldBeSame(new Func<int>(() => 1));
        }

        static void ShouldBeSame<T>(T instance) where T : class
        {
            try
            {
                (instance.ShallowClone() == instance).Should().BeTrue();
                (((T)null).ShallowClone() == null).Should().BeTrue();


                var wrapper = new Wrapper<T>(instance);
                (wrapper.ShallowClone().Value == wrapper.Value).Should().BeTrue();

                var structWrapper = new WrapperStruct<T>(instance);
                (structWrapper.ShallowClone().Value == structWrapper.Value).Should().BeTrue();

                var recordWrapper = new WrapperRecord<T>(instance);
                (recordWrapper.ShallowClone().Value == recordWrapper.Value).Should().BeTrue();

                var readonlyWrapper = new WrapperReadonly<T>(instance);
                (readonlyWrapper.ShallowClone().Value == readonlyWrapper.Value).Should().BeTrue();
            }
            finally
            {
                if (instance is IDisposable d) d.Dispose();
            }
        }
    }
}
