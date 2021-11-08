﻿using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using static Simple.Dotnet.Cloning.Generators.CustomTypesGenerator;

namespace Simple.Dotnet.Cloning
{
    public static class CloningTypes
    {
        public static HashSet<Type> SafeToCopy = new HashSet<Type>
        {
            // Common:
            typeof(char),
            typeof(char?),
            typeof(byte),
            typeof(byte?),
            typeof(bool),
            typeof(bool?),
            typeof(sbyte),
            typeof(sbyte?),
            typeof(short),
            typeof(short?),
            typeof(ushort),
            typeof(ushort?),
            typeof(int),
            typeof(int?),
            typeof(uint),
            typeof(uint?),
            typeof(long),
            typeof(long?),
            typeof(ulong),
            typeof(ulong?),
            typeof(double),
            typeof(double?),
            typeof(decimal),
            typeof(decimal?),
            typeof(IntPtr),
            typeof(IntPtr?),
            typeof(UIntPtr),
            typeof(UIntPtr?),

            // Useful structs:
            typeof(Guid),
            typeof(Guid?),
            typeof(TimeSpan),
            typeof(TimeSpan?),
            typeof(DateTimeOffset),
            typeof(DateTimeOffset?),
            typeof(DateTime),
            typeof(DateTime?),
            typeof(CancellationToken),
            typeof(CancellationToken?),
            typeof(Memory<>),
            typeof(ReadOnlyMemory<>),
            typeof(ImmutableArray<>),
            typeof(ArraySegment<>),
            typeof(CancellationTokenRegistration),
            typeof(CancellationTokenRegistration?),

            // Classes:
            typeof(string), // immutable
            typeof(TimeZoneInfo),
            typeof(Timer),
            typeof(System.Timers.Timer),

            // Threading
            typeof(Task),
            typeof(Task<>),
            typeof(Thread),
            typeof(TaskCompletionSource<>),
            typeof(CancellationTokenSource),
            typeof(Lazy<>),
            typeof(AsyncLocal<>),
            typeof(ThreadLocal<>),
            typeof(AutoResetEvent),
            typeof(Barrier),
            typeof(CountdownEvent),
            typeof(ManualResetEvent),
            typeof(ManualResetEventSlim),
            typeof(Mutex),
            typeof(Overlapped),
            typeof(ReaderWriterLock),
            typeof(ReaderWriterLockSlim),
            typeof(Semaphore),
            typeof(SemaphoreSlim),
            typeof(WaitHandle),
            typeof(LockCookie),
            typeof(LockCookie?),
            typeof(SpinLock),
            typeof(SpinLock?),
            typeof(SpinWait),
            typeof(SpinWait?),
            typeof(ConcurrentBag<>),
            typeof(ConcurrentDictionary<,>),
            typeof(ConcurrentQueue<>),
            typeof(ConcurrentStack<>),
            typeof(WaitHandle),
            typeof(RegisteredWaitHandle),
            typeof(EventWaitHandle),

            // Reflection
            typeof(Assembly),
            typeof(Type),
            typeof(TypeInfo),
            typeof(MarshalByRefObject),
            typeof(MemberInfo),
            typeof(ConstructorInfo),
            typeof(PropertyInfo),
            typeof(FieldInfo),
            typeof(LocalVariableInfo),
            typeof(ParameterInfo),
            typeof(EventInfo),
            typeof(ManifestResourceInfo),
            typeof(ILGenerator),

            // Immutables
            typeof(ImmutableDictionary<,>),
            typeof(ImmutableStack<>),
            typeof(ImmutableSortedSet<>),
            typeof(ImmutableSortedDictionary<,>),
            typeof(ImmutableQueue<>),
            typeof(ImmutableList<>),
            typeof(ImmutableHashSet<>),

            // Collections
            typeof(LinkedListNode<>), // Cloning will break collection
            typeof(Dictionary<,>.ValueCollection),
            typeof(Dictionary<,>.KeyCollection),
            typeof(SortedDictionary<,>.ValueCollection),
            typeof(SortedDictionary<,>.KeyCollection),

            // Data
            typeof(DBNull),

            // Serialization
            typeof(SerializationInfo),
            typeof(SerializationBinder),
            typeof(SerializationObjectManager),
            typeof(SerializationInfoEnumerator),
            typeof(XmlObjectSerializer),
            typeof(XmlSerializableServices),

            // Interfaces
            typeof(IBufferWriter<>),
            typeof(IComparer<>),
            typeof(ICustomAttributeProvider),
            typeof(IEqualityComparer<>),
            typeof(IFormatter),
            typeof(IFormatterConverter),
            typeof(IFormatProvider),
            typeof(IServiceProvider),
        };

        public static readonly Dictionary<Type, Func<Type[], MethodInfo>> Custom = new Dictionary<Type, Func<Type[], MethodInfo>>
        {
            [typeof(LinkedList<>)] = t => LinkedListOpenedMethod.MakeGenericMethod(t),
            [typeof(Dictionary<,>)] = t => DictionaryOpenedMethod.MakeGenericMethod(t),
            [typeof(SortedDictionary<,>)] = t => SortedDictionaryOpenedMethod.MakeGenericMethod(t)
        };
    }
}
