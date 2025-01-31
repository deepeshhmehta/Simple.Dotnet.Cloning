﻿using Simple.Dotnet.Cloning.Cloners;
using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Simple.Dotnet.Cloning.Generators
{
    internal static class NullableGenerator
    {
        static readonly Type ClonerOpenType = typeof(RootCloner<>);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILGenerator CopyNullable(this ILGenerator generator, Type type, Type innerType)
        {
            generator.Emit(OpCodes.Ldarga_S, (byte)0); // Load argument onto stack
            generator.Emit(OpCodes.Call, type.GetProperty(nameof(Nullable<int>.HasValue))!.GetGetMethod()!); // Check if it has value

            var returnLabel = generator.DefineLabel(); // Label for return instruction

            // If HasValue
            generator.Emit(OpCodes.Brfalse_S, returnLabel); // If false -> jump
            generator.Emit(OpCodes.Ldarga_S, (byte)0); // Load argument onto stack
            generator.Emit(OpCodes.Call, type.GetProperty(nameof(Nullable<int>.Value))!.GetGetMethod()!); // Use .Value and put onto stack
            generator.Emit(OpCodes.Call, ClonerOpenType.MakeGenericType(innerType).GetMethod(nameof(Cloner.DeepClone), BindingFlags.Public | BindingFlags.Static)!); // Use pushed value and call DeepClone on it
            generator.Emit(OpCodes.Newobj, type.GetConstructor(new[] { innerType })!); // Call new Nullable<T>(value) with a value pushed to stack
            generator.Emit(OpCodes.Stloc_0); // Save 
            generator.Emit(OpCodes.Br_S, returnLabel);

            // Else just skip
            generator.MarkLabel(returnLabel);

            return generator;
        }
    }
}
